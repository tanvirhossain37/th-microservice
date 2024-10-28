using TH.AddressMS.App;

namespace TH.AddressMS.API;

public interface IAddressHub
{
    public Task BroadcastOnSaveAddressAsync(AddressViewModel viewModel);
    public Task BroadcastOnUpdateAddressAsync(AddressViewModel viewModel);
    public Task BroadcastOnArchiveAddressAsync(AddressViewModel viewModel);
    public Task BroadcastOnDeleteAddressAsync(AddressViewModel viewModel);
}