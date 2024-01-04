using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TH.MongoRnDMS.App
{
    public interface IRepo<T> where T : class
    {
        Task SaveAsync(T entity);
        Task SaveRangeAsync(IEnumerable<T> entities);

        Task UpdateAsync(object id, T entity);

        Task DeleteAsync(object id);

        Task<T> FindByIdAsync(object id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetQueryableAsync(Expression<Func<T, bool>> filter, int pageIndex, int pageSize);
    }
}