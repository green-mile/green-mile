using Microsoft.AspNetCore.Identity;

using Web.Models;
using Web.Utils;

namespace Web.Services
{
    public interface IHouseholdService
    {
        public Task<Result<Household>> createHousehold(Household household);
        public Task<Result<Tuple<User, Household>>> addUserToHousehold(string userId, int householdId);
        public Task<Result<Household>> retrieveHouseholdDetails(int householdId);
        public Task<Result<User>> removeUserFromHousehold(string userId);
        //public Task updateHouseholdDetails(int householdId, Household household);
    }
}
