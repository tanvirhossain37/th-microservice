using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.MongoRnDMS.Core;

namespace TH.MongoRnDMS.App
{
    public class TreeService : BaseService, ITreeService
    {
        public TreeService(IUoW uoW) : base(uoW)
        {
        }

        public async Task SaveAsync(Tree entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            await UoW.TreeRepo.SaveAsync(entity);
        }

        public async Task UpdateAsync(Tree entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            await UoW.TreeRepo.UpdateAsync(entity.Id, entity);
        }

        public async Task DeleteAsync(Tree entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            await UoW.TreeRepo.DeleteAsync(entity.Id);
        }

        public async Task<Tree> FindByIdAsync(long id)
        {
            if (id <= 0) throw new ArgumentNullException(nameof(id));

            return await UoW.TreeRepo.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Tree>> GetAsync(TreeFilterModel filter)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            ;
        }
    }
}