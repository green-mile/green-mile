using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Web.Lib;
using Web.Models;

namespace Web.Pages;

public class LoginModel : PageModel
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _http;

    [BindProperty, Required]
    public string UserName { get; set; }
    [BindProperty, Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [BindProperty, Required]
    public bool RememberMe { get; set; }

    public LoginModel(SignInManager<User> signInManager, IHttpContextAccessor http, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _http = http;
        _userManager = userManager;
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var identityResult = await _signInManager.PasswordSignInAsync(UserName, Password, RememberMe, true);
            if (identityResult.Succeeded)
            {
                var user = await _userManager.Users
                    .Include(u => u.Household)
                    // Using First(...) is more efficient because Users are
                    // guaranteed to have a unique Username (registration check).
                    // This avoids the overhead of Single(...)'s uniqueness
                    // check and improves performance.
                    .FirstAsync(u => u.UserName == UserName);
                _http.HttpContext.Session.SetString(SessionVariable.UserName, user.UserName);
                _http.HttpContext.Session.SetString(SessionVariable.UserId, user.Id);
                _http.HttpContext.Session.SetString(SessionVariable.HousholdName, user.Household.Name);
                return RedirectToPage("/Index");
            }
            ModelState.AddModelError("", "Username or Password incorrect.");
        }
        return Page();
    }
}