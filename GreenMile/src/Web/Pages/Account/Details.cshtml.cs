using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.UiState;
using Web.Models;
using Web.Services;
using Microsoft.AspNetCore.Identity;


namespace Web.Pages.Account
{

    [Authorize]
    public class DetailsModel : PageModel
    {

        [BindProperty]
        public AccountUiState? AccountUiState { get; set; } = new AccountUiState();
        [BindProperty]
        public IFormFile? Upload { get; set; }
        private readonly IHouseholdService _householdService;
        private readonly UserManager<User> _userManager;
        private readonly IImageService _imageService;

        public DetailsModel(IHouseholdService householdService, UserManager<User> userManager, IImageService imageService)
        {
            _householdService = householdService;
            _userManager = userManager;
            _imageService = imageService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
          
            User user = (await _userManager.GetUserAsync(HttpContext.User));
            if (!await _userManager.IsInRoleAsync(user, "Member"))
            {
                return Redirect("/account/transferhousehold");
            }
            Household household = (await _householdService.RetrieveHouseholdDetails(user.HouseholdId ?? -1)).Value;
            AccountUiState.Tab = TempData["tab"]?.ToString();
            AccountUiState.Household = household;
            AccountUiState.FirstName = user.FirstName;
            AccountUiState.LastName = user.LastName;
            AccountUiState.Username = user.UserName;
            AccountUiState.EmailAddress = user.Email;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            TempData["tab"] = AccountUiState.Tab;
            User user = (await _userManager.GetUserAsync(HttpContext.User));
            List<Boolean> formCheck = new List<Boolean>()
            {
                AccountUiState.FirstName != user.FirstName, AccountUiState.Username != user.UserName, AccountUiState.LastName != user.LastName, AccountUiState.EmailAddress != user.Email, AccountUiState.NewPassword != null,
                AccountUiState.ConfirmPassword != null, AccountUiState.HasImageChanged
            };
            if(ModelState.IsValid && formCheck.Any(x => x == true))

            {
                if(formCheck.Any(x => x == true))
                {
                    if (!(await _userManager.CheckPasswordAsync(user, AccountUiState.Password)))
                    {
                        ModelState.AddModelError("AccountUiState.Password", "Please enter the correct password if you want to change credentials!");
                        return Page();
                    }
                    if (AccountUiState.NewPassword != AccountUiState.ConfirmPassword)
                    {
                        ModelState.AddModelError("AccountUiState.NewPassword", "Please confirm the password correctly");
                        return Page();
                    }
                    if (AccountUiState.NewPassword != null) await _userManager.ResetPasswordAsync(user, await _userManager.GeneratePasswordResetTokenAsync(user), AccountUiState.NewPassword);

                    user.FirstName = AccountUiState.FirstName;
                    user.LastName = AccountUiState.LastName;
                    user.UserName = AccountUiState.Username;
                    user.Email = AccountUiState.EmailAddress;
                    if(AccountUiState.Upload!=null)
                    {
                        Console.WriteLine("WHAT THE FUC");
                    }
                    if(AccountUiState.HasImageChanged && Upload != null)
                    {
                        _imageService.StoreImage(Upload, user);
                    }
                 

                    await _userManager.UpdateAsync(user);
                    TempData["success"] = "Changes have been saved"!;
                }
                else
                {
                    TempData["info"] = "Your changes are already saved";
                }
   

            } else
            {
                TempData["error"] = "Form not filled properly!";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            TempData["tab"] = AccountUiState.Tab;
            User user = await _userManager.GetUserAsync(HttpContext.User);
            if(!ModelState.IsValid && (user.UserName != AccountUiState.Username || !await _userManager.CheckPasswordAsync(user, AccountUiState.Password)))
            {
                TempData["tab"] = "danger-zone";
                TempData["error"] = "Username and/or password incorrect!";


                return Redirect("/Account/Details");
            } else
            {
                await _userManager.DeleteAsync(user);
                TempData["success"] = "Account deleted!";
                return Redirect("/");
            }

        }
    }
}
