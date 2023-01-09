using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Models;
using Web.Services;

namespace Web.Pages.FoodTracker
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly FoodItemService _fooditemService;
        private readonly UserManager<User> _userManager;
        private readonly IHouseholdService _householdService;

        [BindProperty]
        public int Id { get; set; }
        public int Count { get; set; }
        public IEnumerable<FoodItem> FoodItemList { get; set; }


        public IndexModel(FoodItemService fooditemService, UserManager<User> userManager, IHouseholdService householdService)
        {
            _fooditemService = fooditemService;
            _userManager = userManager;
            _householdService = householdService;
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var household = (await _householdService.RetrieveHouseholdDetails(user.HouseholdId ?? -1)).Value;
            FoodItemList = _fooditemService.GetAll(household);
            Count = FoodItemList.Count();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            FoodItem? food = await _fooditemService.GetFoodItemById(Id);
            if (food != null)
            {
                _fooditemService.DeleteFoodItem(food);
                return Redirect("/Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
