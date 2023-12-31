using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.AddressMS.Mongo
{
    public class MongoDbContext
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;

        public MongoDbContext(string connectionString, string databaseName)
        {
            connectionString = string.IsNullOrWhiteSpace(connectionString) ? throw new ArgumentNullException(nameof(connectionString)) : connectionString.Trim();
            databaseName = string.IsNullOrWhiteSpace(databaseName) ? throw new ArgumentNullException(nameof(databaseName)) : databaseName.Trim();

            _mongoClient = new MongoClient(connectionString);
            _mongoDatabase = _mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<T> DbSet<T>() where T : class
        {
            string name = typeof(T).Name;
            return _mongoDatabase.GetCollection<T>(name);
        }
    }
}