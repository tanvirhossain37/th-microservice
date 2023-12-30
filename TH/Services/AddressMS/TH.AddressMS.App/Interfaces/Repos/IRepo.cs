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
        void SaveRange(IEnumerable<TEntity> entities);

        TEntity FindById(long id, DataFilter dataFilter = new DataFilter());
    }
}
