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
        internal Vault GetById (int vaultId, string userId)
        {
            string sql = @"
            SELECT * FROM Vaults WHERE Id = @vaultId AND userId = @userId";
            return _db.QueryFirstOrDefault<Vault> (sql, new { vaultId, userId });
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
    }
}