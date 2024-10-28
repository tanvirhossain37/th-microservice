using Microsoft.AspNetCore.SignalR;
using TH.AddressMS.App;

namespace TH.AddressMS.API;

public class AddressHub : Hub<IAddressHub>
{
    public async Task BroadcastOnSaveAddressAsync(AddressViewModel viewModel)
    {
        await Clients.All.BroadcastOnSaveAddressAsync(viewModel);
    }

    public async Task BroadcastOnUpdateAddressAsync(AddressViewModel viewModel)
    {
        await Clients.All.BroadcastOnUpdateAddressAsync(viewModel);
    }

    public async Task BroadcastOnArchiveAddressAsync(AddressViewModel viewModel)
    {
        await Clients.All.BroadcastOnArchiveAddressAsync(viewModel);
    }

    public async Task BroadcastOnDeleteAddressAsync(AddressViewModel viewModel)
    {
        await Clients.All.BroadcastOnDeleteAddressAsync(viewModel);
    }
}