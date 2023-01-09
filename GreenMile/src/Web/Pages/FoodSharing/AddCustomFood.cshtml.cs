using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Services;
using Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages.FoodSharing
{
    [Authorize]
    public class AddCustomFoodModel : PageModel
    {
        private readonly DonationService _donationService;
        private readonly CustomFoodService _customFoodService;
        private readonly UserManager<User> _userManager;
        private IWebHostEnvironment _environment;
        public AddCustomFoodModel(DonationService donationService, 
            CustomFoodService customFoodService, 
            UserManager<User> userManager,
            IWebHostEnvironment environment
            )
        {
            _donationService = donationService;
            _customFoodService = customFoodService;
            _userManager = userManager;
            _environment = environment;
        }

        [BindProperty]
        public CustomFood MyCustomFood { get; set; } = new();

        [BindProperty]
        [Required]
        public IFormFile? Upload { get; set; }

        public Donation MyDonation { get; set; } = new();

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                if (Upload != null)
                {
                    if (Upload.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("Upload",
                        "File size cannot exceed 2MB.");
                        return Page();
                    }

                    var uploadsFolder = "uploads";
                    var imageFile = Guid.NewGuid() + Path.GetExtension(Upload.FileName);
                    var imagePath = Path.Combine(_environment.ContentRootPath,"wwwroot", uploadsFolder, imageFile);
                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    await Upload.CopyToAsync(fileStream);

                    MyCustomFood.Image = string.Format("/{0}/{1}", uploadsFolder,imageFile);
                }

                MyDonation.Status = "Active";
                MyDonation.Type = "Active";
                MyDonation.Date = DateTime.Now;
                MyDonation.CustomFood = MyCustomFood;
                MyDonation.User = await _userManager.GetUserAsync(HttpContext.User);

                _customFoodService.AddCustomFood(MyCustomFood);
                _donationService.AddDonation(MyDonation);

                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Donation Offer {0} is created Of Food", MyCustomFood.Name);
                return Redirect("/FoodSharing/Index");
            }
            return Page();
        }
    }
}
