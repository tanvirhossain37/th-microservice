using Microsoft.AspNetCore.SignalR;
using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public class UserCompanyHub : Hub<IUserCompanyHub>
{
    public async Task BroadcastOnSaveUserCompanyAsync(UserCompanyViewModel viewModel)
    {
        await Clients.All.BroadcastOnSaveUserCompanyAsync(viewModel);
    }

    public async Task BroadcastOnUpdateUserCompanyAsync(UserCompanyViewModel viewModel)
    {
        await Clients.All.BroadcastOnUpdateUserCompanyAsync(viewModel);
    }

    public async Task BroadcastOnArchiveUserCompanyAsync(UserCompanyViewModel viewModel)
    {
        await Clients.All.BroadcastOnArchiveUserCompanyAsync(viewModel);
    }

    public async Task BroadcastOnDeleteUserCompanyAsync(UserCompanyViewModel viewModel)
    {
        await Clients.All.BroadcastOnDeleteUserCompanyAsync(viewModel);
    }
}