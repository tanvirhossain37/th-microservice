using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TH.Common.Lang;
using TH.Common.Model;
using TH.ShadowMS.App;
using TH.ShadowMS.Core;

namespace TH.ShadowMS.API.Controllers;

[Authorize(Policy = "ClaimBasedPolicy")]
public class ShadowController : CustomBaseController
{
    private readonly IShadowService _shadowService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;

    public ShadowController(IShadowService shadowService, IMapper mapper, IServiceScopeFactory scopeFactory, HttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _shadowService = shadowService ?? throw new ArgumentNullException(nameof(shadowService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }

    [HttpPost("FindShadowAsync")]
    [ProducesResponseType(typeof(ShadowViewModel), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ReadPolicy")]
    public async Task<IActionResult> FindShadowAsync([FromBody] ShadowFilterModel filter)
    {
        var entity = await _shadowService.FindAsync(filter, DataFilter);
        if (entity is null) return CustomResult(Lang.Find("error_not_found"), entity, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<Shadow, ShadowViewModel>(entity));
    }

    [HttpPost("GetShadowsAsync")]
    [ProducesResponseType(typeof(List<ShadowViewModel>), (int)HttpStatusCode.OK)]
    [Authorize(Policy = "ReadPolicy")]
    public async Task<IActionResult> GetShadowsAsync([FromBody] ShadowFilterModel filter)
    {
        var entities = await _shadowService.GetAsync(filter, DataFilter);
        if (entities is null) return CustomResult(Lang.Find("error_not_found"), entities, HttpStatusCode.NotFound);

        return CustomResult(Lang.Find("success"), _mapper.Map<List<Shadow>, List<ShadowViewModel>>(entities.ToList()));
    }

    public override void Dispose()
    {
        _shadowService?.Dispose();
    }
}