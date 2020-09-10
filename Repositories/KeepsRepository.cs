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
        internal IEnumerable<Keep> Get (string userId)
        {
            string sql = @"
            SELECT * FROM Keeps WHERE userId = @userId";
            return _db.Query<Keep> (sql, new { userId });
        }

        internal Keep GetById (int id)
        {
            string sql = "SELECT * FROM Keeps WHERE id = @Id;";
            return _db.QueryFirstOrDefault<Keep> (sql, new { id });
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
        public bool Delete(string userId, int id)
        {
            string sql = "DELETE FROM keeps WHERE id = @Id AND userId = @userId LIMIT 1;";
            int rowsAffected = _db.Execute(sql, new { userId, id });
            return rowsAffected == 1;
        }
        public bool Update (Keep updatedKeep)
        {
            string sql = @"UPDATE Keeps
            SET 
            name = @name,
            description = @description,
            userId = @userId,
            img = @img,
            isPrivate = @isPrivate,
            views = @views,
            shares = @shares,
            keeps = @keeps
            WHERE id = @id LIMIT 1;";
            int rowsAffected = _db.Execute (sql, updatedKeep);
            return rowsAffected == 1;
        }

    }
}