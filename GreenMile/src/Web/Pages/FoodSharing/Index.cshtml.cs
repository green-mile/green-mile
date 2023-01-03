using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Models;
using Web.Services;

namespace Web.Pages.FoodSharing
{
    public class IndexModel : PageModel
    {
        private readonly DonationService _donationService;

        public IndexModel(DonationService donationService)
        {
            _donationService = donationService;
        }

        public List<Donation> DonationList { get; set; } = new();

        public void OnGet()
        {
            DonationList = _donationService.GetAll();
            
        }
    }
}
