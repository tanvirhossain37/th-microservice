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

    public async Task BroadcastOnSoftDeleteBranchUserAsync(BranchUserInputModel inputModel)
    {
        await Clients.All.BroadcastOnSoftDeleteBranchUserAsync(inputModel);
    }

    public async Task BroadcastOnDeleteBranchUserAsync(BranchUserInputModel inputModel)
    {
        await Clients.All.BroadcastOnDeleteBranchUserAsync(inputModel);
    }
}