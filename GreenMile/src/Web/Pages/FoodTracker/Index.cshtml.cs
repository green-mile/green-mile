using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Data;
using Web.Models;
using Web.Services;

namespace Web.Pages.FoodTracker
{
    public class IndexModel : PageModel
    {
        private readonly FoodItemService _fooditemService;

        public IndexModel(FoodItemService fooditemService)
        {
            _fooditemService = fooditemService;
        }

        public List<FoodItem> FoodItemList { get; set; } = new();
       
        public void OnGet()
        {
            FoodItemList = _fooditemService.GetAll();
        }

        

      

    }
}
