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
public class BranchUserController : CustomBaseController
{
    private readonly IBranchUserService _branchUserService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;

    public BranchUserController(IBranchUserService branchUserService, IMapper mapper, IServiceScopeFactory scopeFactory)
    {
        _branchUserService = branchUserService ?? throw new ArgumentNullException(nameof(branchUserService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }

    [HttpPost("SaveBranchUserAsync")]
    [ProducesResponseType(typeof(BranchUserViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "WritePolicy")]
    public async Task<IActionResult> SaveBranchUserAsync([FromBody] BranchUserInputModel model)
    {
        var entity = await _branchUserService.SaveAsync(_mapper.Map<BranchUserInputModel, BranchUser>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<BranchUser, BranchUserFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<IBranchUserService>();
            var viewModel = _mapper.Map<BranchUser, BranchUserViewModel>(await service.FindAsync(filter, DataFilter));
            return CustomResult(Lang.Find("success"), viewModel);
        }
    }

    [HttpPost("UpdateBranchUserAsync")]
    [ProducesResponseType(typeof(BranchUserViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "UpdatePolicy")]
    public async Task<IActionResult> UpdateBranchUserAsync([FromBody] BranchUserInputModel model)
    {
        var entity = await _branchUserService.UpdateAsync(_mapper.Map<BranchUserInputModel, BranchUser>(model), DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        using (var scope = _scopeFactory.CreateScope())
        {
            var filter = _mapper.Map<BranchUser, BranchUserFilterModel>(entity);
            var service = scope.ServiceProvider.GetRequiredService<IBranchUserService>();
            var viewModel = _mapper.Map<BranchUser, BranchUserViewModel>(await service.FindAsync(filter, DataFilter));
            return CustomResult(Lang.Find("success"), viewModel);
        }
    }

    [HttpPost("SoftDeleteBranchUserAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "SoftDeletePolicy")]
    public async Task<IActionResult> SoftDeleteBranchUserAsync([FromBody] BranchUserInputModel model)
    {
        var hasDeleted = await _branchUserService.SoftDeleteAsync(_mapper.Map<BranchUserInputModel, BranchUser>(model), DataFilter);

        return CustomResult(Lang.Find("success"), hasDeleted);
    }

    [HttpPost("DeleteBranchUserAsync")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "DeletePolicy")]
    public async Task<IActionResult> DeleteBranchUserAsync([FromBody] BranchUserInputModel model)
    {
        var hasDeleted = await _branchUserService.DeleteAsync(_mapper.Map<BranchUserInputModel, BranchUser>(model), DataFilter);

        return CustomResult(Lang.Find("success"), hasDeleted);
    }

    [HttpPost("FindBranchUserAsync")]
    [ProducesResponseType(typeof(BranchUserViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ReadPolicy")]
    public async Task<IActionResult> FindBranchUserAsync([FromBody] BranchUserFilterModel filter)
    {
        var entity = await _branchUserService.FindAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<BranchUser, BranchUserViewModel>(entity));
    }

    [HttpPost("GetBranchUsersAsync")]
    [ProducesResponseType(typeof(List<BranchUserViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ReadPolicy")]
    public async Task<IActionResult> GetBranchUsersAsync([FromBody] BranchUserFilterModel filter)
    {
        var entities = await _branchUserService.GetAsync(filter, DataFilter);
        if (entities is null) return CustomResult(Lang.Find("error_not_found"), entities, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<List<BranchUser>, List<BranchUserViewModel>>(entities.ToList()));
    }

    public override void Dispose()
    {
        _branchUserService?.Dispose();
    }
}