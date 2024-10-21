using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public interface ICompanyHub
{
    public Task BroadcastOnSaveCompanyAsync(CompanyViewModel viewModel);
    public Task BroadcastOnUpdateCompanyAsync(CompanyViewModel viewModel);
    public Task BroadcastOnArchiveCompanyAsync(CompanyViewModel viewModel);
    public Task BroadcastOnDeleteCompanyAsync(CompanyViewModel viewModel);
}