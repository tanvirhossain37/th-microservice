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

    public async Task BroadcastOnSoftDeleteUserAsync(UserInputModel inputModel)
    {
        await Clients.All.BroadcastOnSoftDeleteUserAsync(inputModel);
    }

    public async Task BroadcastOnDeleteUserAsync(UserInputModel inputModel)
    {
        await Clients.All.BroadcastOnDeleteUserAsync(inputModel);
    }
}