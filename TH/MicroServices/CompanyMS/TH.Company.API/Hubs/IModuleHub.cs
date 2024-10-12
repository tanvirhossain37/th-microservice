using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public interface IModuleHub
{
    public Task BroadcastOnSaveModuleAsync(ModuleViewModel viewModel);
    public Task BroadcastOnUpdateModuleAsync(ModuleViewModel viewModel);
    public Task BroadcastOnSoftDeleteModuleAsync(ModuleViewModel viewModel);
    public Task BroadcastOnDeleteModuleAsync(ModuleViewModel viewModel);
}