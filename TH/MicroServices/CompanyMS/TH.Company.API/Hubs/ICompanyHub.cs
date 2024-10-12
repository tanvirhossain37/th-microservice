using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public interface ICompanyHub
{
    public Task BroadcastOnSaveCompanyAsync(CompanyViewModel viewModel);
    public Task BroadcastOnUpdateCompanyAsync(CompanyViewModel viewModel);
    public Task BroadcastOnSoftDeleteCompanyAsync(CompanyViewModel viewModel);
    public Task BroadcastOnDeleteCompanyAsync(CompanyViewModel viewModel);
    public Task BroadcastOnSaveUserCompanyAsync(UserCompanyViewModel viewModel);
    public Task BroadcastOnUpdateUserCompanyAsync(UserCompanyViewModel viewModel);
    public Task BroadcastOnSoftDeleteUserCompanyAsync(UserCompanyViewModel viewModel);
    public Task BroadcastOnDeleteUserCompanyAsync(UserCompanyViewModel viewModel);
}