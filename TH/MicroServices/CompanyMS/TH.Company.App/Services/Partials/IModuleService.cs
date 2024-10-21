using TH.Common.Model;

namespace TH.CompanyMS.App
{
    public partial interface IModuleService
    {
        Task InitAsync(DataFilter dataFilter);
    }
}