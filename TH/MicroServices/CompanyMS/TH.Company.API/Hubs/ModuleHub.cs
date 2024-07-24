using Microsoft.AspNetCore.SignalR;
using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public class ModuleHub : Hub<IModuleHub>
{
    public async Task BroadcastOnSaveModuleAsync(ModuleViewModel viewModel)
    {
        await Clients.All.BroadcastOnSaveModuleAsync(viewModel);
    }

    public async Task BroadcastOnUpdateModuleAsync(ModuleViewModel viewModel)
    {
        await Clients.All.BroadcastOnUpdateModuleAsync(viewModel);
    }

    public async Task BroadcastOnSoftDeleteModuleAsync(ModuleInputModel inputModel)
    {
        await Clients.All.BroadcastOnSoftDeleteModuleAsync(inputModel);
    }

    public async Task BroadcastOnDeleteModuleAsync(ModuleInputModel inputModel)
    {
        await Clients.All.BroadcastOnDeleteModuleAsync(inputModel);
    }
}