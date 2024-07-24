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

    public async Task BroadcastOnSoftDeletePermissionAsync(PermissionInputModel inputModel)
    {
        await Clients.All.BroadcastOnSoftDeletePermissionAsync(inputModel);
    }

    public async Task BroadcastOnDeletePermissionAsync(PermissionInputModel inputModel)
    {
        await Clients.All.BroadcastOnDeletePermissionAsync(inputModel);
    }
}