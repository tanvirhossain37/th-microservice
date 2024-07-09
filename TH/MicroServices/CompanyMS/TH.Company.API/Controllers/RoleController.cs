using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TH.AddressMS.Core;
using TH.Common.Lang;
using TH.Common.Model;
using TH.CompanyMS.App;
using TH.CompanyMS.Core;

namespace TH.CompanyMS;

[Authorize(Policy = "ClaimBasedPolicy")]
public class RoleController : CustomBaseController
{
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;

    public RoleController(IRoleService roleService, IMapper mapper, IServiceScopeFactory scopeFactory)
    {
        _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }

    [HttpPost("SaveRoleAsync")]
    [ProducesResponseType(typeof(RoleViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "WritePolicy")]
    public async Task<IActionResult> SaveRoleAsync([FromBody] RoleInputModel model)
    {
        var entity = await _roleService.SaveAsync(_mapper.Map<RoleInputModel, Role>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<Role, RoleFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<IRoleService>();
            var viewModel = _mapper.Map<Role, RoleViewModel>(await service.FindAsync(filter, DataFilter));
            return CustomResult(Lang.Find("success"), viewModel);
        }
    }

    [HttpPost("UpdateRoleAsync")]
    [ProducesResponseType(typeof(RoleViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UpdatePolicy")]
    public async Task<IActionResult> UpdateRoleAsync([FromBody] RoleInputModel model)
    {
        var entity = await _roleService.UpdateAsync(_mapper.Map<RoleInputModel, Role>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<Role, RoleFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<IRoleService>();
            var viewModel = _mapper.Map<Role, RoleViewModel>(await service.FindAsync(filter, DataFilter));
            return CustomResult(Lang.Find("success"), viewModel);
        }
    }

    [HttpPost("SoftDeleteRoleAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "SoftDeletePolicy")]
    public async Task<IActionResult> SoftDeleteRoleAsync([FromBody] RoleInputModel model)
    {
        var hasDeleted = await _roleService.SoftDeleteAsync(_mapper.Map<RoleInputModel, Role>(model), DataFilter);

        return CustomResult(Lang.Find("success"), hasDeleted);
    }

    [HttpPost("DeleteRoleAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "DeletePolicy")]
    public async Task<IActionResult> DeleteRoleAsync([FromBody] RoleInputModel model)
    {
        var hasDeleted = await _roleService.DeleteAsync(_mapper.Map<RoleInputModel, Role>(model), DataFilter);

        return CustomResult(Lang.Find("success"), hasDeleted);
    }

    [HttpPost("FindRoleAsync")]
    [ProducesResponseType(typeof(RoleViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ReadPolicy")]
    public async Task<IActionResult> FindRoleAsync([FromBody] RoleFilterModel filter)
    {
        var entity = await _roleService.FindAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<Role, RoleViewModel>(entity));
    }

    [HttpPost("GetRolesAsync")]
    [ProducesResponseType(typeof(List<RoleViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ReadPolicy")]
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