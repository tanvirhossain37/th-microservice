using TH.Common.Model;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public partial interface IUserRepo
{
    Task<User> FindByUserNameAsync(string spaceId, string companyId, string userName, DataFilter dataFilter);
    Task<User> FindByUserNameExceptMeAsync(string id, string spaceId, string companyId, string userName, DataFilter dataFilter);
}