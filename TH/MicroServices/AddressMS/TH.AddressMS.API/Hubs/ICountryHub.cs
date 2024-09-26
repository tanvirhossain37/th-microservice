using TH.AddressMS.App;

namespace TH.AddressMS.API;

public interface ICountryHub
{
    public Task BroadcastOnSaveCountryAsync(CountryViewModel viewModel);
    public Task BroadcastOnUpdateCountryAsync(CountryViewModel viewModel);
    public Task BroadcastOnSoftDeleteCountryAsync(CountryInputModel inputModel);
    public Task BroadcastOnDeleteCountryAsync(CountryInputModel inputModel);
}