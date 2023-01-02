using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Data;
using Web.Models;
using Web.Services;

namespace Web.Pages.FoodTracker
{
    public class AddFoodItemModel : PageModel { 

         private readonly FoodItemService _fooditemService;

        public AddFoodItemModel(FoodItemService fooditemService)
        {
            _fooditemService = fooditemService;
        }
        
     
        public void OnGet()
        {
        }

        public FoodItem MyFoodItem { get; set; } = new();

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                FoodItem? fooditem = _fooditemService.GetFoodItemById(MyFoodItem.Id);
                _fooditemService.AddFoodItem(MyFoodItem);
                return Redirect("/FoodTracker");
            }
            return Page();

        }

    }
}
