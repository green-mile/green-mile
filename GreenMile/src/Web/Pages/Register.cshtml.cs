using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Models;

namespace Web.Pages;

public class RegisterModel : PageModel
{
    private UserManager<User> _userManager { get; }
    private SignInManager<User> _signInManager { get; }

    [BindProperty, Required]
    public string UserName { get; set; }
    [BindProperty, Required]
    public string FirstName { get; set; }
    [BindProperty, Required]
    public string LastName { get; set; }
    [BindProperty, Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [BindProperty, Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [BindProperty, Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords doesn't match.")]
    public string ConfirmPassword { get; set; }

    public RegisterModel(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public void OnGetAsync()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            Console.WriteLine($"[DEBUG]: Username = {UserName}");
            Console.WriteLine($"[DEBUG]: FirstName = {FirstName}");
            Console.WriteLine($"[DEBUG]: LastName = {LastName}");
            Console.WriteLine($"[DEBUG]: Email = {Email}");
            Console.WriteLine($"[DEBUG]: Password = {Password}");

            var user = new User()
            {
                UserName = UserName,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
            };

            var result = await _userManager.CreateAsync(user, Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToPage("/Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return Page();
    }
}