using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

    public void OnGetAsync() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var identityResult = await _signInManager.PasswordSignInAsync(UserName, Password, RememberMe, true);
            if (identityResult.Succeeded)
            {
                var userId = (await _userManager.FindByNameAsync(UserName)).Id;
                _http.HttpContext.Session.SetString(SessionVariable.UserName, UserName);
                _http.HttpContext.Session.SetString(SessionVariable.UserId, userId);
                return RedirectToPage("/Index");
            }
            ModelState.AddModelError("", "Username or Password incorrect.");
        }
        return Page();
    }
}