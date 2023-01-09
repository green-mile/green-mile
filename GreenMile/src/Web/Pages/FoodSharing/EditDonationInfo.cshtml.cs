using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Models;

using Web.Services;

namespace Web.Pages.FoodSharing
{
    [Authorize]
    public class EditDonationInfoModel : PageModel
    {
        private readonly FoodItemService _foodService;
        private readonly DonationService _donationService;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<User> _userManager;

        public EditDonationInfoModel(
            FoodItemService foodItemService,
            DonationService donationService,
            IWebHostEnvironment environment,
            UserManager<User> userManager
        )
        {
            _donationService = donationService;
            _foodService = foodItemService;
            _userManager = userManager;
            _environment = environment;
        }

        [BindProperty]
        public int FoodItemId { get; set; }

        [BindProperty]
        public string? Name { get; set; }

        [BindProperty]
        public string? Description { get; set; }

        [BindProperty]
        public int? Quantity { get; set; }

        [BindProperty]
        public DateTime? ExpiryDate { get; set; }

        [BindProperty]
        public IFormFile? Image { get; set; }

        public bool IsCustom { get; set; }

        public IActionResult OnGet(int id)
        {
            var donation = _donationService.GetDonationById(id);

            if (donation is null)
                return NotFound();

            IsCustom = donation.FoodItem!.IsCustom;
            FoodItemId = donation.FoodItem.Id;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var foodItem = await _foodService.GetFoodItemById(FoodItemId);

            foodItem!.Name = Name ?? foodItem.Name;
            foodItem.Description = Description ?? foodItem.Description;
            foodItem.ExpiryDate = ExpiryDate ?? foodItem.ExpiryDate;
            foodItem.Quantity = Quantity ?? foodItem.Quantity;

            if (Image is not null)
            {
                var uploadFolder = "Uploads";
                var imageFile = Guid.NewGuid() + Path.GetExtension(Image.FileName);
                var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadFolder, imageFile);
                using var fileStream = new FileStream(imagePath, FileMode.Create);
                await Image.CopyToAsync(fileStream);
                foodItem.ImageFilePath = string.Format("/{0}/{1}", uploadFolder, imageFile);
            }

            await _foodService.Update(foodItem);

            return Redirect("/FoodSharing");
        }
    }
}
