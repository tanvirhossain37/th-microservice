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
public class UserController : CustomBaseController
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;

    public UserController(IUserService userService, IMapper mapper, IServiceScopeFactory scopeFactory, HttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }

    [HttpPost("SaveUserAsync")]
    [ProducesResponseType(typeof(UserViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "WritePolicy")]
    public async Task<IActionResult> SaveUserAsync([FromBody] UserInputModel model)
    {
        var entity = await _userService.SaveAsync(_mapper.Map<UserInputModel, User>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<User, UserFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<IUserService>();
            var viewModel = _mapper.Map<User, UserViewModel>(await service.FindAsync(filter, DataFilter));
            return CustomResult(Lang.Find("success"), viewModel);
        }
    }

    [HttpPost("UpdateUserAsync")]
    [ProducesResponseType(typeof(UserViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UpdatePolicy")]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UserInputModel model)
    {
        var entity = await _userService.UpdateAsync(_mapper.Map<UserInputModel, User>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<User, UserFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<IUserService>();
            var viewModel = _mapper.Map<User, UserViewModel>(await service.FindAsync(filter, DataFilter));
            return CustomResult(Lang.Find("success"), viewModel);
        }
    }

    [HttpPost("SoftDeleteUserAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "SoftDeletePolicy")]
    public async Task<IActionResult> SoftDeleteUserAsync([FromBody] UserInputModel model)
    {
        var hasDeleted = await _userService.SoftDeleteAsync(_mapper.Map<UserInputModel, User>(model), DataFilter);

        return CustomResult(Lang.Find("success"), hasDeleted);
    }

    [HttpPost("DeleteUserAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "DeletePolicy")]
    public async Task<IActionResult> DeleteUserAsync([FromBody] UserInputModel model)
    {
        var hasDeleted = await _userService.DeleteAsync(_mapper.Map<UserInputModel, User>(model), DataFilter);

        return CustomResult(Lang.Find("success"), hasDeleted);
    }

    [HttpPost("FindUserAsync")]
    [ProducesResponseType(typeof(UserViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ReadPolicy")]
    public async Task<IActionResult> FindUserAsync([FromBody] UserFilterModel filter)
    {
        var entity = await _userService.FindAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<User, UserViewModel>(entity));
    }

    [HttpPost("GetUsersAsync")]
    [ProducesResponseType(typeof(List<UserViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ReadPolicy")]
    public async Task<IActionResult> GetUsersAsync([FromBody] UserFilterModel filter)
    {
        var entities = await _userService.GetAsync(filter, DataFilter);
        if (entities is null) return CustomResult(Lang.Find("error_not_found"), entities, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<List<User>, List<UserViewModel>>(entities.ToList()));
    }

    public override void Dispose()
    {
        _userService?.Dispose();
    }
}