using TH.Common.Model;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public interface IModuleService : IBaseService
{
    Task<Module> SaveAsync(Module entity, DataFilter dataFilter, bool commit = true);
    Task<Module> UpdateAsync(Module entity, DataFilter dataFilter, bool commit = true);
    Task DeleteAsync(Module entity, DataFilter dataFilter, bool commit = true);
    Task<Module> FindByIdAsync(ModuleFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<Module>> GetAsync(ModuleFilterModel filter, DataFilter dataFilter);
}