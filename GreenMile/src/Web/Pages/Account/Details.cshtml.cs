using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Account
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
