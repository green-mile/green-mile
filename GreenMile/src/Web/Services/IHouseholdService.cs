using Microsoft.AspNetCore.Identity;

using Web.Models;
using Web.Utils;

namespace Web.Services
{
    public interface IHouseholdService
    {
        public Task<Result<Household>> CreateHousehold(string householdName);
        public Task<Result<Tuple<User, Household>>> AddUserToHousehold(string userId, int householdId);
        public Task<Result<Household>> RetrieveHouseholdDetails(int householdId);
        public Task<Result<User>> RemoveUserFromHousehold(string userId);
        public Task<Result<Household>> RetrieveHouseholdDetailsByName(string householdName);
    }
}
