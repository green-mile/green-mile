using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Web.Models;
using Web.Services;

namespace Web.Pages.FoodTracker
{
    public class AddFoodItemModel : PageModel
    {
        private readonly FoodItemService _fooditemService;
        private readonly UserManager<User> _userManager;

        public AddFoodItemModel(FoodItemService fooditemService, UserManager<User> userManager)
        {
            _fooditemService = fooditemService;
            _userManager = userManager;
        }

        [BindProperty, Required, MinLength(1), MaxLength(20)]
        public string Name { get; set; }
        [BindProperty, Required, MinLength(0), MaxLength(100)]
        public string Description { get; set; }
        [BindProperty, Required, Range(1, 100, ErrorMessage = " Choose between 1 - 100")]
        public int Quantity { get; set; }
        [BindProperty, Required]
        public DateTime ExpiryDate { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            var user = await _userManager.Users
                .Include(u => u.Household)
                .FirstAsync(u => u.Id == userId);

            var household = user.Household;

            var newFood = new FoodItem()
            {
                Household = household,
                Name = Name,
                Description = Description,
                Quantity = Quantity,
                ExpiryDate = ExpiryDate
            };

            _fooditemService.AddFoodItem(newFood);
            return Redirect("/FoodTracker");
        }
    }
}
