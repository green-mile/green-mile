using Microsoft.AspNetCore.Identity;

namespace Web.Services
{
    public interface IHouseholdService
    {
        public void createHousehold();
        public void addUserToHousehold();
        public void retrieveCurrentHouseholdDetails();
        public void removeUserFromHousehold();
    }
}
