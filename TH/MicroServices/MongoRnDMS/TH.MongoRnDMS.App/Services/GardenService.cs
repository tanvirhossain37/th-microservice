using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.MongoRnDMS.Core;

namespace TH.MongoRnDMS.App
{
    public class GardenService : BaseService, IGardenService
    {
        public GardenService(IUoW uoW) : base(uoW)
        {
        }

        public async Task SaveAsync(Garden entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            await UoW.GardenRepo.SaveAsync(entity);
        }

        public async Task UpdateAsync(Garden entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            await UoW.GardenRepo.UpdateAsync(entity.Id, entity);
        }

        public async Task DeleteAsync(Garden entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            await UoW.GardenRepo.DeleteAsync(entity.Id);
        }

        public async Task<Garden> FindByIdAsync(long id)
        {
            if (id<=0) throw new ArgumentNullException(nameof(id));

            return await UoW.GardenRepo.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Garden>> GetAsync(GardenFilterModel filter)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            ;
        }
    }
}