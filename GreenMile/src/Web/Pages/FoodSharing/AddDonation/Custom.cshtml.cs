using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Web.Models;
using Web.Services;

namespace Web.Pages.FoodSharing.AddDonation;

[Authorize]
public class CustomModel : PageModel
{
    private readonly DonationService _donationService;
    private readonly FoodItemService _foodItemService;
    private readonly UserManager<User> _userManager;
    private readonly IHostEnvironment _environment;

    public CustomModel(DonationService donationService, FoodItemService foodItemService, UserManager<User> userManager, IHostEnvironment environment)
    {
        _donationService = donationService;
        _foodItemService = foodItemService;
        _userManager = userManager;
        _environment = environment;
    }

    [BindProperty, Required, MinLength(1), MaxLength(20)]
    public string Name { get; set; }
    [BindProperty, Required, MinLength(0), MaxLength(100)]
    public string Description { get; set; }
    [BindProperty, Required, Range(1, 100, ErrorMessage = " Choose between 1 - 100")]
    public int Quantity { get; set; }
    [BindProperty, Required]
    public DateTime ExpiryDate { get; set; }

    [BindProperty, Required]
    public IFormFile Image { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
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
            ExpiryDate = ExpiryDate,
            IsCustom = true,
        };

        var uploadFolder = "Uploads";
        var imageFile = Guid.NewGuid() + Path.GetExtension(Image.FileName);
        var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadFolder, imageFile);
        using var fileStream = new FileStream(imagePath, FileMode.Create);
        await Image.CopyToAsync(fileStream);
        newFood.ImageFilePath = string.Format("/{0}/{1}", uploadFolder, imageFile);

        _foodItemService.AddFoodItem(newFood);

        var donation = new Donation()
        {
            User = user,
            FoodItem = newFood,
            Status = DonationStatus.PENDING,
        };

        _donationService.AddDonation(donation);

        return Redirect("/FoodSharing");
    }
}