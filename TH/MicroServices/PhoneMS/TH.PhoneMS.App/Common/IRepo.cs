using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TH.PhoneMS.App
{
    public interface IRepo<T> where T : class
    {
        T SaveAsync(T entity);
        IEnumerable<T> SaveRangeAsync(IEnumerable<T> entities);

        T UpdateAsync(T entity);
        IEnumerable<T> UpdateRangeAsync(IEnumerable<T> entities);


        T FindByIdAsync(string id);
        T FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);

        IEnumerable<T> GetQueryable(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> includePredicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int pageIndex, int pageSize);
    }
}