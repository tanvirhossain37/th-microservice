using TH.Common.Model;
using TH.ShadowMS.Core;

namespace TH.ShadowMS.App;

public interface IShadowService : IDisposable
{
    Task<Shadow> SaveAsync(Shadow entity, DataFilter dataFilter, bool commit = true);
    Task<Shadow> UpdateAsync(Shadow entity, DataFilter dataFilter, bool commit = true);
    Task<bool> SoftDeleteAsync(Shadow entity, DataFilter dataFilter, bool commit = true);
    Task<bool> DeleteAsync(Shadow entity, DataFilter dataFilter, bool commit = true);
    Task<Shadow> FindAsync(ShadowFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<Shadow>> GetAsync(ShadowFilterModel filter, DataFilter dataFilter);
}