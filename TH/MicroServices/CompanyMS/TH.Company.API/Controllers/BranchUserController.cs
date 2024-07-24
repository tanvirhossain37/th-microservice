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
public class BranchUserController : CustomBaseController
{
    private readonly IBranchUserService _branchUserService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHubContext<BranchUserHub, IBranchUserHub> _hubContext;

    public BranchUserController(IBranchUserService branchUserService, IMapper mapper, IServiceScopeFactory scopeFactory, HttpContextAccessor httpContextAccessor, IHubContext<BranchUserHub, IBranchUserHub> hubContext) : base(httpContextAccessor)
    {
        _branchUserService = branchUserService ?? throw new ArgumentNullException(nameof(branchUserService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));

        _branchUserService.SetUserResolver(UserResolver);
    }

    [HttpPost("SaveBranchUserAsync")]
    [ProducesResponseType(typeof(BranchUserViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "BranchUserWritePolicy")]
    public async Task<IActionResult> SaveBranchUserAsync([FromBody] BranchUserInputModel model)
    {
        var entity = await _branchUserService.SaveAsync(_mapper.Map<BranchUserInputModel, BranchUser>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<BranchUser, BranchUserFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<IBranchUserService>();
            var viewModel = _mapper.Map<BranchUser, BranchUserViewModel>(await service.FindAsync(filter, DataFilter));

            _hubContext.Clients.All.BroadcastOnSaveBranchUserAsync(viewModel);
            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("UpdateBranchUserAsync")]
    [ProducesResponseType(typeof(BranchUserViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "BranchUserUpdatePolicy")]
    public async Task<IActionResult> UpdateBranchUserAsync([FromBody] BranchUserInputModel model)
    {
        var entity = await _branchUserService.UpdateAsync(_mapper.Map<BranchUserInputModel, BranchUser>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<BranchUser, BranchUserFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<IBranchUserService>();
            var viewModel = _mapper.Map<BranchUser, BranchUserViewModel>(await service.FindAsync(filter, DataFilter));

            _hubContext.Clients.All.BroadcastOnUpdateBranchUserAsync(viewModel);
            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("SoftDeleteBranchUserAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "BranchUserSoftDeletePolicy")]
    public async Task<IActionResult> SoftDeleteBranchUserAsync([FromBody] BranchUserInputModel model)
    {
        await _branchUserService.SoftDeleteAsync(_mapper.Map<BranchUserInputModel, BranchUser>(model), DataFilter);

        _hubContext.Clients.All.BroadcastOnSoftDeleteBranchUserAsync(model);
        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("DeleteBranchUserAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "BranchUserDeletePolicy")]
    public async Task<IActionResult> DeleteBranchUserAsync([FromBody] BranchUserInputModel model)
    {
        await _branchUserService.DeleteAsync(_mapper.Map<BranchUserInputModel, BranchUser>(model), DataFilter);

        _hubContext.Clients.All.BroadcastOnDeleteBranchUserAsync(model);
        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("FindBranchUserAsync")]
    [ProducesResponseType(typeof(BranchUserViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "BranchUserReadPolicy")]
    public async Task<IActionResult> FindBranchUserAsync([FromBody] BranchUserFilterModel filter)
    {
        var entity = await _branchUserService.FindAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<BranchUser, BranchUserViewModel>(entity));
    }

    [HttpPost("GetBranchUsersAsync")]
    [ProducesResponseType(typeof(List<BranchUserViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "BranchUserReadPolicy")]
    public async Task<IActionResult> GetBranchUsersAsync([FromBody] BranchUserFilterModel filter)
    {
        var entities = await _branchUserService.GetAsync(filter, DataFilter);
        if (entities is null) return CustomResult(Lang.Find("error_not_found"), entities, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<List<BranchUser>, List<BranchUserViewModel>>(entities.ToList()));
    }

    public override void Dispose()
    {
        _branchUserService?.Dispose();
    }
}