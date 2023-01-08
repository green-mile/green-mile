using Web.Data;
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

        public async Task<FoodItem?> GetFoodItemById(int id)
        {
            return await _context.FoodItems.FindAsync(id);
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

        public async Task Update(FoodItem item)
        {
            _context.FoodItems.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
