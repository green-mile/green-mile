using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Models;

namespace Web.Pages;

[Authorize]
public class LogoutModel : PageModel
{
    private readonly SignInManager<User> _signInManager;
    private readonly IHttpContextAccessor _http;

    public LogoutModel(SignInManager<User> signInManager, IHttpContextAccessor http)
    {
        _signInManager = signInManager;
        _http = http;
    }

    public IActionResult OnGet()
    {
        if (User.Identity.IsAuthenticated)
        {
            _signInManager.SignOutAsync();
            _http.HttpContext.Session.Clear();
        }
        return RedirectToPage("/Index");
    }
}