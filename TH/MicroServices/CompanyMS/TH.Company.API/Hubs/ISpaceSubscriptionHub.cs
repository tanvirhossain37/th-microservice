using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public interface ISpaceSubscriptionHub
{
    public Task BroadcastOnSaveSpaceSubscriptionAsync(SpaceSubscriptionViewModel viewModel);
    public Task BroadcastOnUpdateSpaceSubscriptionAsync(SpaceSubscriptionViewModel viewModel);
    public Task BroadcastOnSoftDeleteSpaceSubscriptionAsync(SpaceSubscriptionViewModel viewModel);
    public Task BroadcastOnDeleteSpaceSubscriptionAsync(SpaceSubscriptionViewModel viewModel);
}