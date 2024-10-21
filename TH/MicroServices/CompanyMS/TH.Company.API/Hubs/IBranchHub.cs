using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public interface IBranchHub
{
    public Task BroadcastOnSaveBranchAsync(BranchViewModel viewModel);
    public Task BroadcastOnUpdateBranchAsync(BranchViewModel viewModel);
    public Task BroadcastOnArchiveBranchAsync(BranchViewModel viewModel);
    public Task BroadcastOnDeleteBranchAsync(BranchViewModel viewModel);
}