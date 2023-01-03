using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Models;
using Web.Services;
using Web.UiState;

namespace Web.Pages.Account.Households
{
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

        public async Task OnGetAsync()
        {
            int user = (int)(await _userManager.GetUserAsync(User)).HouseholdId;
            if (user != null)
            {
                Utils.Result<Household> household = await _householdService.RetrieveHouseholdDetails(user);
                if(household.Status == Utils.Status.SUCCESS && household.Value != null)
                {
                    HouseholdDetailsUiState.Users = household?.Value?.Users;
                } else
                {
                    Console.WriteLine("OI YOU FUCK");
                }
         
            }

        }
    }
}
