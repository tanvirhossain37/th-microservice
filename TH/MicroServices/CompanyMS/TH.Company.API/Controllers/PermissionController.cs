using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TH.Common.Lang;
using TH.Common.Model;
using TH.CompanyMS.App;
using TH.CompanyMS.Core;

namespace TH.CompanyMS;

[Authorize(Policy = "ClaimBasedPolicy")]
public class PermissionController : CustomBaseController
{
    private readonly IPermissionService _permissionService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;

    public PermissionController(IPermissionService permissionService, IMapper mapper, IServiceScopeFactory scopeFactory, HttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }

    [HttpPost("SavePermissionAsync")]
    [ProducesResponseType(typeof(PermissionViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "WritePolicy")]
    public async Task<IActionResult> SavePermissionAsync([FromBody] PermissionInputModel model)
    {
        var entity = await _permissionService.SaveAsync(_mapper.Map<PermissionInputModel, Permission>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<Permission, PermissionFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<IPermissionService>();
            var viewModel = _mapper.Map<Permission, PermissionViewModel>(await service.FindAsync(filter, DataFilter));
            return CustomResult(Lang.Find("success"), viewModel);
        }
    }

    [HttpPost("UpdatePermissionAsync")]
    [ProducesResponseType(typeof(PermissionViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UpdatePolicy")]
    public async Task<IActionResult> UpdatePermissionAsync([FromBody] PermissionInputModel model)
    {
        var entity = await _permissionService.UpdateAsync(_mapper.Map<PermissionInputModel, Permission>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<Permission, PermissionFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<IPermissionService>();
            var viewModel = _mapper.Map<Permission, PermissionViewModel>(await service.FindAsync(filter, DataFilter));
            return CustomResult(Lang.Find("success"), viewModel);
        }
    }

    [HttpPost("SoftDeletePermissionAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "SoftDeletePolicy")]
    public async Task<IActionResult> SoftDeletePermissionAsync([FromBody] PermissionInputModel model)
    {
        var hasDeleted = await _permissionService.SoftDeleteAsync(_mapper.Map<PermissionInputModel, Permission>(model), DataFilter);

        return CustomResult(Lang.Find("success"), hasDeleted);
    }

    [HttpPost("DeletePermissionAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "DeletePolicy")]
    public async Task<IActionResult> DeletePermissionAsync([FromBody] PermissionInputModel model)
    {
        var hasDeleted = await _permissionService.DeleteAsync(_mapper.Map<PermissionInputModel, Permission>(model), DataFilter);

        return CustomResult(Lang.Find("success"), hasDeleted);
    }

    [HttpPost("FindPermissionAsync")]
    [ProducesResponseType(typeof(PermissionViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ReadPolicy")]
    public async Task<IActionResult> FindPermissionAsync([FromBody] PermissionFilterModel filter)
    {
        var entity = await _permissionService.FindAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<Permission, PermissionViewModel>(entity));
    }

    [HttpPost("GetPermissionsAsync")]
    [ProducesResponseType(typeof(List<PermissionViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ReadPolicy")]
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