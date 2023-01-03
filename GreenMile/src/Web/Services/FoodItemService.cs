using System.Linq;

using Web.Data;
using Web.Lib;
using Web.Models;

namespace Web.Services
{
    public class FoodItemService
    {
        private readonly DataContext _context;

        public FoodItemService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<FoodItem> GetAll(Household household)
        {
            return _context.FoodItems
                .Where(f => f.Household == household)
                .ToList();
        }

        public FoodItem? GetFoodItemById(string id)
        {
            return _context.FoodItems
                .FirstOrDefault(x => x.Id.Equals(id));
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
