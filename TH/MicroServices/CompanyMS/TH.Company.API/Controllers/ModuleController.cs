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
public class ModuleController : CustomBaseController
{
    private readonly IModuleService _moduleService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHubContext<ModuleHub, IModuleHub> _hubContext;

    public ModuleController(IModuleService moduleService, IMapper mapper, IServiceScopeFactory scopeFactory, HttpContextAccessor httpContextAccessor, IHubContext<ModuleHub, IModuleHub> hubContext) : base(httpContextAccessor)
    {
        _moduleService = moduleService ?? throw new ArgumentNullException(nameof(moduleService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));

        _moduleService.SetUserResolver(UserResolver);
    }

    [HttpPost("SaveModuleAsync")]
    [ProducesResponseType(typeof(ModuleViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ModuleWritePolicy")]
    public async Task<IActionResult> SaveModuleAsync([FromBody] ModuleInputModel model)
    {
        var entity = await _moduleService.SaveAsync(_mapper.Map<ModuleInputModel, Module>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<Module, ModuleFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<IModuleService>();
            var viewModel = _mapper.Map<Module, ModuleViewModel>(await service.FindByIdAsync(filter, DataFilter));

            _hubContext.Clients.All.BroadcastOnSaveModuleAsync(viewModel);
            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("UpdateModuleAsync")]
    [ProducesResponseType(typeof(ModuleViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ModuleUpdatePolicy")]
    public async Task<IActionResult> UpdateModuleAsync([FromBody] ModuleInputModel model)
    {
        var entity = await _moduleService.UpdateAsync(_mapper.Map<ModuleInputModel, Module>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<Module, ModuleFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<IModuleService>();
            var viewModel = _mapper.Map<Module, ModuleViewModel>(await service.FindByIdAsync(filter, DataFilter));

            _hubContext.Clients.All.BroadcastOnUpdateModuleAsync(viewModel);
            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("SoftDeleteModuleAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ModuleSoftDeletePolicy")]
    public async Task<IActionResult> SoftDeleteModuleAsync([FromBody] ModuleInputModel model)
    {
        await _moduleService.SoftDeleteAsync(_mapper.Map<ModuleInputModel, Module>(model), DataFilter);

        _hubContext.Clients.All.BroadcastOnSoftDeleteModuleAsync(model);
        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("DeleteModuleAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ModuleDeletePolicy")]
    public async Task<IActionResult> DeleteModuleAsync([FromBody] ModuleInputModel model)
    {
        await _moduleService.DeleteAsync(_mapper.Map<ModuleInputModel, Module>(model), DataFilter);

        _hubContext.Clients.All.BroadcastOnDeleteModuleAsync(model);
        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("FindModuleAsync")]
    [ProducesResponseType(typeof(ModuleViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ModuleReadPolicy")]
    public async Task<IActionResult> FindModuleAsync([FromBody] ModuleFilterModel filter)
    {
        var entity = await _moduleService.FindByIdAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<Module, ModuleViewModel>(entity));
    }

    [HttpPost("GetModulesAsync")]
    [ProducesResponseType(typeof(List<ModuleViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ModuleReadPolicy")]
    public async Task<IActionResult> GetModulesAsync([FromBody] ModuleFilterModel filter)
    {
        var entities = await _moduleService.GetAsync(filter, DataFilter);
        if (entities is null) return CustomResult(Lang.Find("error_not_found"), entities, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<List<Module>, List<ModuleViewModel>>(entities.ToList()));
    }

    public override void Dispose()
    {
        _moduleService?.Dispose();
    }
}