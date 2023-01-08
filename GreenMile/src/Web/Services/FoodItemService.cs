using System.Linq;

using Web.Data;
using Web.Lib;
using Web.Models;

namespace Web.Services
{
    public class FoodItemService
    {

        private readonly IHttpContextAccessor _http;
        private readonly IHouseholdService _householdService;


        private readonly DataContext _context;
        public FoodItemService(IHttpContextAccessor http, IHouseholdService householdService,DataContext context)
        {
            _context = context;
            _http = http;
            _householdService = householdService;
        }


        public List<FoodItem> GetAll(Household household)
        {
            

            return _context.FoodItems.Where(x => x.Household.Equals(household)).ToList();

        }

        public FoodItem? GetFoodItemByHousehold(string household )
        {
            FoodItem? fooditem = _context.FoodItems.FirstOrDefault(x => x.Household.Equals(household));

            return fooditem;
        }

        public FoodItem? GetFoodItemById(int id)
        {


            return _context.FoodItems.FirstOrDefault(x => x.Id.Equals(id)); ;
        }

        public void AddFoodItem(FoodItem fooditem)
        {
            _context.FoodItems.Add(fooditem);
            _context.SaveChanges();
        }
        public void UpdateFoodItem(FoodItem fooditem)
        {
            _context.FoodItems.Update(fooditem);
            _context.SaveChanges();
        }
        public void DeleteFoodItem(FoodItem fooditem)
        {
            _context.FoodItems.Remove(fooditem);
            _context.SaveChanges();
        }



    }
}
