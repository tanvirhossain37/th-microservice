using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.MongoRnDMS.Core;

namespace TH.MongoRnDMS.App
{
    public interface ITreeService : IBaseService
    {
        Task SaveAsync(Tree entity);
        Task UpdateAsync(Tree entity);
        Task DeleteAsync(Tree entity);
        Task<Tree> FindByIdAsync(long id);
        Task<IEnumerable<Tree>> GetAsync(TreeFilterModel filter);
    }
}