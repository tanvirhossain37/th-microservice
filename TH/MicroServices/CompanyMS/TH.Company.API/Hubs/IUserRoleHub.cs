using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public interface IUserRoleHub
{
    public Task BroadcastOnSaveUserRoleAsync(UserRoleViewModel viewModel);
    public Task BroadcastOnUpdateUserRoleAsync(UserRoleViewModel viewModel);
    public Task BroadcastOnSoftDeleteUserRoleAsync(UserRoleViewModel viewModel);
    public Task BroadcastOnDeleteUserRoleAsync(UserRoleViewModel viewModel);
}