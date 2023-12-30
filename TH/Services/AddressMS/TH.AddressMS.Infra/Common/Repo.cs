using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.AddressMS.App;

namespace TH.AddressMS.Infra
{
    public class Repo<TEntity> : IRepo<TEntity> where TEntity : class
    {
        protected readonly AddressDbContext _db;

        public Repo(AddressDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public TEntity FindById(object id, DataFilter dataFilter = default)
        {
            throw new NotImplementedException();
        }

        public TEntity Save(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> SaveRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}