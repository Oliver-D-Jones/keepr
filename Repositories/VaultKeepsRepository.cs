using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository (IDbConnection db)
        {
            _db = db;
        }
        internal IEnumerable<VaultKeepViewModel> Get (int vaultId, string userId)
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
        internal VaultKeep GetById (int id)
        {
            string sql = "SELECT * FROM vaultkeeps WHERE id = @Id;";
            return _db.QueryFirstOrDefault<VaultKeep> (sql, new { id });
        }
        public bool Delete(string userId, int id)
        {
            string sql = "DELETE FROM vaultkeeps WHERE id = @Id AND userId = @userId LIMIT 1;";
            int rowsAffected = _db.Execute(sql, new { userId, id });
            return rowsAffected == 1;
        }
        internal VaultKeep Create (VaultKeep newVaultKeep)
        {

            string sql = @"
            INSERT INTO
                vaultkeeps (vaultId, keepId, userId)
            VALUES
                (@vaultId,@keepId,@userId);
            SELECT LAST_INSERT_ID();";
            newVaultKeep.Id = _db.ExecuteScalar<int> (sql, newVaultKeep);
            return newVaultKeep;
        }
    }
}