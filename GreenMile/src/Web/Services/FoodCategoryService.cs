using Web.Data;
using Web.Models;

namespace Web.Services
{
    public class FoodCategoryService
    {

        private readonly DataContext _context;
        public FoodCategoryService(DataContext context)
        {
            _context = context;
            
        }


        public List<Category> GetAll()
        {
            //var userHousedhold = _http.HttpContext?.Session.GetString(SessionVariable.HousholdName);

            return _context.Categories.ToList();

        }
        public Category? GetCategoryByName(string category)
        {


            return _context.Categories.FirstOrDefault(x => x.Name.Equals(category)); 
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
