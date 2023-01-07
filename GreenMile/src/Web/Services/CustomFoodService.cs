
using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

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

        public void UpdateCustomFood(CustomFood customfood)
        {
            _context.CustomFoods.Update(customfood);
            _context.SaveChanges();
        }

        public List<Donation> GetDonationsByUser(string id)
        {
            return _context.Donations
                .Include(d => d.CustomFood)
                .Where(x => x.User.Id.Equals(id))
                .OrderByDescending(m => m.Date)
                .ToList();
        }


    }
}
