using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TH.PhoneMS.App;

namespace TH.PhoneMS.Infra
{
    public class Repo<T> : IRepo<T> where T : class
    {
        public T FindByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public T FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetQueryable(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> includePredicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public T SaveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> SaveRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public T UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> UpdateRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
