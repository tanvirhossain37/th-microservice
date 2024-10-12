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

    public async Task BroadcastOnSoftDeleteRoleAsync(RoleViewModel viewModel)
    {
        await Clients.All.BroadcastOnSoftDeleteRoleAsync(viewModel);
    }

    public async Task BroadcastOnDeleteRoleAsync(RoleViewModel viewModel)
    {
        await Clients.All.BroadcastOnDeleteRoleAsync(viewModel);
    }
}