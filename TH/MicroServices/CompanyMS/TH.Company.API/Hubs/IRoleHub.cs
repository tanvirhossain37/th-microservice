using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public interface IRoleHub
{
    public Task BroadcastOnSaveRoleAsync(RoleViewModel viewModel);
    public Task BroadcastOnUpdateRoleAsync(RoleViewModel viewModel);
    public Task BroadcastOnSoftDeleteRoleAsync(RoleInputModel inputModel);
    public Task BroadcastOnDeleteRoleAsync(RoleInputModel inputModel);
}