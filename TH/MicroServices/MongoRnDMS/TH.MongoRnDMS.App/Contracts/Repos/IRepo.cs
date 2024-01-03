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
        Task<bool> SaveAsync(T entity);
        Task<bool> SaveRangeAsync(IEnumerable<T> entities);

        Task<T> UpdateAsync(object id, T entity);

        Task<bool> DeleteAsync(object id);

        Task<T> FindByIdAsync(object id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetQueryableAsync(Expression<Func<T, bool>> filter, int pageIndex, int pageSize);
    }
}