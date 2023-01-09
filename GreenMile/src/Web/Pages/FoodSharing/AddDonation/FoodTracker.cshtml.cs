using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using Web.Models;
using Web.Services;

namespace Web.Pages.FoodSharing.AddDonation;

[Authorize]
public class FoodTracker : PageModel
{
    private readonly DonationService _donationService;
    private readonly FoodItemService _foodItemService;
    private readonly IHouseholdService _householdService;
    private readonly UserManager<User> _userManager;

    public FoodTracker(DonationService donationService, FoodItemService foodItemService, UserManager<User> userManager, IHouseholdService householdService)
    {
        _donationService = donationService;
        _foodItemService = foodItemService;
        _householdService = householdService;
        _userManager = userManager;
    }

    [BindProperty]
    [Required]
    public int FoodItemId { get; set; }

    public SelectList FoodItems { get; set; }

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);
        var household = (await _householdService.RetrieveHouseholdDetails(user.HouseholdId ?? -1)).Value;
        var foodItems = _foodItemService.GetAll(household);
        FoodItems = new(foodItems, nameof(FoodItem.Id), nameof(FoodItem.Name));
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var user = await _userManager.GetUserAsync(HttpContext.User);
        var foodItem = await _foodItemService.GetFoodItemById(FoodItemId);

        var donation = new Donation()
        {
            FoodItem = foodItem,
            User = user,
            Date = DateTime.Now,
            Status = DonationStatus.PENDING
        };

        _donationService.AddDonation(donation);

        return Redirect("/FoodSharing");
    }
}