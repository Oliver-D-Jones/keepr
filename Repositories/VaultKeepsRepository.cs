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
        internal IEnumerable<VaultKeepViewModel> Create (int vaultId, int keepId,string userId)
        {
            string sql = @"
            INSERT INTO
                vaultkeeps (vaultId, keepId, userId)
            VALUES
                (@vaultId,@keepId,@userId)
            ";
            return _db.Query<VaultKeepViewModel> (sql, new { vaultId,keepId, userId });
        }
    }
}