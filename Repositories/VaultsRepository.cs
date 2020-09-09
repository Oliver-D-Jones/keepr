using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository (IDbConnection db)
        {
            _db = db;
        }
        internal IEnumerable<Vault> Get (string userId)
        {
            string sql = @"
            SELECT * FROM Vaults WHERE userId = @userId";
            return _db.Query<Vault> (sql, new { userId });
        }
        internal Vault GetById (int vaultId)
        {
            string sql = @"
            SELECT * FROM Vaults WHERE Id = @vaultId";
            return _db.QueryFirstOrDefault<Vault> (sql, new { vaultId });
        }
        internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId (int vaultId, string userId)
        {
            string sql = @"
            SELECT
            k.*,
            vk.id as vaultKeepId
            FROM vaultkeeps vk
            INNER JOIN keeps k ON k.id = vk.keepId
            WHERE (vaultId = @vaultId AND vk.userId = @userId);
            ";
            return _db.Query<VaultKeepViewModel> (sql, new { vaultId, userId });
        }
        internal Vault Create (Vault newVault)
        {
            string sql = @"INSERT INTO Vaults
            (name,description,userId)
            VALUES
            (@name, @description, @userId);
            SELECT LAST_INSERT_ID();";
            newVault.Id = _db.ExecuteScalar<int> (sql, newVault);
            return newVault;
        }
        public bool Delete (string userId, int id)
        {
            string sql = "DELETE FROM vaults WHERE id = @Id AND userId = @userId LIMIT 1;";
            int rowsAffected = _db.Execute (sql, new { userId, id });
            return rowsAffected == 1;
        }
    }
}