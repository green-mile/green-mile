using Microsoft.AspNetCore.Mvc;

namespace Web.UiState
{
    public class HouseholdUiState
    {
        [BindProperty]
        public string? JoinHouseholdName { get; set; }

        [BindProperty]
        public string? CreateHouseholdName { get; set; }

        [BindProperty]
        public bool? JoinHousehold { get; set; } = true;

        [BindProperty]
        public string? Address { get; set; } = null;
    }
}
