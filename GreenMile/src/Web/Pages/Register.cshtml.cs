using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Lib;
using Web.Models;
using Web.Services;
using Web.UiState;
using Web.Utils;

namespace Web.Pages;

public class RegisterModel : PageModel
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IHouseholdService _householdService;

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

    [BindProperty, Required]
    public HouseholdUiState HouseholdUiState { get; set; }

    public RegisterModel(UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor contextAccessor, IHouseholdService householdService)
    {
        _householdService = householdService;
        _userManager = userManager;
        _signInManager = signInManager;
        _contextAccessor = contextAccessor;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if ((bool)HouseholdUiState.JoinHousehold && HouseholdUiState.JoinHouseholdName is null)
        {
            ModelState.AddModelError("HouseholdUiState.JoinHouseholdName", "Please fill in the household name you want to join!");
            return Page();
        }
        else if ((bool)!HouseholdUiState.JoinHousehold && HouseholdUiState.CreateHouseholdName is null)
        {
            ModelState.AddModelError("HouseholdUiState.CreateHouseholdName", "Please fill in the household name you want to create!");
            return Page();
        }

        if (ModelState.IsValid)
        {
            if ((bool)HouseholdUiState.JoinHousehold)
            {
                Result<Household> householdResult = await _householdService.RetrieveHouseholdDetailsByName(HouseholdUiState.JoinHouseholdName);
                if (householdResult.Status == Status.FAILURE)
                {
                    ModelState.AddModelError("HouseholdUiState.JoinHouseholdName", householdResult.Message);
                    householdResult.Print();
                    return Page();
                }
            }
            else if ((bool)!HouseholdUiState.JoinHousehold)
            {
                Result<Household> createHouseholdResult = await _householdService.RetrieveHouseholdDetailsByName(HouseholdUiState.CreateHouseholdName);
                if (createHouseholdResult.Status == Status.SUCCESS)
                {
                    ModelState.AddModelError("HouseholdUiState.CreateHouseholdName", createHouseholdResult.Message);
                    createHouseholdResult.Print();
                    return Page();
                }
            }

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
                var userId = (await _userManager.FindByNameAsync(UserName)).Id;

                _contextAccessor.HttpContext.Session.SetString(SessionVariable.UserName, UserName);
                _contextAccessor.HttpContext.Session.SetString(SessionVariable.UserId, userId);

                if ((bool)!HouseholdUiState.JoinHousehold)
                {
                    await _householdService.CreateHousehold(HouseholdUiState.CreateHouseholdName);
                    await _householdService.AddUserToHousehold(userId, (await _householdService.RetrieveHouseholdDetailsByName(HouseholdUiState.CreateHouseholdName)).Value.HouseholdId);
                }
                else
                {
                    await _householdService.AddUserToHousehold(userId, (await _householdService.RetrieveHouseholdDetailsByName(HouseholdUiState.JoinHouseholdName)).Value.HouseholdId);
                }

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