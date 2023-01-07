using System.ComponentModel.DataAnnotations;

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
        private readonly DonationService _donationService;
        private readonly CustomFoodService _customFoodService;
        private readonly UserManager<User> _userManager;
        private IWebHostEnvironment _environment;

        public EditDonationInfoModel(DonationService donationService,
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
        public IFormFile? Upload { get; set; }

        public Donation MyDonation { get; set; } = new ();

        public List<Donation> DonationList { get; set; } = new();
        public IActionResult OnGet(int id)
        {
            Donation? donation = _donationService.GetDonationById(id);
            CustomFood? customfood = donation.CustomFood;
            if (donation != null)
            {
                MyCustomFood = customfood;
                MyDonation= donation;
                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Donation ID {0} not found", id);
                return Redirect("/FoodSharing/Index");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Upload != null)
                {
                    if (Upload.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("Upload", "File size cannot exceed 2MB.");
                        return Page();
                    }

                    var uploadsFolder = "uploads";
                    if(MyCustomFood.Image != null)
                    {
                        var oldImageFile = Path.GetFileName(MyCustomFood.Image);
                        var oldImagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, oldImageFile);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    var imageFile = Guid.NewGuid() + Path.GetExtension(Upload.FileName);
                    var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    await Upload.CopyToAsync(fileStream);

                    MyCustomFood.Image = string.Format("/{0}/{1}", uploadsFolder, imageFile);
                }


                //MyDonation.User = await _userManager.GetUserAsync(HttpContext.User);
                var user = await _userManager.GetUserAsync(HttpContext.User);
                
                MyDonation.CustomFood = MyCustomFood;
                MyDonation.User = user;
                
                _donationService.UpdateDonation(MyDonation);
                _customFoodService.UpdateCustomFood(MyCustomFood);

                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Donation Offer {0} is successfully updated Of Food", MyCustomFood.Name);
                return Redirect("/FoodSharing/Index");
            }
            return Page();
        }
    }
}
