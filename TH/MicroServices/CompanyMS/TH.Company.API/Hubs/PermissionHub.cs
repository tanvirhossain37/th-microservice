using Microsoft.AspNetCore.SignalR;
using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public class PermissionHub : Hub<IPermissionHub>
{
    public async Task BroadcastOnSavePermissionAsync(PermissionViewModel viewModel)
    {
        await Clients.All.BroadcastOnSavePermissionAsync(viewModel);
    }

    public async Task BroadcastOnUpdatePermissionAsync(PermissionViewModel viewModel)
    {
        await Clients.All.BroadcastOnUpdatePermissionAsync(viewModel);
    }

    public async Task BroadcastOnArchivePermissionAsync(PermissionViewModel viewModel)
    {
        await Clients.All.BroadcastOnArchivePermissionAsync(viewModel);
    }

    public async Task BroadcastOnDeletePermissionAsync(PermissionViewModel viewModel)
    {
        await Clients.All.BroadcastOnDeletePermissionAsync(viewModel);
    }
}