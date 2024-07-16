using Microsoft.AspNetCore.SignalR;
using TH.CompanyMS.App;

namespace TH.CompanyMS.API.Hubs;

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
}