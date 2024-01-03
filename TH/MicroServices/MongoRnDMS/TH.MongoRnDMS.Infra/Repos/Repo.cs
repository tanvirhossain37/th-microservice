using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TH.MongoRnDMS.App;

namespace TH.MongoRnDMS.Infra
{
    public class Repo<T> : IRepo<T> where T : class
    {
        protected readonly MongoRnDDbContext DbContext;
        public Repo(MongoRnDDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<T> FindByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetQueryableAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> includePredicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<T> SaveAsync(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            //DbContext.DbSet<T>().insert(entity);
            //return entity;
            return null;
        }

        public Task<IEnumerable<T>> SaveRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}