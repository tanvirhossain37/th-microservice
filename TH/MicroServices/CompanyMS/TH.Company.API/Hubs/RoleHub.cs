using Microsoft.AspNetCore.SignalR;
using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public class RoleHub : Hub<IRoleHub>
{
    public async Task BroadcastOnSaveRoleAsync(RoleViewModel viewModel)
    {
        await Clients.All.BroadcastOnSaveRoleAsync(viewModel);
    }

    public async Task BroadcastOnUpdateRoleAsync(RoleViewModel viewModel)
    {
        await Clients.All.BroadcastOnUpdateRoleAsync(viewModel);
    }

    public async Task BroadcastOnSoftDeleteRoleAsync(RoleInputModel inputModel)
    {
        await Clients.All.BroadcastOnSoftDeleteRoleAsync(inputModel);
    }

    public async Task BroadcastOnDeleteRoleAsync(RoleInputModel inputModel)
    {
        await Clients.All.BroadcastOnDeleteRoleAsync(inputModel);
    }
}