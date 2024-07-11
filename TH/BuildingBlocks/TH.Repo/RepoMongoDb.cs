using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MongoDB.Driver;
using TH.Common.Model;

namespace TH.Repo;

public class RepoMongoDb<T> : IRepoMongoDB<T> where T : class
{
    protected readonly IDatabase _database;

    public RepoMongoDb(IDatabase database)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
    }

    public async Task SaveAsync(T entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));

        await _database.DbSet<T>().InsertOneAsync(entity);
    }

    public async Task SaveRangeAsync(IEnumerable<T> entities)
    {
        if (entities is null) throw new ArgumentNullException(nameof(entities));

        await _database.DbSet<T>().InsertManyAsync(entities);
    }

    public async Task UpdateAsync(object id, T entity)
    {
        if (id is null) throw new ArgumentNullException(nameof(id));
        if (entity is null) throw new ArgumentNullException(nameof(entity));

        FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
        await _database.DbSet<T>().FindOneAndReplaceAsync(filter, entity);
    }

    public async Task DeleteAsync(object id)
    {
        if (id is null) throw new ArgumentNullException(nameof(id));

        FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
        await _database.DbSet<T>().FindOneAndDeleteAsync(filter);
    }

    public async Task<T> FindByIdAsync(object id)
    {
        if (id is null) throw new ArgumentNullException(nameof(id));

        FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
        return await _database.DbSet<T>().Find(filter).SingleOrDefaultAsync();
    }

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter)
    {
        if (filter is null) throw new ArgumentNullException(nameof(filter));

        return await _database.DbSet<T>().Find(filter).FirstOrDefaultAsync();
    }

    public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> filter)
    {
        if (filter is null) throw new ArgumentNullException(nameof(filter));

        return await _database.DbSet<T>().Find(filter).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetQueryableAsync(Expression<Func<T, bool>> filter, int pageIndex, int pageSize)
    {
        if (filter is null) throw new ArgumentNullException(nameof(filter));
        if (pageIndex <= 0) throw new ArgumentNullException(nameof(pageIndex));
        if (pageSize <= 0) throw new ArgumentNullException(nameof(pageSize));

        return await _database.DbSet<T>().Find(filter).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetFilterableAsync(List<Expression<Func<T, bool>>> filters, int pageIndex = (int)PageEnum.PageIndex,
        int pageSize = (int)PageEnum.PageSize)
    {
        if (filters == null) throw new ArgumentNullException(nameof(filters));
        if (pageIndex <= 0) throw new ArgumentNullException(nameof(pageIndex));
        if (pageSize <= 0) throw new ArgumentNullException(nameof(pageSize));
        var documents = new List<T>();

        foreach (var filter in filters)
        {
            documents.AddRange(await _database.DbSet<T>().Find(filter).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync());
        }

        return documents;
    }
}