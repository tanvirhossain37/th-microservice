using Microsoft.AspNetCore.SignalR;
using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public class CompanyHub : Hub<ICompanyHub>
{
    public async Task BroadcastOnSaveCompanyAsync(CompanyViewModel viewModel)
    {
        await Clients.All.BroadcastOnSaveCompanyAsync(viewModel);
    }

    public async Task BroadcastOnUpdateCompanyAsync(CompanyViewModel viewModel)
    {
        await Clients.All.BroadcastOnUpdateCompanyAsync(viewModel);
    }

    public async Task BroadcastOnSoftDeleteCompanyAsync(CompanyInputModel inputModel)
    {
        await Clients.All.BroadcastOnSoftDeleteCompanyAsync(inputModel);
    }

    public async Task BroadcastOnDeleteCompanyAsync(CompanyInputModel inputModel)
    {
        await Clients.All.BroadcastOnDeleteCompanyAsync(inputModel);
    }
}