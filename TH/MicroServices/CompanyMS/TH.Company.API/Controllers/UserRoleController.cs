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
public class UserRoleController : CustomBaseController
{
    private readonly IUserRoleService _userRoleService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHubContext<UserRoleHub, IUserRoleHub> _hubContext;

    public UserRoleController(IUserRoleService userRoleService, IMapper mapper, IServiceScopeFactory scopeFactory, HttpContextAccessor httpContextAccessor, IHubContext<UserRoleHub, IUserRoleHub> hubContext) : base(httpContextAccessor)
    {
        _userRoleService = userRoleService ?? throw new ArgumentNullException(nameof(userRoleService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));

        _userRoleService.SetUserResolver(UserResolver);
    }

    [HttpPost("SaveUserRoleAsync")]
    [ProducesResponseType(typeof(UserRoleViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UserRoleWritePolicy")]
    public async Task<IActionResult> SaveUserRoleAsync([FromBody] UserRoleInputModel model)
    {
        var entity = await _userRoleService.SaveAsync(_mapper.Map<UserRoleInputModel, UserRole>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = new UserRoleFilterModel { Id = entity.Id };
            var service = scope.ServiceProvider.GetRequiredService<IUserRoleService>();
            var viewModel = _mapper.Map<UserRole, UserRoleViewModel>(await service.FindByIdAsync(filter, DataFilter));

            await _hubContext.Clients.All.BroadcastOnSaveUserRoleAsync(viewModel);

            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("UpdateUserRoleAsync")]
    [ProducesResponseType(typeof(UserRoleViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UserRoleUpdatePolicy")]
    public async Task<IActionResult> UpdateUserRoleAsync([FromBody] UserRoleInputModel model)
    {
        var entity = await _userRoleService.UpdateAsync(_mapper.Map<UserRoleInputModel, UserRole>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = new UserRoleFilterModel { Id = entity.Id };
            var service = scope.ServiceProvider.GetRequiredService<IUserRoleService>();
            var viewModel = _mapper.Map<UserRole, UserRoleViewModel>(await service.FindByIdAsync(filter, DataFilter));

            await _hubContext.Clients.All.BroadcastOnUpdateUserRoleAsync(viewModel);

            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("SoftDeleteUserRoleAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UserRoleSoftDeletePolicy")]
    public async Task<IActionResult> SoftDeleteUserRoleAsync([FromBody] UserRoleInputModel model)
    {
        //first grab it
        var filter = new UserRoleFilterModel { Id = model.Id };
        var viewModel = _mapper.Map<UserRole, UserRoleViewModel>(await _userRoleService.FindByIdAsync(filter, DataFilter));

        //then soft delete
        await _userRoleService.SoftDeleteAsync(_mapper.Map<UserRoleInputModel, UserRole>(model), DataFilter);

        await _hubContext.Clients.All.BroadcastOnSoftDeleteUserRoleAsync(viewModel);

        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("DeleteUserRoleAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UserRoleDeletePolicy")]
    public async Task<IActionResult> DeleteUserRoleAsync([FromBody] UserRoleInputModel model)
    {
        //first grab it
        var filter = new UserRoleFilterModel { Id = model.Id };
        var viewModel = _mapper.Map<UserRole, UserRoleViewModel>(await _userRoleService.FindByIdAsync(filter, DataFilter));

        //then delete
        await _userRoleService.DeleteAsync(_mapper.Map<UserRoleInputModel, UserRole>(model), DataFilter);

        await _hubContext.Clients.All.BroadcastOnDeleteUserRoleAsync(viewModel);

        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("FindUserRoleAsync")]
    [ProducesResponseType(typeof(UserRoleViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UserRoleReadPolicy")]
    public async Task<IActionResult> FindUserRoleAsync([FromBody] UserRoleFilterModel filter)
    {
        var entity = await _userRoleService.FindByIdAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<UserRole, UserRoleViewModel>(entity));
    }

    [HttpPost("GetUserRolesAsync")]
    [ProducesResponseType(typeof(List<UserRoleViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UserRoleReadPolicy")]
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