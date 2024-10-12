using TH.CompanyMS.Core;
using TH.Common.Model;

namespace TH.CompanyMS.App;

public interface ISpaceSubscriptionService : IBaseService
{
    Task<SpaceSubscription> SaveAsync(SpaceSubscription entity, DataFilter dataFilter, bool commit = true);
    Task<SpaceSubscription> UpdateAsync(SpaceSubscription entity, DataFilter dataFilter, bool commit = true);
    Task<bool> SoftDeleteAsync(SpaceSubscription entity, DataFilter dataFilter, bool commit = true);
    Task<bool> DeleteAsync(SpaceSubscription entity, DataFilter dataFilter, bool commit = true);
    Task<SpaceSubscription> FindByIdAsync(SpaceSubscriptionFilterModel filter, DataFilter dataFilter);
    Task<SpaceSubscription> FindBySpaceIdAsync(SpaceSubscriptionFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<SpaceSubscription>> GetAsync(SpaceSubscriptionFilterModel filter, DataFilter dataFilter);
}