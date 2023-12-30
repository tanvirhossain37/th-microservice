using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.AddressMS.App
{
    public interface IRepo<TEntity> where TEntity : class
    {
        TEntity Save(TEntity entity);
        IEnumerable<TEntity> SaveRange(IEnumerable<TEntity> entities);

        TEntity FindById(object id, DataFilter dataFilter = new DataFilter());
    }
}