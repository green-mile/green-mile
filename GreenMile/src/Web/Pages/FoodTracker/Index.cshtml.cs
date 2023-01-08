using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Lib;
using Web.Models;
using Web.Services;

namespace Web.Pages.FoodTracker
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly FoodItemService _fooditemService;
        private readonly IHouseholdService _householdService;
        private readonly IHttpContextAccessor _contextAccessor;

        [BindProperty]
        public int Id { get; set; }
        public IEnumerable<FoodItem> FoodItemList { get; set; }


        public IndexModel(FoodItemService fooditemService, IHouseholdService householdService, IHttpContextAccessor contextAccessor)
        {
            _fooditemService = fooditemService;
            _householdService = householdService;
            _contextAccessor = contextAccessor;
        }

        public async Task OnGetAsync()
        {
            var householdName = _contextAccessor.HttpContext?.Session.GetString(SessionVariable.HousholdName);
            var household = (await _householdService.RetrieveHouseholdDetailsByName(householdName)).Value;
            FoodItemList = _fooditemService.GetAll(household);
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
