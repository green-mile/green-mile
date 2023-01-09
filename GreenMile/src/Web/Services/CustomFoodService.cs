
using System.ComponentModel.DataAnnotations;

using Web.Data;
using Web.Models;

namespace Web.Services
{
    public class CustomFoodService
    {
        private readonly DataContext _context;

        public CustomFoodService(DataContext context)
        {
            _context = context;
        }

        public void AddCustomFood(CustomFood customfood)
        {
            _context.CustomFoods.Add(customfood);
            _context.SaveChanges();
        }
        public List<CustomFood> GetAll()
        {
            return _context.CustomFoods.OrderBy(m => m.Id).ToList();
        }


    }
}
