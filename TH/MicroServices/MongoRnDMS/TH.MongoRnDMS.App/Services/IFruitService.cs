using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.MongoRnDMS.Core;

namespace TH.MongoRnDMS.App
{
    public interface IFruitService : IBaseService
    {
        Task SaveAsync(Fruit entity);
        Task UpdateAsync(Fruit entity);
        Task DeleteAsync(Fruit entity);
        Task<Fruit> FindByIdAsync(long id);
        Task<IEnumerable<Fruit>> GetAsync(FruitFilterModel filter);
    }
}