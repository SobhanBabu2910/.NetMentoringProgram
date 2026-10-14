using CartService.DAL.Entities;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartService.DAL.Database
{
    public class LiteDbContext
    {
        private readonly LiteDatabase _database;
        public LiteDbContext(string databasePath)
        {
            _database = new LiteDatabase(databasePath);
        }
        public ILiteCollection<Cart> Carts =>_database.GetCollection<Cart>("Carts");
    }
}
