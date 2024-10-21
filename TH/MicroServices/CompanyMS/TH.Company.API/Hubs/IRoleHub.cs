using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public interface IRoleHub
{
    public Task BroadcastOnSaveRoleAsync(RoleViewModel viewModel);
    public Task BroadcastOnUpdateRoleAsync(RoleViewModel viewModel);
    public Task BroadcastOnArchiveRoleAsync(RoleViewModel viewModel);
    public Task BroadcastOnDeleteRoleAsync(RoleViewModel viewModel);
}