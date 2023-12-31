using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.PhoneMS.App
{
    public interface IRepo<T> where T : class
    {
        T Save(T entity);
        IEnumerable<T> SaveRange(IEnumerable<T> entities);

        T Update(T entity);
        IEnumerable<T> UpdateRange(IEnumerable<T> entities);


        T FindById(string id);
        IEnumerable<T> Get();
    }
}
