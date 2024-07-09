using System.Linq.Expressions;
using TH.Common.Model;

namespace TH.Repo;

public interface IRepoSQL<TEntity> where TEntity : class
{
    Task SaveAsync(TEntity entity, DataFilter dataFilter = new DataFilter());
    Task SaveRangeAsync(IEnumerable<TEntity> entities, DataFilter dataFilter = new DataFilter());

    Task<TEntity> FirstOrDefaultQueryableAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        DataFilter dataFilter);

    Task<TEntity> SingleOrDefaultQueryableAsync(Expression<Func<TEntity, bool>> predicate, DataFilter dataFilter = new DataFilter());

    //Task<TEntity?> FindById(string id, string companyId, DataFilter dataFilter = new DataFilter());

    Task<IEnumerable<TEntity>> GetQueryableAsync(Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>> includePredicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, int pageIndex = (int)PageEnum.PageIndex,
        int pageSize = (int)PageEnum.PageSize, DataFilter dataFilter = new DataFilter());

    Task<IEnumerable<TEntity>> GetFilterableAsync(IList<Expression<Func<TEntity, bool>>> predicates,
        IList<Expression<Func<TEntity, object>>> includePredicates, IList<SortFilter> sortFilters,
        int pageIndex = (int)PageEnum.PageIndex,
        int pageSize = (int)PageEnum.PageSize, DataFilter dataFilter = new DataFilter());
}