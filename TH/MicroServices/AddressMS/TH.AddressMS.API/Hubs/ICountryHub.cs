using TH.AddressMS.App;

namespace TH.AddressMS.API;

public interface ICountryHub
{
    public Task BroadcastOnSaveCountryAsync(CountryViewModel viewModel);
    public Task BroadcastOnUpdateCountryAsync(CountryViewModel viewModel);
    public Task BroadcastOnArchiveCountryAsync(CountryViewModel viewModel);
    public Task BroadcastOnDeleteCountryAsync(CountryViewModel viewModel);
}