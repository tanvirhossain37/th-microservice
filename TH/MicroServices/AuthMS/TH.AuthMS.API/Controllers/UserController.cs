using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TH.Common.Model;
using TH.UserSvc.App;

namespace TH.AuthMS.API.Controllers;

[Authorize(Policy = "ClaimBasedPolicy")]
public class UserController : CustomBaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    // GET
    [HttpPost("FindUserByIdAsync")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> FindUserByIdAsync(UserFilterModel filter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));

        //_userService.FindByIdAsync(filter.Id);

        var userIdentity = User.Identity;
        return CustomResult("You saved me!", "");
    }

    public override void Dispose()
    {
        _userService?.Dispose();
    }
}