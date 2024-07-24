using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public interface IUserHub
{
    public Task BroadcastOnSaveUserAsync(UserViewModel viewModel);
    public Task BroadcastOnUpdateUserAsync(UserViewModel viewModel);
    public Task BroadcastOnSoftDeleteUserAsync(UserInputModel inputModel);
    public Task BroadcastOnDeleteUserAsync(UserInputModel inputModel);
}