using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public interface IModuleHub
{
    public Task BroadcastOnSaveModuleAsync(ModuleViewModel viewModel);
    public Task BroadcastOnUpdateModuleAsync(ModuleViewModel viewModel);
    public Task BroadcastOnSoftDeleteModuleAsync(ModuleInputModel inputModel);
    public Task BroadcastOnDeleteModuleAsync(ModuleInputModel inputModel);
}