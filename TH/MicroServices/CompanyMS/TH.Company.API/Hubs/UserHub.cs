using Microsoft.AspNetCore.SignalR;
using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public class UserHub : Hub<IUserHub>
{
    public async Task BroadcastOnSaveUserAsync(UserViewModel viewModel)
    {
        await Clients.All.BroadcastOnSaveUserAsync(viewModel);
    }

    public async Task BroadcastOnUpdateUserAsync(UserViewModel viewModel)
    {
        await Clients.All.BroadcastOnUpdateUserAsync(viewModel);
    }

    public async Task BroadcastOnSoftDeleteUserAsync(UserViewModel viewModel)
    {
        await Clients.All.BroadcastOnSoftDeleteUserAsync(viewModel);
    }

    public async Task BroadcastOnDeleteUserAsync(UserViewModel viewModel)
    {
        await Clients.All.BroadcastOnDeleteUserAsync(viewModel);
    }
}