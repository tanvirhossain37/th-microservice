using Microsoft.AspNetCore.SignalR;
using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public class BranchHub : Hub<IBranchHub>
{
    public async Task BroadcastOnSaveBranchAsync(BranchViewModel viewModel)
    {
        await Clients.All.BroadcastOnSaveBranchAsync(viewModel);
    }

    public async Task BroadcastOnUpdateBranchAsync(BranchViewModel viewModel)
    {
        await Clients.All.BroadcastOnUpdateBranchAsync(viewModel);
    }

    public async Task BroadcastOnArchiveBranchAsync(BranchViewModel viewModel)
    {
        await Clients.All.BroadcastOnArchiveBranchAsync(viewModel);
    }

    public async Task BroadcastOnDeleteBranchAsync(BranchViewModel viewModel)
    {
        await Clients.All.BroadcastOnDeleteBranchAsync(viewModel);
    }
}