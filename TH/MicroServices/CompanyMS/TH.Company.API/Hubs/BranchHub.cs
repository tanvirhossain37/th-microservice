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

    public async Task BroadcastOnSoftDeleteBranchAsync(BranchInputModel inputModel)
    {
        await Clients.All.BroadcastOnSoftDeleteBranchAsync(inputModel);
    }

    public async Task BroadcastOnDeleteBranchAsync(BranchInputModel inputModel)
    {
        await Clients.All.BroadcastOnDeleteBranchAsync(inputModel);
    }
}