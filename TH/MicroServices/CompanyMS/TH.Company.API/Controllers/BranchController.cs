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
public class BranchController : CustomBaseController
{
    private readonly IBranchService _branchService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;

    public BranchController(IBranchService branchService, IMapper mapper, IServiceScopeFactory scopeFactory)
    {
        _branchService = branchService ?? throw new ArgumentNullException(nameof(branchService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }

    [HttpPost("SaveBranchAsync")]
    [ProducesResponseType(typeof(BranchViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "WritePolicy")]
    public async Task<IActionResult> SaveBranchAsync([FromBody] BranchInputModel model)
    {
        var entity = await _branchService.SaveAsync(_mapper.Map<BranchInputModel, Branch>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<Branch, BranchFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<IBranchService>();
            var viewModel = _mapper.Map<Branch, BranchViewModel>(await service.FindAsync(filter, DataFilter));
            return CustomResult(Lang.Find("success"), viewModel);
        }
    }

    [HttpPost("UpdateBranchAsync")]
    [ProducesResponseType(typeof(BranchViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UpdatePolicy")]
    public async Task<IActionResult> UpdateBranchAsync([FromBody] BranchInputModel model)
    {
        var entity = await _branchService.UpdateAsync(_mapper.Map<BranchInputModel, Branch>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<Branch, BranchFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<IBranchService>();
            var viewModel = _mapper.Map<Branch, BranchViewModel>(await service.FindAsync(filter, DataFilter));
            return CustomResult(Lang.Find("success"), viewModel);
        }
    }

    [HttpPost("SoftDeleteBranchAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "SoftDeletePolicy")]
    public async Task<IActionResult> SoftDeleteBranchAsync([FromBody] BranchInputModel model)
    {
        var hasDeleted = await _branchService.SoftDeleteAsync(_mapper.Map<BranchInputModel, Branch>(model), DataFilter);

        return CustomResult(Lang.Find("success"), hasDeleted);
    }

    [HttpPost("DeleteBranchAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "DeletePolicy")]
    public async Task<IActionResult> DeleteBranchAsync([FromBody] BranchInputModel model)
    {
        var hasDeleted = await _branchService.DeleteAsync(_mapper.Map<BranchInputModel, Branch>(model), DataFilter);

        return CustomResult(Lang.Find("success"), hasDeleted);
    }

    [HttpPost("FindBranchAsync")]
    [ProducesResponseType(typeof(BranchViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ReadPolicy")]
    public async Task<IActionResult> FindBranchAsync([FromBody] BranchFilterModel filter)
    {
        var entity = await _branchService.FindAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<Branch, BranchViewModel>(entity));
    }

    [HttpPost("GetBranchesAsync")]
    [ProducesResponseType(typeof(List<BranchViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ReadPolicy")]
    public async Task<IActionResult> GetBranchesAsync([FromBody] BranchFilterModel filter)
    {
        var entities = await _branchService.GetAsync(filter, DataFilter);
        if (entities is null) return CustomResult(Lang.Find("error_not_found"), entities, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<List<Branch>, List<BranchViewModel>>(entities.ToList()));
    }

    public override void Dispose()
    {
        _branchService?.Dispose();
    }
}