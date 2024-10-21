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
public class BranchController : CustomBaseController
{
    private readonly IBranchService _branchService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHubContext<BranchHub, IBranchHub> _hubContext;

    public BranchController(IBranchService branchService, IMapper mapper, IServiceScopeFactory scopeFactory, HttpContextAccessor httpContextAccessor, IHubContext<BranchHub, IBranchHub> hubContext) : base(httpContextAccessor)
    {
        _branchService = branchService ?? throw new ArgumentNullException(nameof(branchService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));

        _branchService.SetUserResolver(UserResolver);
    }

    [HttpPost("SaveBranchAsync")]
    [ProducesResponseType(typeof(BranchViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "BranchWritePolicy")]
    public async Task<IActionResult> SaveBranchAsync([FromBody] BranchInputModel model)
    {
        var entity = await _branchService.SaveAsync(_mapper.Map<BranchInputModel, Branch>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = new BranchFilterModel { Id = entity.Id };
            var service = scope.ServiceProvider.GetRequiredService<IBranchService>();
            var viewModel = _mapper.Map<Branch, BranchViewModel>(await service.FindByIdAsync(filter, DataFilter));

            await _hubContext.Clients.All.BroadcastOnSaveBranchAsync(viewModel);

            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("UpdateBranchAsync")]
    [ProducesResponseType(typeof(BranchViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "BranchUpdatePolicy")]
    public async Task<IActionResult> UpdateBranchAsync([FromBody] BranchInputModel model)
    {
        var entity = await _branchService.UpdateAsync(_mapper.Map<BranchInputModel, Branch>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = new BranchFilterModel { Id = entity.Id };
            var service = scope.ServiceProvider.GetRequiredService<IBranchService>();
            var viewModel = _mapper.Map<Branch, BranchViewModel>(await service.FindByIdAsync(filter, DataFilter));

            await _hubContext.Clients.All.BroadcastOnUpdateBranchAsync(viewModel);

            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("ArchiveBranchAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "BranchArchivePolicy")]
    public async Task<IActionResult> ArchiveBranchAsync([FromBody] BranchInputModel model)
    {
        //first grab it
        var filter = new BranchFilterModel { Id = model.Id };
        var viewModel = _mapper.Map<Branch, BranchViewModel>(await _branchService.FindByIdAsync(filter, DataFilter));

        //then archive
        await _branchService.ArchiveAsync(_mapper.Map<BranchInputModel, Branch>(model), DataFilter);

        await _hubContext.Clients.All.BroadcastOnArchiveBranchAsync(viewModel);

        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("DeleteBranchAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "BranchDeletePolicy")]
    public async Task<IActionResult> DeleteBranchAsync([FromBody] BranchInputModel model)
    {
        //first grab it
        var filter = new BranchFilterModel { Id = model.Id };
        var viewModel = _mapper.Map<Branch, BranchViewModel>(await _branchService.FindByIdAsync(filter, DataFilter));

        //then delete
        await _branchService.DeleteAsync(_mapper.Map<BranchInputModel, Branch>(model), DataFilter);

        await _hubContext.Clients.All.BroadcastOnDeleteBranchAsync(viewModel);

        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("FindBranchAsync")]
    [ProducesResponseType(typeof(BranchViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "BranchReadPolicy")]
    public async Task<IActionResult> FindBranchAsync([FromBody] BranchFilterModel filter)
    {
        var entity = await _branchService.FindByIdAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<Branch, BranchViewModel>(entity));
    }

    [HttpPost("GetBranchesAsync")]
    [ProducesResponseType(typeof(List<BranchViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "BranchReadPolicy")]
    public async Task<IActionResult> GetBranchesAsync([FromBody] BranchFilterModel filter)
    {
        var entities = await _branchService.GetAsync(filter, DataFilter);
        if (entities is null) return CustomResult(Lang.Find("error_not_found"), entities, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<List<Branch>, List<BranchViewModel>>(entities.ToList()));
    }

    public override void Dispose()
    {
        _branchService?.Dispose();
    }
}