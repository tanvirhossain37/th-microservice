using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TH.Common.Model;
using TH.Repo;
using X.PagedList;

namespace TH.Repo;

public class RepoSQL<TEntity> : IRepoSQL<TEntity> where TEntity : class
{
    protected readonly DbContext DbContext;
    protected readonly ICustomSort CustomSort;

    public RepoSQL(DbContext dbContext, ICustomSort customSort)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        CustomSort = customSort ?? throw new ArgumentNullException(nameof(customSort));
    }

    public async Task SaveAsync(TEntity entity, DataFilter dataFilter = new DataFilter())
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var result = await DbContext.Set<TEntity>().AddAsync(entity);
    }

    public async Task SaveRangeAsync(IEnumerable<TEntity> entities, DataFilter dataFilter = new DataFilter())
    {
        if (entities == null) throw new ArgumentNullException(nameof(entities));

        await DbContext.Set<TEntity>().AddRangeAsync(entities);
    }

    public async Task<TEntity> FirstOrDefaultQueryableAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, DataFilter dataFilter)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        if (orderBy == null) throw new ArgumentNullException(nameof(orderBy));
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));

        IQueryable<TEntity> queryResult = DbContext.Set<TEntity>();

        if (predicate != null)
            queryResult = queryResult.Where(predicate);

        if (orderBy != null)
            queryResult = orderBy(queryResult);

        return await queryResult.FirstOrDefaultAsync();
    }

    public async Task<TEntity> SingleOrDefaultQueryableAsync(Expression<Func<TEntity, bool>> predicate, DataFilter dataFilter = new DataFilter())
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));

        IQueryable<TEntity> queryResult = DbContext.Set<TEntity>();

        if (predicate != null)
            queryResult = queryResult.Where(predicate);

        return await queryResult.SingleOrDefaultAsync();
    }

    public void Delete(TEntity entity, DataFilter dataFilter = new DataFilter())
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        DbContext.Set<TEntity>().Remove(entity);
    }

    public void DeleteRange(IEnumerable<TEntity> entities, DataFilter dataFilter = new DataFilter())
    {
        if (entities == null) throw new ArgumentNullException(nameof(entities));

        DbContext.Set<TEntity>().RemoveRange(entities);
    }

    //public async Task<TEntity?> FindById(string id, string companyId, DataFilter dataFilter = new DataFilter())
    //{
    //    if (companyId == null) throw new ArgumentNullException(nameof(companyId));
    //    id = (string.IsNullOrWhiteSpace(id)) ? throw new ArgumentNullException(nameof(id)) : id.Trim();

    //    return await DbContext.Set<TEntity>().FindAsync(id);
    //}

    public async Task<IEnumerable<TEntity>> GetQueryableAsync(Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>> includePredicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        int pageIndex = (int)PageEnum.PageIndex, int pageSize = (int)PageEnum.PageSize,
        DataFilter dataFilter = new DataFilter())
    {
        //if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        //if (includePredicate == null) throw new ArgumentNullException(nameof(includePredicate));
        if (orderBy == null) throw new ArgumentNullException(nameof(orderBy));
        IQueryable<TEntity> queryResult = DbContext.Set<TEntity>();

        if (predicate != null)
            queryResult = queryResult.Where(predicate);

        if (includePredicate != null)
            queryResult = queryResult.Include(includePredicate);

        if (orderBy != null)
            queryResult = orderBy(queryResult);

        if (pageSize == (int)PageEnum.All)
        {
            return await queryResult.ToListAsync();
        }

        return await queryResult.ToPagedListAsync(pageIndex, pageSize);
    }

    public async Task<IEnumerable<TEntity>> GetFilterableAsync(IList<Expression<Func<TEntity, bool>>> predicates,
        IList<Expression<Func<TEntity, object>>> includePredicates, IList<SortFilter> sortFilters, int pageIndex = (int)PageEnum.PageIndex,
        int pageSize = (int)PageEnum.PageSize,
        DataFilter dataFilter = new DataFilter())
    {
        if (predicates == null) throw new ArgumentNullException(nameof(predicates));
        if (includePredicates == null) throw new ArgumentNullException(nameof(includePredicates));
        if (sortFilters == null) throw new ArgumentNullException(nameof(sortFilters));

        IQueryable<TEntity> queryResult = DbContext.Set<TEntity>();

        if (predicates != null)
        {
            foreach (var predicate in predicates)
            {
                if (predicate != null)
                    queryResult = queryResult.Where(predicate);
            }
        }

        if (includePredicates != null)
        {
            foreach (var includePredicate in includePredicates)
            {
                if (includePredicate != null)
                    queryResult = queryResult.Include(includePredicate);
            }
        }

        if (sortFilters != null)
        {
            for (var i = 0; i < sortFilters.Count; i++)
            {
                if (i == 0)
                    queryResult = sortFilters[i].Operation == OrderByEnum.Ascending
                        ? CustomSort.OrderBy(queryResult.OfType<TEntity>(), sortFilters[i].PropertyName)
                        : CustomSort.OrderByDescending(queryResult.OfType<TEntity>(),
                            sortFilters[i].PropertyName);
                else
                    queryResult = sortFilters[i].Operation == OrderByEnum.Ascending
                        ? CustomSort.ThenBy((IOrderedQueryable<TEntity>)queryResult,
                            sortFilters[i].PropertyName)
                        : CustomSort.ThenByDescending((IOrderedQueryable<TEntity>)queryResult,
                            sortFilters[i].PropertyName);
            }
        }

        if (pageSize == (int)PageEnum.All)
        {
            return await queryResult.ToListAsync();
        }

        return await queryResult.ToPagedListAsync(pageIndex, pageSize);
    }
}