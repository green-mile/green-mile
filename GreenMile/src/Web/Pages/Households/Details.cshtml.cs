using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Models;
using Web.Services;
using Web.UiState;
using Web.Utils;

namespace Web.Pages.Account.Households
{
    [Authorize]
    public class DetailsModel : PageModel
    {

        private readonly IHouseholdService _householdService;
        private readonly UserManager<User> _userManager;
        public DetailsModel(IHouseholdService householdService, UserManager<User> userManager)
        {
            _userManager = userManager;
            _householdService = householdService;
        }
        [BindProperty]
        public HouseholdDetailsUiState HouseholdDetailsUiState { get; set; } = new HouseholdDetailsUiState();

        public async Task<IActionResult> OnGetAsync()
        {

            int user = (int)(await _userManager.GetUserAsync(User)).HouseholdId;
            if (!await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), "Member"))
            {
                return Redirect("/account/transferhousehold");
            }
            if (user != null)
            {
                Utils.Result<Household> household = await _householdService.RetrieveHouseholdDetails(user);
                if (household.Status == Utils.Status.SUCCESS && household.Value != null)
                {
                    HouseholdDetailsUiState.Users = household?.Value?.Users;
                }
                else
                {
                    Console.WriteLine("OI YOU FUCK");
                }

            }
            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _householdService.RemoveUserFromHousehold(HouseholdDetailsUiState?.UserRemoveId);
            TempData["success"] = "User has been removed!";

            return Redirect("/households/details");
        }

        public async Task<IActionResult> OnPostPatchAsync()
        {
            Result<User> result = await _householdService.TransferHouseholdOwnership((await _userManager.GetUserAsync(User)).Id, HouseholdDetailsUiState?.NextOwnerId);
            if (result.Status == Status.FAILURE)
            {
                TempData["error"] = "Something unexpected went wrong";
                return Page();
            };


            TempData["success"] = "Household ownership transferred!";
            return Redirect("/households/details");
        }


    }
}
