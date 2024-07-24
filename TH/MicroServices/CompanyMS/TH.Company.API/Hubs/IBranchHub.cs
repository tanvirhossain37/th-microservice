using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public interface IBranchHub
{
    public Task BroadcastOnSaveBranchAsync(BranchViewModel viewModel);
    public Task BroadcastOnUpdateBranchAsync(BranchViewModel viewModel);
    public Task BroadcastOnSoftDeleteBranchAsync(BranchInputModel inputModel);
    public Task BroadcastOnDeleteBranchAsync(BranchInputModel inputModel);
}