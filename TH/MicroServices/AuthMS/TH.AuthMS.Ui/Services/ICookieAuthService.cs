using System.Security.Claims;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;

namespace TH.AuthMS.Ui;

public interface ICookieAuthService
{
    Task<bool> LoginAsync(LoginModel.InputModel model);
    Task LogoutAsync();
    Task<bool> RegistrarAsync(LoginModel.InputModel model);
    Task<bool> AddUserClaimsAsync(string user, Claim claim);
    Task GenerateCookieAuthenticationAsync(string userName);
}