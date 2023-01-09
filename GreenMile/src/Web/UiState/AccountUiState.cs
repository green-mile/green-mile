using System.ComponentModel.DataAnnotations;

using Web.Models;
namespace Web.UiState
{
    public class AccountUiState
    {
        public string? Tab { get; set; } = "dashboard";
        [Required]
        public string? Username { get; set; } = null;
        [Required]
        public string? FirstName { get;set; } = null;
        [Required]
        public string? LastName { get; set; } = null;
        [Required]
        public string? Password { get; set; } = null;
        public string? ConfirmPassword { get; set; } = null;
        public string? NewPassword { get; set; } = null;
        [Required]
        public string? EmailAddress { get; set; } = null;
        public Household? Household { get; set; } = null;
        public string? DeletePassword { get; set; } = null;
        public string? DeleteUsername { get; set; } = null;

        public IFormFile? Upload { get; set; }

      
        public bool HasImageChanged { get; set; } = false;



    }
}
