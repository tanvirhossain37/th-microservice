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

    public async Task BroadcastOnSoftDeleteCountryAsync(CountryInputModel inputModel)
    {
        await Clients.All.BroadcastOnSoftDeleteCountryAsync(inputModel);
    }

    public async Task BroadcastOnDeleteCountryAsync(CountryInputModel inputModel)
    {
        await Clients.All.BroadcastOnDeleteCountryAsync(inputModel);
    }
}