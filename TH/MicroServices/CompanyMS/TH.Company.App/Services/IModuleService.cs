using TH.CompanyMS.Core;
using TH.Common.Model;

namespace TH.CompanyMS.App;

public partial interface IModuleService : IBaseService
{
    Task<Module> SaveAsync(Module entity, DataFilter dataFilter, bool commit = true);
    Task<Module> UpdateAsync(Module entity, DataFilter dataFilter, bool commit = true);
    Task<bool> ArchiveAsync(Module entity, DataFilter dataFilter, bool commit = true);
    Task<bool> DeleteAsync(Module entity, DataFilter dataFilter, bool commit = true);
    Task<Module> FindByIdAsync(ModuleFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<Module>> GetAsync(ModuleFilterModel filter, DataFilter dataFilter);
}