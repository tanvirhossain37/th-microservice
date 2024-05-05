using Microsoft.EntityFrameworkCore;

namespace TH.RepoSQL;

public class Repo<TEntity> : IRepo<TEntity> where TEntity : class
{
    protected readonly DbContext DbContext;

    public Repo(DbContext dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<TEntity?> Save(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var result = await DbContext.Set<TEntity>().AddAsync(entity);
        return result.Entity;
    }

    public async Task SaveRange(IEnumerable<TEntity> entities)
    {
        if (entities == null) throw new ArgumentNullException(nameof(entities));

        await DbContext.Set<TEntity>().AddRangeAsync(entities);
    }

    public async Task<TEntity?> FindById(string id)
    {
        id = (string.IsNullOrWhiteSpace(id)) ? throw new ArgumentNullException(nameof(id)) : id.Trim();

        return await DbContext.Set<TEntity>().FindAsync(id);
    }
}