using Microsoft.AspNetCore.SignalR;
using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public class UserRoleHub : Hub<IUserRoleHub>
{
    public async Task BroadcastOnSaveUserRoleAsync(UserRoleViewModel viewModel)
    {
        await Clients.All.BroadcastOnSaveUserRoleAsync(viewModel);
    }

    public async Task BroadcastOnUpdateUserRoleAsync(UserRoleViewModel viewModel)
    {
        await Clients.All.BroadcastOnUpdateUserRoleAsync(viewModel);
    }

    public async Task BroadcastOnSoftDeleteUserRoleAsync(UserRoleInputModel inputModel)
    {
        await Clients.All.BroadcastOnSoftDeleteUserRoleAsync(inputModel);
    }

    public async Task BroadcastOnDeleteUserRoleAsync(UserRoleInputModel inputModel)
    {
        await Clients.All.BroadcastOnDeleteUserRoleAsync(inputModel);
    }
}