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
public class UserController : CustomBaseController
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHubContext<UserHub, IUserHub> _hubContext;

    public UserController(IUserService userService, IMapper mapper, IServiceScopeFactory scopeFactory, HttpContextAccessor httpContextAccessor, IHubContext<UserHub, IUserHub> hubContext) : base(httpContextAccessor)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));

        _userService.SetUserResolver(UserResolver);
    }

    [HttpPost("SaveUserAsync")]
    [ProducesResponseType(typeof(UserViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UserWritePolicy")]
    public async Task<IActionResult> SaveUserAsync([FromBody] UserInputModel model)
    {
        var entity = await _userService.SaveAsync(_mapper.Map<UserInputModel, User>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<User, UserFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<IUserService>();
            var viewModel = _mapper.Map<User, UserViewModel>(await service.FindAsync(filter, DataFilter));

            _hubContext.Clients.All.BroadcastOnSaveUserAsync(viewModel);
            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("UpdateUserAsync")]
    [ProducesResponseType(typeof(UserViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UserUpdatePolicy")]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UserInputModel model)
    {
        var entity = await _userService.UpdateAsync(_mapper.Map<UserInputModel, User>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<User, UserFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<IUserService>();
            var viewModel = _mapper.Map<User, UserViewModel>(await service.FindAsync(filter, DataFilter));

            _hubContext.Clients.All.BroadcastOnUpdateUserAsync(viewModel);
            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("SoftDeleteUserAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UserSoftDeletePolicy")]
    public async Task<IActionResult> SoftDeleteUserAsync([FromBody] UserInputModel model)
    {
        await _userService.SoftDeleteAsync(_mapper.Map<UserInputModel, User>(model), DataFilter);

        _hubContext.Clients.All.BroadcastOnSoftDeleteUserAsync(model);
        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("DeleteUserAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UserDeletePolicy")]
    public async Task<IActionResult> DeleteUserAsync([FromBody] UserInputModel model)
    {
        await _userService.DeleteAsync(_mapper.Map<UserInputModel, User>(model), DataFilter);

        _hubContext.Clients.All.BroadcastOnDeleteUserAsync(model);
        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("FindUserAsync")]
    [ProducesResponseType(typeof(UserViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UserReadPolicy")]
    public async Task<IActionResult> FindUserAsync([FromBody] UserFilterModel filter)
    {
        var entity = await _userService.FindAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<User, UserViewModel>(entity));
    }

    [HttpPost("GetUsersAsync")]
    [ProducesResponseType(typeof(List<UserViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UserReadPolicy")]
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