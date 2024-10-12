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
public class RoleController : CustomBaseController
{
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHubContext<RoleHub, IRoleHub> _hubContext;

    public RoleController(IRoleService roleService, IMapper mapper, IServiceScopeFactory scopeFactory, HttpContextAccessor httpContextAccessor, IHubContext<RoleHub, IRoleHub> hubContext) : base(httpContextAccessor)
    {
        _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));

        _roleService.SetUserResolver(UserResolver);
    }

    [HttpPost("SaveRoleAsync")]
    [ProducesResponseType(typeof(RoleViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "RoleWritePolicy")]
    public async Task<IActionResult> SaveRoleAsync([FromBody] RoleInputModel model)
    {
        var entity = await _roleService.SaveAsync(_mapper.Map<RoleInputModel, Role>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = new RoleFilterModel { Id = entity.Id };
            var service = scope.ServiceProvider.GetRequiredService<IRoleService>();
            var viewModel = _mapper.Map<Role, RoleViewModel>(await service.FindByIdAsync(filter, DataFilter));

            await _hubContext.Clients.All.BroadcastOnSaveRoleAsync(viewModel);

            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("UpdateRoleAsync")]
    [ProducesResponseType(typeof(RoleViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "RoleUpdatePolicy")]
    public async Task<IActionResult> UpdateRoleAsync([FromBody] RoleInputModel model)
    {
        var entity = await _roleService.UpdateAsync(_mapper.Map<RoleInputModel, Role>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = new RoleFilterModel { Id = entity.Id };
            var service = scope.ServiceProvider.GetRequiredService<IRoleService>();
            var viewModel = _mapper.Map<Role, RoleViewModel>(await service.FindByIdAsync(filter, DataFilter));

            await _hubContext.Clients.All.BroadcastOnUpdateRoleAsync(viewModel);

            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("SoftDeleteRoleAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "RoleSoftDeletePolicy")]
    public async Task<IActionResult> SoftDeleteRoleAsync([FromBody] RoleInputModel model)
    {
        //first grab it
        var filter = new RoleFilterModel { Id = model.Id };
        var viewModel = _mapper.Map<Role, RoleViewModel>(await _roleService.FindByIdAsync(filter, DataFilter));

        //then soft delete
        await _roleService.SoftDeleteAsync(_mapper.Map<RoleInputModel, Role>(model), DataFilter);

        await _hubContext.Clients.All.BroadcastOnSoftDeleteRoleAsync(viewModel);

        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("DeleteRoleAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "RoleDeletePolicy")]
    public async Task<IActionResult> DeleteRoleAsync([FromBody] RoleInputModel model)
    {
        //first grab it
        var filter = new RoleFilterModel { Id = model.Id };
        var viewModel = _mapper.Map<Role, RoleViewModel>(await _roleService.FindByIdAsync(filter, DataFilter));

        //then delete
        await _roleService.DeleteAsync(_mapper.Map<RoleInputModel, Role>(model), DataFilter);

        await _hubContext.Clients.All.BroadcastOnDeleteRoleAsync(viewModel);

        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("FindRoleAsync")]
    [ProducesResponseType(typeof(RoleViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "RoleReadPolicy")]
    public async Task<IActionResult> FindRoleAsync([FromBody] RoleFilterModel filter)
    {
        var entity = await _roleService.FindByIdAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<Role, RoleViewModel>(entity));
    }

    [HttpPost("GetRolesAsync")]
    [ProducesResponseType(typeof(List<RoleViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "RoleReadPolicy")]
    public async Task<IActionResult> GetRolesAsync([FromBody] RoleFilterModel filter)
    {
        var entities = await _roleService.GetAsync(filter, DataFilter);
        if (entities is null) return CustomResult(Lang.Find("error_not_found"), entities, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<List<Role>, List<RoleViewModel>>(entities.ToList()));
    }

    public override void Dispose()
    {
        _roleService?.Dispose();
    }
}