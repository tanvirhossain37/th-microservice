using Microsoft.AspNetCore.SignalR;
using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public class BranchUserHub : Hub<IBranchUserHub>
{
    public async Task BroadcastOnSaveBranchUserAsync(BranchUserViewModel viewModel)
    {
        await Clients.All.BroadcastOnSaveBranchUserAsync(viewModel);
    }

    public async Task BroadcastOnUpdateBranchUserAsync(BranchUserViewModel viewModel)
    {
        await Clients.All.BroadcastOnUpdateBranchUserAsync(viewModel);
    }

    public async Task BroadcastOnArchiveBranchUserAsync(BranchUserViewModel viewModel)
    {
        await Clients.All.BroadcastOnArchiveBranchUserAsync(viewModel);
    }

    public async Task BroadcastOnDeleteBranchUserAsync(BranchUserViewModel viewModel)
    {
        await Clients.All.BroadcastOnDeleteBranchUserAsync(viewModel);
    }
}