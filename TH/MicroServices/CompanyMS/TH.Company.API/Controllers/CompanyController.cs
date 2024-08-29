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
public class CompanyController : CustomBaseController
{
    private readonly ICompanyService _companyService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHubContext<CompanyHub, ICompanyHub> _hubContext;

    public CompanyController(ICompanyService companyService, IMapper mapper, IServiceScopeFactory scopeFactory, HttpContextAccessor httpContextAccessor, IHubContext<CompanyHub, ICompanyHub> hubContext) : base(httpContextAccessor)
    {
        _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));

        _companyService.SetUserResolver(UserResolver);
    }

    [HttpPost("SaveCompanyAsync")]
    [ProducesResponseType(typeof(CompanyViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "CompanyWritePolicy")]
    public async Task<IActionResult> SaveCompanyAsync([FromBody] CompanyInputModel model)
    {
        var entity = await _companyService.SaveAsync(_mapper.Map<CompanyInputModel, Company>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<Company, CompanyFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<ICompanyService>();
            var viewModel = _mapper.Map<Company, CompanyViewModel>(await service.FindByIdAsync(filter, DataFilter));

            _hubContext.Clients.All.BroadcastOnSaveCompanyAsync(viewModel);
            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("UpdateCompanyAsync")]
    [ProducesResponseType(typeof(CompanyViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "CompanyUpdatePolicy")]
    public async Task<IActionResult> UpdateCompanyAsync([FromBody] CompanyInputModel model)
    {
        var entity = await _companyService.UpdateAsync(_mapper.Map<CompanyInputModel, Company>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<Company, CompanyFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<ICompanyService>();
            var viewModel = _mapper.Map<Company, CompanyViewModel>(await service.FindByIdAsync(filter, DataFilter));

            _hubContext.Clients.All.BroadcastOnUpdateCompanyAsync(viewModel);
            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("SoftDeleteCompanyAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "CompanySoftDeletePolicy")]
    public async Task<IActionResult> SoftDeleteCompanyAsync([FromBody] CompanyInputModel model)
    {
        await _companyService.SoftDeleteAsync(_mapper.Map<CompanyInputModel, Company>(model), DataFilter);

        _hubContext.Clients.All.BroadcastOnSoftDeleteCompanyAsync(model);
        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("DeleteCompanyAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "CompanyDeletePolicy")]
    public async Task<IActionResult> DeleteCompanyAsync([FromBody] CompanyInputModel model)
    {
        await _companyService.DeleteAsync(_mapper.Map<CompanyInputModel, Company>(model), DataFilter);

        _hubContext.Clients.All.BroadcastOnDeleteCompanyAsync(model);
        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("FindCompanyAsync")]
    [ProducesResponseType(typeof(CompanyViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "CompanyReadPolicy")]
    public async Task<IActionResult> FindCompanyAsync([FromBody] CompanyFilterModel filter)
    {
        var entity = await _companyService.FindByIdAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<Company, CompanyViewModel>(entity));
    }

    [HttpPost("GetCompaniesAsync")]
    [ProducesResponseType(typeof(List<CompanyViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "CompanyReadPolicy")]
    public async Task<IActionResult> GetCompaniesAsync([FromBody] CompanyFilterModel filter)
    {
        var entities = await _companyService.GetAsync(filter, DataFilter);
        if (entities is null) return CustomResult(Lang.Find("error_not_found"), entities, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<List<Company>, List<CompanyViewModel>>(entities.ToList()));
    }

    public override void Dispose()
    {
        _companyService?.Dispose();
    }
}