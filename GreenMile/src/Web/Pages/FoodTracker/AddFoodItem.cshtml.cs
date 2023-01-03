using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Data;
using Web.Lib;
using Web.Models;
using Web.Services;

namespace Web.Pages.FoodTracker
{
    public class AddFoodItemModel : PageModel { 

        private readonly FoodItemService _fooditemService;

        private readonly IHttpContextAccessor _http;
        private readonly IHouseholdService _householdService;

        public AddFoodItemModel(IHttpContextAccessor http, IHouseholdService householdService, FoodItemService fooditemService)
        {
            _http = http;
            _householdService = householdService;
            _fooditemService = fooditemService;
        }

        

        //[BindProperty, Required]
        //public string userHousedhold { get; set; }
        [BindProperty, Required,MinLength(1),MaxLength(20)]
        public string Name { get; set; }
        [BindProperty, Required,MinLength(0),MaxLength(100)]
        public string Description { get; set; }
        [BindProperty, Required,Range(1,100,ErrorMessage = " Choose between 1 - 100")]
        public int Count { get; set; }
        [BindProperty, Required]
        public string  ExpiryDate { get; set; } 

        


        public void OnGet()
        {

           
        }


        public async Task<IActionResult> OnPostAsync()
        {
           
           

            if (ModelState.IsValid)
            {
                Guid myuuid = Guid.NewGuid();
                string myuuidAsString = myuuid.ToString();

                var  userHousedhold = _http.HttpContext?.Session.GetString(SessionVariable.HousholdName);

                var newFood = new FoodItem()
                {
                    Id = myuuidAsString,
                    Household = userHousedhold,
                    Name = Name,
                    Description = Description,
                    Count = Count,
                    ExpiryDate = ExpiryDate
                };


                _fooditemService.AddFoodItem(newFood);
                return Redirect("/FoodTracker");

                
            }
           
            return Page();

        }

    }
}