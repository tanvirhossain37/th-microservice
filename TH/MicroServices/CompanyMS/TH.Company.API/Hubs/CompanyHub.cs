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

    public async Task BroadcastOnSoftDeleteCompanyAsync(CompanyViewModel viewModel)
    {
        await Clients.All.BroadcastOnSoftDeleteCompanyAsync(viewModel);
    }

    public async Task BroadcastOnDeleteCompanyAsync(CompanyViewModel viewModel)
    {
        await Clients.All.BroadcastOnDeleteCompanyAsync(viewModel);
    }

    public async Task BroadcastOnSaveUserCompanyAsync(UserCompanyViewModel viewModel)
    {
        await Clients.All.BroadcastOnSaveUserCompanyAsync(viewModel);
    }
    public async Task BroadcastOnUpdateUserCompanyAsync(UserCompanyViewModel viewModel)
    {
        await Clients.All.BroadcastOnUpdateUserCompanyAsync(viewModel);
    }

    public async Task BroadcastOnSoftDeleteUserCompanyAsync(UserCompanyViewModel viewModel)
    {
        await Clients.All.BroadcastOnSoftDeleteUserCompanyAsync(viewModel);
    }

    public async Task BroadcastOnDeleteUserCompanyAsync(UserCompanyViewModel viewModel)
    {
        await Clients.All.BroadcastOnDeleteUserCompanyAsync(viewModel);
    }
}