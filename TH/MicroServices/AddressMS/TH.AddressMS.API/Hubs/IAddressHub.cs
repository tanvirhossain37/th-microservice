using TH.AddressMS.App;

namespace TH.AddressMS.API;

public interface IAddressHub
{
    public Task BroadcastOnSaveAddressAsync(AddressViewModel viewModel);
    public Task BroadcastOnUpdateAddressAsync(AddressViewModel viewModel);
    public Task BroadcastOnSoftDeleteAddressAsync(AddressInputModel inputModel);
    public Task BroadcastOnDeleteAddressAsync(AddressInputModel inputModel);
}