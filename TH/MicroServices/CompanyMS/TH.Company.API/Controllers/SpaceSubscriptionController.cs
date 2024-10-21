using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TH.Common.Lang;
using TH.Common.Model;
using TH.CompanyMS.App;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.API;

[Authorize(Policy = "ClaimBasedPolicy")]
public class SpaceSubscriptionController : CustomBaseController
{
    private readonly ISpaceSubscriptionService _spaceSubscriptionService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHubContext<SpaceSubscriptionHub, ISpaceSubscriptionHub> _hubContext;

    public SpaceSubscriptionController(ISpaceSubscriptionService spaceSubscriptionService, IMapper mapper, IServiceScopeFactory scopeFactory, HttpContextAccessor httpContextAccessor, IHubContext<SpaceSubscriptionHub, ISpaceSubscriptionHub> hubContext) : base(httpContextAccessor)
    {
        _spaceSubscriptionService = spaceSubscriptionService ?? throw new ArgumentNullException(nameof(spaceSubscriptionService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));

        _spaceSubscriptionService.SetUserResolver(UserResolver);
    }

    [HttpPost("SaveSpaceSubscriptionAsync")]
    [ProducesResponseType(typeof(SpaceSubscriptionViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "SpaceSubscriptionWritePolicy")]
    public async Task<IActionResult> SaveSpaceSubscriptionAsync([FromBody] SpaceSubscriptionInputModel model)
    {
        var entity = await _spaceSubscriptionService.SaveAsync(_mapper.Map<SpaceSubscriptionInputModel, SpaceSubscription>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = new SpaceSubscriptionFilterModel { Id = entity.Id };
            var service = scope.ServiceProvider.GetRequiredService<ISpaceSubscriptionService>();
            var viewModel = _mapper.Map<SpaceSubscription, SpaceSubscriptionViewModel>(await service.FindByIdAsync(filter, DataFilter));

            await _hubContext.Clients.All.BroadcastOnSaveSpaceSubscriptionAsync(viewModel);

            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("UpdateSpaceSubscriptionAsync")]
    [ProducesResponseType(typeof(SpaceSubscriptionViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "SpaceSubscriptionUpdatePolicy")]
    public async Task<IActionResult> UpdateSpaceSubscriptionAsync([FromBody] SpaceSubscriptionInputModel model)
    {
        var entity = await _spaceSubscriptionService.UpdateAsync(_mapper.Map<SpaceSubscriptionInputModel, SpaceSubscription>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = new SpaceSubscriptionFilterModel { Id = entity.Id };
            var service = scope.ServiceProvider.GetRequiredService<ISpaceSubscriptionService>();
            var viewModel = _mapper.Map<SpaceSubscription, SpaceSubscriptionViewModel>(await service.FindByIdAsync(filter, DataFilter));

            await _hubContext.Clients.All.BroadcastOnUpdateSpaceSubscriptionAsync(viewModel);

            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("ArchiveSpaceSubscriptionAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "SpaceSubscriptionArchivePolicy")]
    public async Task<IActionResult> ArchiveSpaceSubscriptionAsync([FromBody] SpaceSubscriptionInputModel model)
    {
        //first grab it
        var filter = new SpaceSubscriptionFilterModel { Id = model.Id };
        var viewModel = _mapper.Map<SpaceSubscription, SpaceSubscriptionViewModel>(await _spaceSubscriptionService.FindByIdAsync(filter, DataFilter));

        //then archive
        await _spaceSubscriptionService.ArchiveAsync(_mapper.Map<SpaceSubscriptionInputModel, SpaceSubscription>(model), DataFilter);

        await _hubContext.Clients.All.BroadcastOnArchiveSpaceSubscriptionAsync(viewModel);

        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("DeleteSpaceSubscriptionAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "SpaceSubscriptionDeletePolicy")]
    public async Task<IActionResult> DeleteSpaceSubscriptionAsync([FromBody] SpaceSubscriptionInputModel model)
    {
        //first grab it
        var filter = new SpaceSubscriptionFilterModel { Id = model.Id };
        var viewModel = _mapper.Map<SpaceSubscription, SpaceSubscriptionViewModel>(await _spaceSubscriptionService.FindByIdAsync(filter, DataFilter));

        //then delete
        await _spaceSubscriptionService.DeleteAsync(_mapper.Map<SpaceSubscriptionInputModel, SpaceSubscription>(model), DataFilter);

        await _hubContext.Clients.All.BroadcastOnDeleteSpaceSubscriptionAsync(viewModel);

        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("FindSpaceSubscriptionAsync")]
    [ProducesResponseType(typeof(SpaceSubscriptionViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "SpaceSubscriptionReadPolicy")]
    public async Task<IActionResult> FindSpaceSubscriptionAsync([FromBody] SpaceSubscriptionFilterModel filter)
    {
        var entity = await _spaceSubscriptionService.FindByIdAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<SpaceSubscription, SpaceSubscriptionViewModel>(entity));
    }

    [HttpPost("GetSpaceSubscriptionsAsync")]
    [ProducesResponseType(typeof(List<SpaceSubscriptionViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "SpaceSubscriptionReadPolicy")]
    public async Task<IActionResult> GetSpaceSubscriptionsAsync([FromBody] SpaceSubscriptionFilterModel filter)
    {
        var entities = await _spaceSubscriptionService.GetAsync(filter, DataFilter);
        if (entities is null) return CustomResult(Lang.Find("error_not_found"), entities, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<List<SpaceSubscription>, List<SpaceSubscriptionViewModel>>(entities.ToList()));
    }

    public override void Dispose()
    {
        _spaceSubscriptionService?.Dispose();
    }
}