using Microsoft.AspNetCore.SignalR;
using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public class SpaceSubscriptionHub : Hub<ISpaceSubscriptionHub>
{
    public async Task BroadcastOnSaveSpaceSubscriptionAsync(SpaceSubscriptionViewModel viewModel)
    {
        await Clients.All.BroadcastOnSaveSpaceSubscriptionAsync(viewModel);
    }

    public async Task BroadcastOnUpdateSpaceSubscriptionAsync(SpaceSubscriptionViewModel viewModel)
    {
        await Clients.All.BroadcastOnUpdateSpaceSubscriptionAsync(viewModel);
    }

    public async Task BroadcastOnSoftDeleteSpaceSubscriptionAsync(SpaceSubscriptionViewModel viewModel)
    {
        await Clients.All.BroadcastOnSoftDeleteSpaceSubscriptionAsync(viewModel);
    }

    public async Task BroadcastOnDeleteSpaceSubscriptionAsync(SpaceSubscriptionViewModel viewModel)
    {
        await Clients.All.BroadcastOnDeleteSpaceSubscriptionAsync(viewModel);
    }
}