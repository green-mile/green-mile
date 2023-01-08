
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

<<<<<<< HEAD
using Web.Data;
=======
>>>>>>> 26b665f86e3f69afc3f8516503f69e14f5b03d50
using Web.Lib;
using Web.Models;
using Web.Services;

namespace Web.Pages.FoodTracker
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly FoodItemService _fooditemService;
<<<<<<< HEAD
        private readonly FoodCategoryService _foodcategoryService;
=======
>>>>>>> 26b665f86e3f69afc3f8516503f69e14f5b03d50
        private readonly IHouseholdService _householdService;
        private readonly IHttpContextAccessor _contextAccessor;

        [BindProperty]
<<<<<<< HEAD
        public int Id { get; set;}

        public IEnumerable<FoodItem> FoodItemList { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public int Count { get; set; }



        public IndexModel(FoodItemService fooditemService, IHouseholdService householdService, IHttpContextAccessor contextAccessor, FoodCategoryService foodcategoryService)
=======
        public int Id { get; set; }
        public IEnumerable<FoodItem> FoodItemList { get; set; }


        public IndexModel(FoodItemService fooditemService, IHouseholdService householdService, IHttpContextAccessor contextAccessor)
>>>>>>> 26b665f86e3f69afc3f8516503f69e14f5b03d50
        {
            _fooditemService = fooditemService;
            _householdService = householdService;
            _contextAccessor = contextAccessor;
<<<<<<< HEAD
            _foodcategoryService = foodcategoryService;
=======
>>>>>>> 26b665f86e3f69afc3f8516503f69e14f5b03d50
        }

        public async Task OnGetAsync()
        {
            var householdName = _contextAccessor.HttpContext?.Session.GetString(SessionVariable.HousholdName);
            var household = (await _householdService.RetrieveHouseholdDetailsByName(householdName)).Value;
            FoodItemList = _fooditemService.GetAll(household);
<<<<<<< HEAD
            Count = FoodItemList.Count();

            var newcategory = new Category()
            {
                Name = "Fruit",
                Description = "Fruits are healthy"

            };
            _foodcategoryService.AddCategory(newcategory);


=======
>>>>>>> 26b665f86e3f69afc3f8516503f69e14f5b03d50
        }

        public async Task<IActionResult> OnPostAsync()
        {
<<<<<<< HEAD
            FoodItem? food = _fooditemService.GetFoodItemById(Id);
=======
            FoodItem? food = await _fooditemService.GetFoodItemById(Id);
>>>>>>> 26b665f86e3f69afc3f8516503f69e14f5b03d50
            if (food != null)
            {
               
                _fooditemService.DeleteFoodItem(food);
<<<<<<< HEAD
                return Redirect("/FoodTracker");

            }
           
            return Page();
=======
                return Redirect("/Index");
            }
            else
            {
                return Page();
            }
>>>>>>> 26b665f86e3f69afc3f8516503f69e14f5b03d50
        }
    }
}
