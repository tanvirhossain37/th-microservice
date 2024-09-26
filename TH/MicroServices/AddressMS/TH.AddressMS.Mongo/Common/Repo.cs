using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.AddressMS.App;
using TH.Common.Model;

namespace TH.AddressMS.Mongo
{
    public class Repo<TEntity> : IRepo<TEntity> where TEntity : class
    {
        protected readonly MongoDbContext DbContext;

        public Repo(MongoDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public TEntity Save(TEntity entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            DbContext.DbSet<TEntity>().InsertOne(entity);
            return entity;
        }

        public IEnumerable<TEntity> SaveRange(IEnumerable<TEntity> entities)
        {
            if (entities is null) throw new ArgumentNullException(nameof(entities));

            DbContext.DbSet<TEntity>().InsertMany(entities);
            return entities;
        }

        public TEntity FindById(object id, DataFilter dataFilter = default)
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", id);

            return DbContext.DbSet<TEntity>().Find(filter).SingleOrDefault();
        }
    }
}