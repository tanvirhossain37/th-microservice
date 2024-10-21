using TH.Common.Model;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App
{
    public partial interface ISpaceSubscriptionService
    {
        //todo
        Task<SpaceSubscription> FindBySpaceIdAsync(SpaceSubscriptionFilterModel filter, DataFilter dataFilter);
}
}