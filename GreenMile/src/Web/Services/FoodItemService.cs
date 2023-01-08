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

<<<<<<< HEAD

        public List<FoodItem> GetAll(Household household)
        {
            

            return _context.FoodItems.Where(x => x.Household.Equals(household)).ToList();

=======
        public IEnumerable<FoodItem> GetAll(Household household)
        {
            return _context.FoodItems
                .Where(f => f.Household == household)
                .ToList();
>>>>>>> 26b665f86e3f69afc3f8516503f69e14f5b03d50
        }

        public async Task<FoodItem?> GetFoodItemById(int id)
        {
<<<<<<< HEAD
            FoodItem? fooditem = _context.FoodItems.FirstOrDefault(x => x.Household.Equals(household));

            return fooditem;
        }

        public FoodItem? GetFoodItemById(int id)
        {


            return _context.FoodItems.FirstOrDefault(x => x.Id.Equals(id)); ;
=======
            return await _context.FoodItems.FindAsync(id);
>>>>>>> 26b665f86e3f69afc3f8516503f69e14f5b03d50
        }

        public void AddFoodItem(FoodItem fooditem)
        {
            _context.FoodItems.Add(fooditem);
            _context.SaveChanges();
        }
<<<<<<< HEAD
        public void UpdateFoodItem(FoodItem fooditem)
        {
            _context.FoodItems.Update(fooditem);
            _context.SaveChanges();
        }
=======
        
>>>>>>> 26b665f86e3f69afc3f8516503f69e14f5b03d50
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
