using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Lib;
using Web.Models;
using Web.Services;

namespace Web.Pages.FoodSharing
{
    [Authorize]
    public class MyDonationsModel : PageModel
    {
        private readonly DonationService _donationService;
        private readonly UserManager<User> _userManager;
        public MyDonationsModel(DonationService donationService, 
                                UserManager<User> userManager)
        {
            _donationService = donationService;
            _userManager = userManager;
        }

        public List<Donation> DonationList { get; set; } = new();
        public int MyCount { get; set; } = 0;
        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userID = user.Id;
            DonationList = _donationService.GetDonationsByUser(userID);
            MyCount = DonationList.Count;
            return Page();
        }
    }
}
