using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public interface IUserCompanyHub
{
    public Task BroadcastOnSaveUserCompanyAsync(UserCompanyViewModel viewModel);
    public Task BroadcastOnUpdateUserCompanyAsync(UserCompanyViewModel viewModel);
    public Task BroadcastOnSoftDeleteUserCompanyAsync(UserCompanyViewModel viewModel);
    public Task BroadcastOnDeleteUserCompanyAsync(UserCompanyViewModel viewModel);
}