using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TH.Common.Lang;
using TH.Common.Model;
using TH.AddressMS.App;
using TH.AddressMS.Core;

namespace TH.AddressMS.API;

[Authorize(Policy = "ClaimBasedPolicy")]
public class CountryController : CustomBaseController
{
    private readonly ICountryService _countryService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHubContext<CountryHub, ICountryHub> _hubContext;

    public CountryController(ICountryService countryService, IMapper mapper, IServiceScopeFactory scopeFactory, HttpContextAccessor httpContextAccessor, IHubContext<CountryHub, ICountryHub> hubContext) : base(httpContextAccessor)
    {
        _countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));

        _countryService.SetUserResolver(UserResolver);
    }

    [HttpPost("SaveCountryAsync")]
    [ProducesResponseType(typeof(CountryViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "CountryWritePolicy")]
    public async Task<IActionResult> SaveCountryAsync([FromBody] CountryInputModel model)
    {
        var entity = await _countryService.SaveAsync(_mapper.Map<CountryInputModel, Country>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = new CountryFilterModel { Id = entity.Id };
            var service = scope.ServiceProvider.GetRequiredService<ICountryService>();
            var viewModel = _mapper.Map<Country, CountryViewModel>(await service.FindByIdAsync(filter, DataFilter));

            await _hubContext.Clients.All.BroadcastOnSaveCountryAsync(viewModel);

            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("UpdateCountryAsync")]
    [ProducesResponseType(typeof(CountryViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "CountryUpdatePolicy")]
    public async Task<IActionResult> UpdateCountryAsync([FromBody] CountryInputModel model)
    {
        var entity = await _countryService.UpdateAsync(_mapper.Map<CountryInputModel, Country>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = new CountryFilterModel { Id = entity.Id };
            var service = scope.ServiceProvider.GetRequiredService<ICountryService>();
            var viewModel = _mapper.Map<Country, CountryViewModel>(await service.FindByIdAsync(filter, DataFilter));

            await _hubContext.Clients.All.BroadcastOnUpdateCountryAsync(viewModel);

            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("ArchiveCountryAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "CountryArchivePolicy")]
    public async Task<IActionResult> ArchiveCountryAsync([FromBody] CountryInputModel model)
    {
        //first grab it
        var filter = new CountryFilterModel { Id = model.Id };
        var viewModel = _mapper.Map<Country, CountryViewModel>(await _countryService.FindByIdAsync(filter, DataFilter));

        //then archive
        await _countryService.ArchiveAsync(_mapper.Map<CountryInputModel, Country>(model), DataFilter);

        await _hubContext.Clients.All.BroadcastOnArchiveCountryAsync(viewModel);

        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("DeleteCountryAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "CountryDeletePolicy")]
    public async Task<IActionResult> DeleteCountryAsync([FromBody] CountryInputModel model)
    {
        //first grab it
        var filter = new CountryFilterModel { Id = model.Id };
        var viewModel = _mapper.Map<Country, CountryViewModel>(await _countryService.FindByIdAsync(filter, DataFilter));

        //then delete
        await _countryService.DeleteAsync(_mapper.Map<CountryInputModel, Country>(model), DataFilter);

        await _hubContext.Clients.All.BroadcastOnDeleteCountryAsync(viewModel);

        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("FindCountryAsync")]
    [ProducesResponseType(typeof(CountryViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "CountryReadPolicy")]
    public async Task<IActionResult> FindCountryAsync([FromBody] CountryFilterModel filter)
    {
        var entity = await _countryService.FindByIdAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<Country, CountryViewModel>(entity));
    }

    [HttpPost("GetCountriesAsync")]
    [ProducesResponseType(typeof(List<CountryViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "CountryReadPolicy")]
    public async Task<IActionResult> GetCountriesAsync([FromBody] CountryFilterModel filter)
    {
        var entities = await _countryService.GetAsync(filter, DataFilter);
        if (entities is null) return CustomResult(Lang.Find("error_not_found"), entities, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<List<Country>, List<CountryViewModel>>(entities.ToList()));
    }

    public override void Dispose()
    {
        _countryService?.Dispose();
    }
}