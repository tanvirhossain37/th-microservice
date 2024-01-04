using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.MongoRnDMS.Core;

namespace TH.MongoRnDMS.App
{
    public interface IGardenService : IBaseService
    {
        Task SaveAsync(Garden entity);
        Task UpdateAsync(Garden entity);
        Task DeleteAsync(Garden entity);
        Task<Garden> FindByIdAsync(long id);
        Task<IEnumerable<Garden>> GetAsync(GardenFilterModel filter);
    }
}