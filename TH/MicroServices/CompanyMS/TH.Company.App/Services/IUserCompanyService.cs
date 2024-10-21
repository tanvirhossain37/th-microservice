using TH.CompanyMS.Core;
using TH.Common.Model;

namespace TH.CompanyMS.App;

public partial interface IUserCompanyService : IBaseService
{
    Task<UserCompany> SaveAsync(UserCompany entity, DataFilter dataFilter, bool commit = true);
    Task<UserCompany> UpdateAsync(UserCompany entity, DataFilter dataFilter, bool commit = true);
    Task<bool> ArchiveAsync(UserCompany entity, DataFilter dataFilter, bool commit = true);
    Task<bool> DeleteAsync(UserCompany entity, DataFilter dataFilter, bool commit = true);
    Task<UserCompany> FindByIdAsync(UserCompanyFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<UserCompany>> GetAsync(UserCompanyFilterModel filter, DataFilter dataFilter);
}