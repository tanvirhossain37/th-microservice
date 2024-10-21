using TH.Common.Model;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App
{
    public partial interface IUserCompanyService
    {
        //todo
        Task<UserCompany> FindByCompanyIdAsync(UserCompanyFilterModel filter, DataFilter dataFilter);
    }
}