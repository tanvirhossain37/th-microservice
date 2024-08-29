using TH.Common.Model;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public interface IUserCompanyService : IBaseService
{
    Task<UserCompany> SaveAsync(UserCompany entity, DataFilter dataFilter, bool commit = true);
    Task<UserCompany> UpdateAsync(UserCompany entity, DataFilter dataFilter, bool commit = true);
    Task<bool> SoftDeleteAsync(UserCompany entity, DataFilter dataFilter, bool commit = true);
    Task<bool> DeleteAsync(UserCompany entity, DataFilter dataFilter, bool commit = true);
    Task<UserCompany> FindByIdAsync(UserCompanyFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<UserCompany>> GetAsync(UserCompanyFilterModel filter, DataFilter dataFilter);
}