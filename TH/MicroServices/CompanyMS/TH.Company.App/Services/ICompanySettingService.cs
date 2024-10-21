using TH.CompanyMS.Core;
using TH.Common.Model;

namespace TH.CompanyMS.App;

public partial interface ICompanySettingService : IBaseService
{
    Task<CompanySetting> SaveAsync(CompanySetting entity, DataFilter dataFilter, bool commit = true);
    Task<CompanySetting> UpdateAsync(CompanySetting entity, DataFilter dataFilter, bool commit = true);
    Task<bool> ArchiveAsync(CompanySetting entity, DataFilter dataFilter, bool commit = true);
    Task<bool> DeleteAsync(CompanySetting entity, DataFilter dataFilter, bool commit = true);
    Task<CompanySetting> FindByIdAsync(CompanySettingFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<CompanySetting>> GetAsync(CompanySettingFilterModel filter, DataFilter dataFilter);
}