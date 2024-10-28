using Microsoft.AspNetCore.SignalR;
using TH.AddressMS.App;

namespace TH.AddressMS.API;

public class CountryHub : Hub<ICountryHub>
{
    public async Task BroadcastOnSaveCountryAsync(CountryViewModel viewModel)
    {
        await Clients.All.BroadcastOnSaveCountryAsync(viewModel);
    }

    public async Task BroadcastOnUpdateCountryAsync(CountryViewModel viewModel)
    {
        await Clients.All.BroadcastOnUpdateCountryAsync(viewModel);
    }

    public async Task BroadcastOnArchiveCountryAsync(CountryViewModel viewModel)
    {
        await Clients.All.BroadcastOnArchiveCountryAsync(viewModel);
    }

    public async Task BroadcastOnDeleteCountryAsync(CountryViewModel viewModel)
    {
        await Clients.All.BroadcastOnDeleteCountryAsync(viewModel);
    }
}