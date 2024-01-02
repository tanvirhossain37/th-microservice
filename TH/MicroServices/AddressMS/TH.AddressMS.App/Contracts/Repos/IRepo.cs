using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.AddressMS.App
{
    public interface IRepo<T> where T : class
    {
        T Save(T entity);
        IEnumerable<T> SaveRange(IEnumerable<T> entities);

        T FindById(object id, DataFilter dataFilter = new DataFilter());
    }
}