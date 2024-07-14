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
public class UserRoleController : CustomBaseController
{
    private readonly IUserRoleService _userRoleService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;

    public UserRoleController(IUserRoleService userRoleService, IMapper mapper, IServiceScopeFactory scopeFactory, HttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _userRoleService = userRoleService ?? throw new ArgumentNullException(nameof(userRoleService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }

    [HttpPost("SaveUserRoleAsync")]
    [ProducesResponseType(typeof(UserRoleViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "WritePolicy")]
    public async Task<IActionResult> SaveUserRoleAsync([FromBody] UserRoleInputModel model)
    {
        var entity = await _userRoleService.SaveAsync(_mapper.Map<UserRoleInputModel, UserRole>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<UserRole, UserRoleFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<IUserRoleService>();
            var viewModel = _mapper.Map<UserRole, UserRoleViewModel>(await service.FindAsync(filter, DataFilter));
            return CustomResult(Lang.Find("success"), viewModel);
        }
    }

    [HttpPost("UpdateUserRoleAsync")]
    [ProducesResponseType(typeof(UserRoleViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UpdatePolicy")]
    public async Task<IActionResult> UpdateUserRoleAsync([FromBody] UserRoleInputModel model)
    {
        var entity = await _userRoleService.UpdateAsync(_mapper.Map<UserRoleInputModel, UserRole>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<UserRole, UserRoleFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<IUserRoleService>();
            var viewModel = _mapper.Map<UserRole, UserRoleViewModel>(await service.FindAsync(filter, DataFilter));
            return CustomResult(Lang.Find("success"), viewModel);
        }
    }

    [HttpPost("SoftDeleteUserRoleAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "SoftDeletePolicy")]
    public async Task<IActionResult> SoftDeleteUserRoleAsync([FromBody] UserRoleInputModel model)
    {
        var hasDeleted = await _userRoleService.SoftDeleteAsync(_mapper.Map<UserRoleInputModel, UserRole>(model), DataFilter);

        return CustomResult(Lang.Find("success"), hasDeleted);
    }

    [HttpPost("DeleteUserRoleAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "DeletePolicy")]
    public async Task<IActionResult> DeleteUserRoleAsync([FromBody] UserRoleInputModel model)
    {
        var hasDeleted = await _userRoleService.DeleteAsync(_mapper.Map<UserRoleInputModel, UserRole>(model), DataFilter);

        return CustomResult(Lang.Find("success"), hasDeleted);
    }

    [HttpPost("FindUserRoleAsync")]
    [ProducesResponseType(typeof(UserRoleViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ReadPolicy")]
    public async Task<IActionResult> FindUserRoleAsync([FromBody] UserRoleFilterModel filter)
    {
        var entity = await _userRoleService.FindAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<UserRole, UserRoleViewModel>(entity));
    }

    [HttpPost("GetUserRolesAsync")]
    [ProducesResponseType(typeof(List<UserRoleViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ReadPolicy")]
    public async Task<IActionResult> GetUserRolesAsync([FromBody] UserRoleFilterModel filter)
    {
        var entities = await _userRoleService.GetAsync(filter, DataFilter);
        if (entities is null) return CustomResult(Lang.Find("error_not_found"), entities, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<List<UserRole>, List<UserRoleViewModel>>(entities.ToList()));
    }

    public override void Dispose()
    {
        _userRoleService?.Dispose();
    }
}