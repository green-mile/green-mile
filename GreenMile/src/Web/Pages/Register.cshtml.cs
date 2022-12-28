using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Lib;
using Web.Models;
using Web.Services;
using Web.Utils;
using Web.UiState;
using static System.Net.WebRequestMethods;

namespace Web.Pages;

public class RegisterModel : PageModel
{
    private UserManager<User> _userManager { get; }
    private SignInManager<User> _signInManager { get; }
    private IHttpContextAccessor _contextAccessor { get; set; }
    private IHouseholdService _householdService { get; set; }


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

    public void OnGetAsync()
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
                Result<Household> householdResult = (await _householdService.retrieveHouseholdDetailsByName(HouseholdUiState.JoinHouseholdName));
                if (householdResult.Status == Status.FAILURE) {
                    ModelState.AddModelError("HouseholdUiState.JoinHouseholdName", householdResult.Message);
                    householdResult.Print();
                    return Page();
                }
          
            }
            else if ((bool)!HouseholdUiState.JoinHousehold)
            {
                Result<Household> createHouseholdResult = (await _householdService.retrieveHouseholdDetailsByName(HouseholdUiState.CreateHouseholdName));
                if (createHouseholdResult.Status == Status.SUCCESS)
                {
                    ModelState.AddModelError("HouseholdUiState.CreateHouseholdName", createHouseholdResult.Message);
                    createHouseholdResult.Print();
                    return Page();
                }
            }
 


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
                var userId = (await _userManager.FindByNameAsync(UserName)).Id;
                
                _contextAccessor.HttpContext.Session.SetString(SessionVariable.UserName, UserName);
                _contextAccessor.HttpContext.Session.SetString(SessionVariable.UserId, userId);
                

                if((bool)!HouseholdUiState.JoinHousehold)
                {
                    
                    await _householdService.createHousehold(HouseholdUiState.CreateHouseholdName);
                    await _householdService.addUserToHousehold(userId, (await _householdService.retrieveHouseholdDetailsByName(HouseholdUiState.CreateHouseholdName)).Value.HouseholdId);
                } else
                {

                    await _householdService.addUserToHousehold(userId, (await _householdService.retrieveHouseholdDetailsByName(HouseholdUiState.JoinHouseholdName)).Value.HouseholdId);
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