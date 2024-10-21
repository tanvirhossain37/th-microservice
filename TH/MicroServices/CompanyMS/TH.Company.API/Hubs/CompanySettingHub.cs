using Microsoft.AspNetCore.SignalR;
using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public class CompanySettingHub : Hub<ICompanySettingHub>
{
    public async Task BroadcastOnSaveCompanySettingAsync(CompanySettingViewModel viewModel)
    {
        await Clients.All.BroadcastOnSaveCompanySettingAsync(viewModel);
    }

    public async Task BroadcastOnUpdateCompanySettingAsync(CompanySettingViewModel viewModel)
    {
        await Clients.All.BroadcastOnUpdateCompanySettingAsync(viewModel);
    }

    public async Task BroadcastOnArchiveCompanySettingAsync(CompanySettingViewModel viewModel)
    {
        await Clients.All.BroadcastOnArchiveCompanySettingAsync(viewModel);
    }

    public async Task BroadcastOnDeleteCompanySettingAsync(CompanySettingViewModel viewModel)
    {
        await Clients.All.BroadcastOnDeleteCompanySettingAsync(viewModel);
    }
}