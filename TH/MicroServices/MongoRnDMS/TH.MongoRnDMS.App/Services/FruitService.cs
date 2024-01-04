using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.MongoRnDMS.Core;

namespace TH.MongoRnDMS.App
{
    public class FruitService : BaseService, IFruitService
    {
        public FruitService(IUoW uoW) : base(uoW)
        {
        }

        public async Task SaveAsync(Fruit entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            await UoW.FruitRepo.SaveAsync(entity);
        }

        public async Task UpdateAsync(Fruit entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            await UoW.FruitRepo.UpdateAsync(entity.Id, entity);
        }

        public async Task DeleteAsync(Fruit entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            await UoW.FruitRepo.DeleteAsync(entity.Id);
        }

        public async Task<Fruit> FindByIdAsync(long id)
        {
            if (id <= 0) throw new ArgumentNullException(nameof(id));

            return await UoW.FruitRepo.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Fruit>> GetAsync(FruitFilterModel filter)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            ;
        }
    }
}