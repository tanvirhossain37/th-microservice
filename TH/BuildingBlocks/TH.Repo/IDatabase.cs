namespace TH.Repo;
using MongoDB.Driver;

public interface IDatabase
{
    IMongoCollection<T> DbSet<T>() where T : class;
}