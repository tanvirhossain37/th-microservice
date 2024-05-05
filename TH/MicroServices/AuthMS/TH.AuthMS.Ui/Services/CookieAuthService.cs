using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;

namespace TH.AuthMS.Ui;

public class CookieAuthService : ICookieAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserRoleStore<IdentityRole> _userRoleStore;
    private readonly IHttpContextAccessor _httpContext;

    public CookieAuthService(UserManager<IdentityUser> userManager, IUserRoleStore<IdentityRole> userRoleStore, IHttpContextAccessor httpContext)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _userRoleStore = userRoleStore ?? throw new ArgumentNullException(nameof(userRoleStore));
        _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
    }

    public async Task<bool> LoginAsync(LoginModel.InputModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        //_userManager.FindByNameAsync(model.Email)
        return false;
    }

    public async Task LogoutAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RegistrarAsync(LoginModel.InputModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddUserClaimsAsync(string user, Claim claim)
    {
        throw new NotImplementedException();
    }

    public async Task GenerateCookieAuthenticationAsync(string userName)
    {
        throw new NotImplementedException();
    }
}