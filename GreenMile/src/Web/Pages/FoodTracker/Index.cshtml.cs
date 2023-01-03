using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Data;
using Web.Models;
using Web.Services;

namespace Web.Pages.FoodTracker
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly FoodItemService _fooditemService;

        [BindProperty]
        public string Id { get; set; }

        public IndexModel(FoodItemService fooditemService)
        {
            _fooditemService = fooditemService;
        }

        public List<FoodItem> FoodItemList { get; set; } = new();
       
        public void OnGet()
        {
            FoodItemList = _fooditemService.GetAll();
        }

        public async Task<IActionResult> OnPostAsync()
        {
         
            FoodItem? food = _fooditemService.GetFoodItemById(Id);
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
