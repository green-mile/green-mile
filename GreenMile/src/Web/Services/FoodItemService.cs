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


        public List<FoodItem> GetAll()
        {
            return _context.FoodItems.ToList();
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


    }
}
