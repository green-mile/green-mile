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


        public List<FoodItem> GetAll()
        {
            var userHousedhold = _http.HttpContext?.Session.GetString(SessionVariable.HousholdName);

            return _context.FoodItems.Where(x => x.Household.Equals(userHousedhold)).ToList();

        }

        public FoodItem? GetFoodItemByHousehold(string household )
        {
            FoodItem? fooditem = _context.FoodItems.FirstOrDefault(x => x.Household.Equals(household));

            return fooditem;
        }

        public FoodItem? GetFoodItemById(string id)
        {
            FoodItem? fooditem = _context.FoodItems.FirstOrDefault(x => x.Id.Equals(id));

            return fooditem;
        }

        public void AddFoodItem(FoodItem fooditem)
        {
            _context.FoodItems.Add(fooditem);
            _context.SaveChanges();
        }
        public void DeleteFoodItem(FoodItem fooditem)
        {
            _context.FoodItems.Remove(fooditem);
            _context.SaveChanges();
        }



    }
}
