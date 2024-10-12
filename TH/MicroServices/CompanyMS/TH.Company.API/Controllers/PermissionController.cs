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
public class PermissionController : CustomBaseController
{
    private readonly IPermissionService _permissionService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHubContext<PermissionHub, IPermissionHub> _hubContext;

    public PermissionController(IPermissionService permissionService, IMapper mapper, IServiceScopeFactory scopeFactory, HttpContextAccessor httpContextAccessor, IHubContext<PermissionHub, IPermissionHub> hubContext) : base(httpContextAccessor)
    {
        _permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));

        _permissionService.SetUserResolver(UserResolver);
    }

    [HttpPost("SavePermissionAsync")]
    [ProducesResponseType(typeof(PermissionViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "PermissionWritePolicy")]
    public async Task<IActionResult> SavePermissionAsync([FromBody] PermissionInputModel model)
    {
        var entity = await _permissionService.SaveAsync(_mapper.Map<PermissionInputModel, Permission>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = new PermissionFilterModel { Id = entity.Id };
            var service = scope.ServiceProvider.GetRequiredService<IPermissionService>();
            var viewModel = _mapper.Map<Permission, PermissionViewModel>(await service.FindByIdAsync(filter, DataFilter));

            await _hubContext.Clients.All.BroadcastOnSavePermissionAsync(viewModel);

            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("UpdatePermissionAsync")]
    [ProducesResponseType(typeof(PermissionViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "PermissionUpdatePolicy")]
    public async Task<IActionResult> UpdatePermissionAsync([FromBody] PermissionInputModel model)
    {
        var entity = await _permissionService.UpdateAsync(_mapper.Map<PermissionInputModel, Permission>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = new PermissionFilterModel { Id = entity.Id };
            var service = scope.ServiceProvider.GetRequiredService<IPermissionService>();
            var viewModel = _mapper.Map<Permission, PermissionViewModel>(await service.FindByIdAsync(filter, DataFilter));

            await _hubContext.Clients.All.BroadcastOnUpdatePermissionAsync(viewModel);

            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("SoftDeletePermissionAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "PermissionSoftDeletePolicy")]
    public async Task<IActionResult> SoftDeletePermissionAsync([FromBody] PermissionInputModel model)
    {
        //first grab it
        var filter = new PermissionFilterModel { Id = model.Id };
        var viewModel = _mapper.Map<Permission, PermissionViewModel>(await _permissionService.FindByIdAsync(filter, DataFilter));

        //then soft delete
        await _permissionService.SoftDeleteAsync(_mapper.Map<PermissionInputModel, Permission>(model), DataFilter);

        await _hubContext.Clients.All.BroadcastOnSoftDeletePermissionAsync(viewModel);

        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("DeletePermissionAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "PermissionDeletePolicy")]
    public async Task<IActionResult> DeletePermissionAsync([FromBody] PermissionInputModel model)
    {
        //first grab it
        var filter = new PermissionFilterModel { Id = model.Id };
        var viewModel = _mapper.Map<Permission, PermissionViewModel>(await _permissionService.FindByIdAsync(filter, DataFilter));

        //then delete
        await _permissionService.DeleteAsync(_mapper.Map<PermissionInputModel, Permission>(model), DataFilter);

        await _hubContext.Clients.All.BroadcastOnDeletePermissionAsync(viewModel);

        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("FindPermissionAsync")]
    [ProducesResponseType(typeof(PermissionViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "PermissionReadPolicy")]
    public async Task<IActionResult> FindPermissionAsync([FromBody] PermissionFilterModel filter)
    {
        var entity = await _permissionService.FindByIdAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<Permission, PermissionViewModel>(entity));
    }

    [HttpPost("GetPermissionsAsync")]
    [ProducesResponseType(typeof(List<PermissionViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "PermissionReadPolicy")]
    public async Task<IActionResult> GetPermissionsAsync([FromBody] PermissionFilterModel filter)
    {
        var entities = await _permissionService.GetAsync(filter, DataFilter);
        if (entities is null) return CustomResult(Lang.Find("error_not_found"), entities, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<List<Permission>, List<PermissionViewModel>>(entities.ToList()));
    }

    public override void Dispose()
    {
        _permissionService?.Dispose();
    }
}