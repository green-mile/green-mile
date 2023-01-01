using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Web.Data;
using Web.Models;
using Web.Utils;

namespace Web.Services
{
    public class HouseholdService : IHouseholdService
    {
        private readonly UserManager<User> _userManager;
        private readonly DataContext _authDbContext;
        public HouseholdService(UserManager<User> userManager, DataContext authDbContext)
        {
            _userManager = userManager;
            _authDbContext = authDbContext;
        }


       async Task<Result<Tuple<User, Household>>> IHouseholdService.AddUserToHousehold(string userId, int householdId)
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



        async Task<Result<Household>> IHouseholdService.CreateHousehold(string householdName)
        {
            if((await _authDbContext.Household.FirstOrDefaultAsync(x => x.Name == householdName)) is null)
            {
                Household householdObj =new Household() {
                    Name= householdName
                };

                await _authDbContext.Household.AddAsync(householdObj);
                await _authDbContext.SaveChangesAsync();
                return Result<Household>.Success("Household has been created!", householdObj);
            }
            return Result<Household>.Failure("Household exists!");
           
        }

      

        async Task<Result<User>> IHouseholdService.RemoveUserFromHousehold(string userId)
        {
            User? user = await _userManager.FindByIdAsync(userId);
            if(user is null)
            {
                return Result<User>.Failure("User was not found");
            }
            user.Household = null;
            
             return Result<User>.Success("User was found and household as been removed!", user);
           
        }

        public async Task<Result<Household>> RetrieveHouseholdDetails(int householdId)
        {
            Household? household = await _authDbContext.Household
                .Include(h => h.Users)
                .FirstOrDefaultAsync(h => h.HouseholdId == householdId);
            return household is null
                ? Result<Household>.Failure("Household was not found")
                : Result<Household>.Success("Household exists!", household);
        }

        public async Task<Result<Household>> RetrieveHouseholdDetailsByName(string householdName)
        {

            Household? household = await _authDbContext.Household
                .Include(h => h.Users)
                .FirstOrDefaultAsync(x => x.Name == householdName);

            return household is null
                ? Result<Household>.Failure("Household was not found")
                : Result<Household>.Success("Household exists!", household);
        }
    }
}
