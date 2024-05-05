using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TH.AuthMS.Ui.Pages
{
    [Authorize]
    public class OnlyAdminPageModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
