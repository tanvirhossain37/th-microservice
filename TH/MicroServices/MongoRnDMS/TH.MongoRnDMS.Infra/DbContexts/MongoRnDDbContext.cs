using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.MongoRnDMS.Infra
{
    public class MongoRnDDbContext
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        public MongoRnDDbContext(string connectionString, string databaseName)
        {
            connectionString = string.IsNullOrWhiteSpace(connectionString) ? throw new ArgumentNullException(nameof(connectionString)) : connectionString.Trim();
            databaseName = string.IsNullOrWhiteSpace(databaseName) ? throw new ArgumentNullException(nameof(databaseName)) : databaseName.Trim();

            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
        }

        public IMongoCollection<T> DbSet<T>() where T : class
        {
            string name = typeof(T).Name;
            return _database.GetCollection<T>(name);
        }
    }
}