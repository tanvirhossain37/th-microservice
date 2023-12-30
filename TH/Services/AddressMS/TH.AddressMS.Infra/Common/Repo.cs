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

        public TEntity Save(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            throw new NotImplementedException();
        }

        public void SaveRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public TEntity FindById(long id, DataFilter dataFilter = default)
        {
            throw new NotImplementedException();
        }
    }
}