using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TH.AddressMS.Grpc;
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
    private readonly AddressGrpcClientService _addressGrpcClientService;

    public CompanyController(ICompanyService companyService, IMapper mapper, IServiceScopeFactory scopeFactory, HttpContextAccessor httpContextAccessor, IHubContext<CompanyHub, ICompanyHub> hubContext, AddressGrpcClientService addressGrpcClientService) : base(httpContextAccessor)
    {
        _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        _addressGrpcClientService = addressGrpcClientService ?? throw new ArgumentNullException(nameof(addressGrpcClientService));

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
            var filter = new CompanyFilterModel { Id = entity.Id };
            var service = scope.ServiceProvider.GetRequiredService<ICompanyService>();
            var viewModel = _mapper.Map<Company, CompanyViewModel>(await service.FindByIdAsync(filter, DataFilter));

            //grpc - save address
            foreach (var branch in model.Branches)
            {
                var request = _mapper.Map<AddressInputModel, AddressInputRequest>(branch.Address);
                var viewReply = await _addressGrpcClientService.TrySaveAsync(request);
            }



            await _hubContext.Clients.All.BroadcastOnSaveCompanyAsync(viewModel);

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
            var filter = new CompanyFilterModel { Id = entity.Id };
            var service = scope.ServiceProvider.GetRequiredService<ICompanyService>();
            var viewModel = _mapper.Map<Company, CompanyViewModel>(await service.FindByIdAsync(filter, DataFilter));

            await _hubContext.Clients.All.BroadcastOnUpdateCompanyAsync(viewModel);

            return CustomResult(Lang.Find("success"));
        }
    }

    [HttpPost("ArchiveCompanyAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "CompanyArchivePolicy")]
    public async Task<IActionResult> ArchiveCompanyAsync([FromBody] CompanyInputModel model)
    {
        //first grab it
        var filter = new CompanyFilterModel { Id = model.Id };
        var viewModel = _mapper.Map<Company, CompanyViewModel>(await _companyService.FindByIdAsync(filter, DataFilter));

        //then archive
        await _companyService.ArchiveAsync(_mapper.Map<CompanyInputModel, Company>(model), DataFilter);

        await _hubContext.Clients.All.BroadcastOnArchiveCompanyAsync(viewModel);

        return CustomResult(Lang.Find("success"));
    }

    [HttpPost("DeleteCompanyAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "CompanyDeletePolicy")]
    public async Task<IActionResult> DeleteCompanyAsync([FromBody] CompanyInputModel model)
    {
        //first grab it
        var filter = new CompanyFilterModel { Id = model.Id };
        var viewModel = _mapper.Map<Company, CompanyViewModel>(await _companyService.FindByIdAsync(filter, DataFilter));

        //then delete
        await _companyService.DeleteAsync(_mapper.Map<CompanyInputModel, Company>(model), DataFilter);

        await _hubContext.Clients.All.BroadcastOnDeleteCompanyAsync(viewModel);

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