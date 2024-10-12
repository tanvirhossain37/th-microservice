using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public interface IPermissionHub
{
    public Task BroadcastOnSavePermissionAsync(PermissionViewModel viewModel);
    public Task BroadcastOnUpdatePermissionAsync(PermissionViewModel viewModel);
    public Task BroadcastOnSoftDeletePermissionAsync(PermissionViewModel viewModel);
    public Task BroadcastOnDeletePermissionAsync(PermissionViewModel viewModel);
}