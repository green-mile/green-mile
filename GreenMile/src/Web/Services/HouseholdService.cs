using Microsoft.AspNetCore.Identity;

using Web.Data;
using Web.Models;
using Web.Utils;

namespace Web.Services
{
    public class HouseholdService : IHouseholdService
    {
        private readonly UserManager<User> _userManager;
        private readonly AuthDbContext _authDbContext;
        public HouseholdService(UserManager<User> userManager, AuthDbContext authDbContext)
        {
            _userManager = userManager;
            _authDbContext = authDbContext;
        }

    

       async Task<Result<Tuple<User, Household>>> IHouseholdService.addUserToHousehold(string userId, int householdId)
        {
           Household? household = await _authDbContext.Household.FindAsync(householdId);
           User? user = await _userManager.FindByIdAsync(userId.ToString());
           if (household is null)
           {
                return Result<Tuple<User, Household>>.Failure("Household was not found");
           }
           if (user is null)
           {
                return Result<Tuple<User, Household>>.Failure("User was not found");  
           }
          
            household.Users.Add(user);
            await _authDbContext.SaveChangesAsync();
           
            return Result<Tuple<User, Household>>.Success("User has been added to the household!", new Tuple<User, Household> (user, household));
           

       }



        async Task<Result<Household>> IHouseholdService.createHousehold(Household household)
        {
            await _authDbContext.Household.AddAsync(household);
            await _authDbContext.SaveChangesAsync();
            return Result<Household>.Success("Household has been created!", household);
        }

      

        async Task<Result<User>> IHouseholdService.removeUserFromHousehold(string userId)
        {
            User? user = await _userManager.FindByIdAsync(userId);
            if(user is null)
            {
                return Result<User>.Failure("User was not found");
            }
            user.Household = null;
            
             return Result<User>.Success("User was found and household as been removed!", user);
            
           
        }

    

        async Task<Result<Household>> IHouseholdService.retrieveHouseholdDetails(int householdId)
        {
            Household? household = await _authDbContext.Household.FindAsync(householdId);
            if(household is null)
            {
                return Result<Household>.Failure("Household was not found");
            }
            return Result<Household>.Success("Household was found!", household);
           
        }
    }
}
