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

        Task<T> SaveAsync(T entity);
        Task<IEnumerable<T>> SaveRangeAsync(IEnumerable<T> entities);

        Task<T> UpdateAsync(T entity);
        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);

        Task<T> FindByIdAsync(string id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);

        Task<IEnumerable<T>> GetQueryableAsync(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> includePredicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int pageIndex, int pageSize);
    }
}