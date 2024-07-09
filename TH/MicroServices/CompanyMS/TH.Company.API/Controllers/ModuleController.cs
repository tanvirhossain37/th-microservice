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
public class ModuleController : CustomBaseController
{
    private readonly IModuleService _moduleService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;

    public ModuleController(IModuleService moduleService, IMapper mapper, IServiceScopeFactory scopeFactory)
    {
        _moduleService = moduleService ?? throw new ArgumentNullException(nameof(moduleService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }

    [HttpPost("SaveModuleAsync")]
    [ProducesResponseType(typeof(ModuleViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "WritePolicy")]
    public async Task<IActionResult> SaveModuleAsync([FromBody] ModuleInputModel model)
    {
        var entity = await _moduleService.SaveAsync(_mapper.Map<ModuleInputModel, Module>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<Module, ModuleFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<IModuleService>();
            var viewModel = _mapper.Map<Module, ModuleViewModel>(await service.FindAsync(filter, DataFilter));
            return CustomResult(Lang.Find("success"), viewModel);
        }
    }

    [HttpPost("UpdateModuleAsync")]
    [ProducesResponseType(typeof(ModuleViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UpdatePolicy")]
    public async Task<IActionResult> UpdateModuleAsync([FromBody] ModuleInputModel model)
    {
        var entity = await _moduleService.UpdateAsync(_mapper.Map<ModuleInputModel, Module>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<Module, ModuleFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<IModuleService>();
            var viewModel = _mapper.Map<Module, ModuleViewModel>(await service.FindAsync(filter, DataFilter));
            return CustomResult(Lang.Find("success"), viewModel);
        }
    }

    [HttpPost("SoftDeleteModuleAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "SoftDeletePolicy")]
    public async Task<IActionResult> SoftDeleteModuleAsync([FromBody] ModuleInputModel model)
    {
        var hasDeleted = await _moduleService.SoftDeleteAsync(_mapper.Map<ModuleInputModel, Module>(model), DataFilter);

        return CustomResult(Lang.Find("success"), hasDeleted);
    }

    [HttpPost("DeleteModuleAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "DeletePolicy")]
    public async Task<IActionResult> DeleteModuleAsync([FromBody] ModuleInputModel model)
    {
        var hasDeleted = await _moduleService.DeleteAsync(_mapper.Map<ModuleInputModel, Module>(model), DataFilter);

        return CustomResult(Lang.Find("success"), hasDeleted);
    }

    [HttpPost("FindModuleAsync")]
    [ProducesResponseType(typeof(ModuleViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ReadPolicy")]
    public async Task<IActionResult> FindModuleAsync([FromBody] ModuleFilterModel filter)
    {
        var entity = await _moduleService.FindAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<Module, ModuleViewModel>(entity));
    }

    [HttpPost("GetModulesAsync")]
    [ProducesResponseType(typeof(List<ModuleViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ReadPolicy")]
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