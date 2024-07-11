using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TH.Repo;

namespace TH.ShadowMS.Infra.DbContexts;

public class ShadowDBContext : IDatabase
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    static IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
    static string _connectionString = configuration.GetConnectionString("MongoDB");
    static string _databaseName = configuration.GetValue<string>("DatabaseName");

    public ShadowDBContext()
    {
        _client = new MongoClient(_connectionString);
        _database = _client.GetDatabase(_databaseName);
    }

    //public ShadowDBContext(string connectionString, string databaseName)
    //{
    //    connectionString = string.IsNullOrWhiteSpace(connectionString)
    //        ? throw new ArgumentNullException(nameof(connectionString))
    //        : connectionString.Trim();
    //    databaseName = string.IsNullOrWhiteSpace(databaseName) ? throw new ArgumentNullException(nameof(databaseName)) : databaseName.Trim();

    //    _client = new MongoClient(connectionString);
    //    _database = _client.GetDatabase(databaseName);
    //}

    public IMongoCollection<T> DbSet<T>() where T : class
    {
        string name = typeof(T).Name;
        return _database.GetCollection<T>(name);
    }
}