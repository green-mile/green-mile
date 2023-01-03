
namespace MyCompany.Services
{
    public class CustomFoodService
    {
        private readonly DataContext _context;

        public CustomFoodService(context)
        {
            _context = context;
        }

        public void AddCustomFood(CustomFood customfood)
        {
            _context.CustomFoods.add(customfood);
            _context.savechanges();
        }

    }
}
