using Web.Models;

namespace Web.UiState
{
    public class HouseholdDetailsUiState
    {
        public ICollection<User>? Users { get; set; }
        public string? UserRemoveId { get; set; }
        public string?  NextOwnerId { get; set; }
    }
}
