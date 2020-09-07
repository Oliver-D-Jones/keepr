using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository (IDbConnection db)
        {
            _db = db;
        }
        //NOTE GET ALL PUBLIC KEEPS
        internal IEnumerable<Keep> Get ()
        {
            string sql = "SELECT * FROM Keeps WHERE isPrivate = 0;";
            return _db.Query<Keep> (sql);
        }
        //NOTE GET USER KEEPS
        internal IEnumerable<VaultKeepViewModel> Get (string userId)
        {
            string sql = @"
            SELECT * FROM Keeps WHERE userId = @userId";
            return _db.Query<VaultKeepViewModel> (sql, new { userId });
        }

        internal Keep Create (Keep newKeep)
        {
            string sql = @"INSERT INTO keeps
            (name,description,userId,img,isPrivate,views,shares,keeps)
            VALUES
            (@name, @description, @userId, @img, @isPrivate, @views, @shares, @keeps);
            SELECT LAST_INSERT_ID();";
            newKeep.Id = _db.ExecuteScalar<int> (sql, newKeep);
            return newKeep;
        }
    }
}