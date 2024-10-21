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
public class CompanySettingController : CustomBaseController
{
    private readonly ICompanySettingService _companySettingService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHubContext<CompanySettingHub, ICompanySettingHub> _hubContext;

    public CompanySettingController(ICompanySettingService companySettingService, IMapper mapper, IServiceScopeFactory scopeFactory, HttpContextAccessor httpContextAccessor, IHubContext<CompanySettingHub, ICompanySettingHub> hubContext) : base(httpContextAccessor)
    {
        _companySettingService = companySettingService ?? throw new ArgumentNullException(nameof(companySettingService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));

        _companySettingService.SetUserResolver(UserResolver);
    }

    [HttpPost("SaveCompanySettingAsync")]
    [ProducesResponseType(typeof(CompanySettingViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "CompanySettingWritePolicy")]
    public async Task<IActionResult> SaveCompanySettingAsync([FromBody] CompanySettingInputModel model)
    {
        var entity = await _companySettingService.SaveAsync(_mapper.Map<CompanySettingInputModel, CompanySetting>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = new CompanySettingFilterModel { Id = entity.Id };
            var service = scope.ServiceProvider.GetRequiredService<ICompanySettingService>();
            var viewModel = _mapper.Map<CompanySetting, CompanySettingViewModel>(await service.FindByIdAsync(filter, DataFilter));

            await _hubContext.Clients.All.BroadcastOnSaveCompanySettingAsync(viewModel);

            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("UpdateCompanySettingAsync")]
    [ProducesResponseType(typeof(CompanySettingViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "CompanySettingUpdatePolicy")]
    public async Task<IActionResult> UpdateCompanySettingAsync([FromBody] CompanySettingInputModel model)
    {
        var entity = await _companySettingService.UpdateAsync(_mapper.Map<CompanySettingInputModel, CompanySetting>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = new CompanySettingFilterModel { Id = entity.Id };
            var service = scope.ServiceProvider.GetRequiredService<ICompanySettingService>();
            var viewModel = _mapper.Map<CompanySetting, CompanySettingViewModel>(await service.FindByIdAsync(filter, DataFilter));

            await _hubContext.Clients.All.BroadcastOnUpdateCompanySettingAsync(viewModel);

            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("ArchiveCompanySettingAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "CompanySettingArchivePolicy")]
    public async Task<IActionResult> ArchiveCompanySettingAsync([FromBody] CompanySettingInputModel model)
    {
        //first grab it
        var filter = new CompanySettingFilterModel { Id = model.Id };
        var viewModel = _mapper.Map<CompanySetting, CompanySettingViewModel>(await _companySettingService.FindByIdAsync(filter, DataFilter));

        //then archive
        await _companySettingService.ArchiveAsync(_mapper.Map<CompanySettingInputModel, CompanySetting>(model), DataFilter);

        await _hubContext.Clients.All.BroadcastOnArchiveCompanySettingAsync(viewModel);

        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("DeleteCompanySettingAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "CompanySettingDeletePolicy")]
    public async Task<IActionResult> DeleteCompanySettingAsync([FromBody] CompanySettingInputModel model)
    {
        //first grab it
        var filter = new CompanySettingFilterModel { Id = model.Id };
        var viewModel = _mapper.Map<CompanySetting, CompanySettingViewModel>(await _companySettingService.FindByIdAsync(filter, DataFilter));

        //then delete
        await _companySettingService.DeleteAsync(_mapper.Map<CompanySettingInputModel, CompanySetting>(model), DataFilter);

        await _hubContext.Clients.All.BroadcastOnDeleteCompanySettingAsync(viewModel);

        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("FindCompanySettingAsync")]
    [ProducesResponseType(typeof(CompanySettingViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "CompanySettingReadPolicy")]
    public async Task<IActionResult> FindCompanySettingAsync([FromBody] CompanySettingFilterModel filter)
    {
        var entity = await _companySettingService.FindByIdAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<CompanySetting, CompanySettingViewModel>(entity));
    }

    [HttpPost("GetCompanySettingsAsync")]
    [ProducesResponseType(typeof(List<CompanySettingViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "CompanySettingReadPolicy")]
    public async Task<IActionResult> GetCompanySettingsAsync([FromBody] CompanySettingFilterModel filter)
    {
        var entities = await _companySettingService.GetAsync(filter, DataFilter);
        if (entities is null) return CustomResult(Lang.Find("error_not_found"), entities, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<List<CompanySetting>, List<CompanySettingViewModel>>(entities.ToList()));
    }

    public override void Dispose()
    {
        _companySettingService?.Dispose();
    }
}