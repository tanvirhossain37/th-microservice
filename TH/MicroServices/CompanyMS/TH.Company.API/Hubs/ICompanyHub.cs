using TH.CompanyMS.App;

namespace TH.CompanyMS.API.Hubs;

public interface ICompanyHub
{
    public Task BroadcastOnSaveCompanyAsync(CompanyViewModel viewModel);
    public Task BroadcastOnUpdateCompanyAsync(CompanyViewModel viewModel);
}