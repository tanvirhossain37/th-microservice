using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public interface IUserHub
{
    public Task BroadcastOnSaveUserAsync(UserViewModel viewModel);
    public Task BroadcastOnUpdateUserAsync(UserViewModel viewModel);
    public Task BroadcastOnSoftDeleteUserAsync(UserViewModel viewModel);
    public Task BroadcastOnDeleteUserAsync(UserViewModel viewModel);
}