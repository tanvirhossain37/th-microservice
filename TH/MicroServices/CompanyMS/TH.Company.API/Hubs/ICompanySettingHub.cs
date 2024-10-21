using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public interface ICompanySettingHub
{
    public Task BroadcastOnSaveCompanySettingAsync(CompanySettingViewModel viewModel);
    public Task BroadcastOnUpdateCompanySettingAsync(CompanySettingViewModel viewModel);
    public Task BroadcastOnArchiveCompanySettingAsync(CompanySettingViewModel viewModel);
    public Task BroadcastOnDeleteCompanySettingAsync(CompanySettingViewModel viewModel);
}