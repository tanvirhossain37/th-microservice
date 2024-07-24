using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public interface ICompanyHub
{
    public Task BroadcastOnSaveCompanyAsync(CompanyViewModel viewModel);
    public Task BroadcastOnUpdateCompanyAsync(CompanyViewModel viewModel);
    public Task BroadcastOnSoftDeleteCompanyAsync(CompanyInputModel inputModel);
    public Task BroadcastOnDeleteCompanyAsync(CompanyInputModel inputModel);
}