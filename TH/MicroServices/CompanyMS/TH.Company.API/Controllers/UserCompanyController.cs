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
public class UserCompanyController : CustomBaseController
{
    private readonly IUserCompanyService _userCompanyService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHubContext<UserCompanyHub, IUserCompanyHub> _hubContext;

    public UserCompanyController(IUserCompanyService userCompanyService, IMapper mapper, IServiceScopeFactory scopeFactory, HttpContextAccessor httpContextAccessor, IHubContext<UserCompanyHub, IUserCompanyHub> hubContext) : base(httpContextAccessor)
    {
        _userCompanyService = userCompanyService ?? throw new ArgumentNullException(nameof(userCompanyService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));

        _userCompanyService.SetUserResolver(UserResolver);
    }

    [HttpPost("SaveUserCompanyAsync")]
    [ProducesResponseType(typeof(UserCompanyViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UserCompanyWritePolicy")]
    public async Task<IActionResult> SaveUserCompanyAsync([FromBody] UserCompanyInputModel model)
    {
        var entity = await _userCompanyService.SaveAsync(_mapper.Map<UserCompanyInputModel, UserCompany>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = new UserCompanyFilterModel { Id = entity.Id };
            var service = scope.ServiceProvider.GetRequiredService<IUserCompanyService>();
            var viewModel = _mapper.Map<UserCompany, UserCompanyViewModel>(await service.FindByIdAsync(filter, DataFilter));

            await _hubContext.Clients.All.BroadcastOnSaveUserCompanyAsync(viewModel);

            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("UpdateUserCompanyAsync")]
    [ProducesResponseType(typeof(UserCompanyViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UserCompanyUpdatePolicy")]
    public async Task<IActionResult> UpdateUserCompanyAsync([FromBody] UserCompanyInputModel model)
    {
        var entity = await _userCompanyService.UpdateAsync(_mapper.Map<UserCompanyInputModel, UserCompany>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = new UserCompanyFilterModel { Id = entity.Id };
            var service = scope.ServiceProvider.GetRequiredService<IUserCompanyService>();
            var viewModel = _mapper.Map<UserCompany, UserCompanyViewModel>(await service.FindByIdAsync(filter, DataFilter));

            await _hubContext.Clients.All.BroadcastOnUpdateUserCompanyAsync(viewModel);

            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("ArchiveUserCompanyAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UserCompanyArchivePolicy")]
    public async Task<IActionResult> ArchiveUserCompanyAsync([FromBody] UserCompanyInputModel model)
    {
        //first grab it
        var filter = new UserCompanyFilterModel { Id = model.Id };
        var viewModel = _mapper.Map<UserCompany, UserCompanyViewModel>(await _userCompanyService.FindByIdAsync(filter, DataFilter));

        //then archive
        await _userCompanyService.ArchiveAsync(_mapper.Map<UserCompanyInputModel, UserCompany>(model), DataFilter);

        await _hubContext.Clients.All.BroadcastOnArchiveUserCompanyAsync(viewModel);

        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("DeleteUserCompanyAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UserCompanyDeletePolicy")]
    public async Task<IActionResult> DeleteUserCompanyAsync([FromBody] UserCompanyInputModel model)
    {
        //first grab it
        var filter = new UserCompanyFilterModel { Id = model.Id };
        var viewModel = _mapper.Map<UserCompany, UserCompanyViewModel>(await _userCompanyService.FindByIdAsync(filter, DataFilter));

        //then delete
        await _userCompanyService.DeleteAsync(_mapper.Map<UserCompanyInputModel, UserCompany>(model), DataFilter);

        await _hubContext.Clients.All.BroadcastOnDeleteUserCompanyAsync(viewModel);

        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("FindUserCompanyAsync")]
    [ProducesResponseType(typeof(UserCompanyViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UserCompanyReadPolicy")]
    public async Task<IActionResult> FindUserCompanyAsync([FromBody] UserCompanyFilterModel filter)
    {
        var entity = await _userCompanyService.FindByIdAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<UserCompany, UserCompanyViewModel>(entity));
    }

    [HttpPost("GetUserCompaniesAsync")]
    [ProducesResponseType(typeof(List<UserCompanyViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UserCompanyReadPolicy")]
    public async Task<IActionResult> GetUserCompaniesAsync([FromBody] UserCompanyFilterModel filter)
    {
        var entities = await _userCompanyService.GetAsync(filter, DataFilter);
        if (entities is null) return CustomResult(Lang.Find("error_not_found"), entities, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<List<UserCompany>, List<UserCompanyViewModel>>(entities.ToList()));
    }

    public override void Dispose()
    {
        _userCompanyService?.Dispose();
    }
}