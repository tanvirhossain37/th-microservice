using MongoDB.Driver;
using SharpCompress.Common;
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

        public async Task<bool> SaveAsync(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            await DbContext.DbSet<T>().InsertOneAsync(entity);
            return true;
        }

        public async Task<bool> SaveRangeAsync(IEnumerable<T> entities)
        {
            if (entities is null) throw new ArgumentNullException(nameof(entities));

            await DbContext.DbSet<T>().InsertManyAsync(entities);
            return true;
        }

        public async Task<T> UpdateAsync(object id, T entity)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
            T val = await DbContext.DbSet<T>().FindOneAndReplaceAsync(filter, entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(object id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));

            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
            T val = await DbContext.DbSet<T>().FindOneAndDeleteAsync(filter);

            return val != null;
        }

        public async Task<T> FindByIdAsync(object id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));

            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
            return await DbContext.DbSet<T>().Find(filter).SingleOrDefaultAsync();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter)
        {
            if (filter is null) throw new ArgumentNullException(nameof(filter));

            return await DbContext.DbSet<T>().Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetQueryableAsync(Expression<Func<T, bool>> filter, int pageIndex, int pageSize)
        {
            if (filter is null) throw new ArgumentNullException(nameof(filter));
            if (pageIndex <= 0) throw new ArgumentNullException(nameof(pageIndex));
            if (pageSize <= 0) throw new ArgumentNullException(nameof(pageSize));

            return await DbContext.DbSet<T>().Find(filter).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
        }
    }
}