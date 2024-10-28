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
public class AddressController : CustomBaseController
{
    private readonly IAddressService _addressService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHubContext<AddressHub, IAddressHub> _hubContext;

    public AddressController(IAddressService addressService, IMapper mapper, IServiceScopeFactory scopeFactory, HttpContextAccessor httpContextAccessor, IHubContext<AddressHub, IAddressHub> hubContext) : base(httpContextAccessor)
    {
        _addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));

        _addressService.SetUserResolver(UserResolver);
    }

    [HttpPost("SaveAddressAsync")]
    [ProducesResponseType(typeof(AddressViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "AddressWritePolicy")]
    public async Task<IActionResult> SaveAddressAsync([FromBody] AddressInputModel model)
    {
        var entity = await _addressService.SaveAsync(_mapper.Map<AddressInputModel, Address>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = new AddressFilterModel { Id = entity.Id };
            var service = scope.ServiceProvider.GetRequiredService<IAddressService>();
            var viewModel = _mapper.Map<Address, AddressViewModel>(await service.FindByIdAsync(filter, DataFilter));

            await _hubContext.Clients.All.BroadcastOnSaveAddressAsync(viewModel);

            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("UpdateAddressAsync")]
    [ProducesResponseType(typeof(AddressViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "AddressUpdatePolicy")]
    public async Task<IActionResult> UpdateAddressAsync([FromBody] AddressInputModel model)
    {
        var entity = await _addressService.UpdateAsync(_mapper.Map<AddressInputModel, Address>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = new AddressFilterModel { Id = entity.Id };
            var service = scope.ServiceProvider.GetRequiredService<IAddressService>();
            var viewModel = _mapper.Map<Address, AddressViewModel>(await service.FindByIdAsync(filter, DataFilter));

            await _hubContext.Clients.All.BroadcastOnUpdateAddressAsync(viewModel);

            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("ArchiveAddressAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "AddressArchivePolicy")]
    public async Task<IActionResult> ArchiveAddressAsync([FromBody] AddressInputModel model)
    {
        //first grab it
        var filter = new AddressFilterModel { Id = model.Id };
        var viewModel = _mapper.Map<Address, AddressViewModel>(await _addressService.FindByIdAsync(filter, DataFilter));

        //then archive
        await _addressService.ArchiveAsync(_mapper.Map<AddressInputModel, Address>(model), DataFilter);

        await _hubContext.Clients.All.BroadcastOnArchiveAddressAsync(viewModel);

        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("DeleteAddressAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "AddressDeletePolicy")]
    public async Task<IActionResult> DeleteAddressAsync([FromBody] AddressInputModel model)
    {
        //first grab it
        var filter = new AddressFilterModel { Id = model.Id };
        var viewModel = _mapper.Map<Address, AddressViewModel>(await _addressService.FindByIdAsync(filter, DataFilter));

        //then delete
        await _addressService.DeleteAsync(_mapper.Map<AddressInputModel, Address>(model), DataFilter);

        await _hubContext.Clients.All.BroadcastOnDeleteAddressAsync(viewModel);

        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("FindAddressAsync")]
    [ProducesResponseType(typeof(AddressViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "AddressReadPolicy")]
    public async Task<IActionResult> FindAddressAsync([FromBody] AddressFilterModel filter)
    {
        var entity = await _addressService.FindByIdAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<Address, AddressViewModel>(entity));
    }

    [HttpPost("GetAddressesAsync")]
    [ProducesResponseType(typeof(List<AddressViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "AddressReadPolicy")]
    public async Task<IActionResult> GetAddressesAsync([FromBody] AddressFilterModel filter)
    {
        var entities = await _addressService.GetAsync(filter, DataFilter);
        if (entities is null) return CustomResult(Lang.Find("error_not_found"), entities, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<List<Address>, List<AddressViewModel>>(entities.ToList()));
    }

    public override void Dispose()
    {
        _addressService?.Dispose();
    }
}