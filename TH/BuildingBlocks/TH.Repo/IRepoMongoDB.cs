using System.Linq.Expressions;
using TH.Common.Model;

namespace TH.Repo;

public interface IRepoMongoDB<T> where T : class
{
    Task SaveAsync(T entity);
    Task SaveRangeAsync(IEnumerable<T> entities);
    Task UpdateAsync(object id, T entity);
    Task DeleteAsync(object id);
    Task<T> FindByIdAsync(object id);
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter);
    Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> filter);
    Task<IEnumerable<T>> GetQueryableAsync(Expression<Func<T, bool>> filter, int pageIndex, int pageSize);
    Task<IEnumerable<T>> GetFilterableAsync(List<Expression<Func<T, bool>>> filters, int pageIndex= (int)PageEnum.PageIndex, int pageSize = (int)PageEnum.PageSize);
}