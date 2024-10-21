using TH.CompanyMS.App;

namespace TH.CompanyMS.API;

public interface IBranchUserHub
{
    public Task BroadcastOnSaveBranchUserAsync(BranchUserViewModel viewModel);
    public Task BroadcastOnUpdateBranchUserAsync(BranchUserViewModel viewModel);
    public Task BroadcastOnArchiveBranchUserAsync(BranchUserViewModel viewModel);
    public Task BroadcastOnDeleteBranchUserAsync(BranchUserViewModel viewModel);
}