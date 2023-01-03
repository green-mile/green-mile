using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Models;

namespace Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly UserManager<User> _userManager;

    public IndexModel(ILogger<IndexModel> logger, UserManager<User> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        if (!await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), "Member"))
        {
            return Redirect("/account/transferhousehold");
        }
        return Page();
    }
}