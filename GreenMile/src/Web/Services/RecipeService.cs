using Microsoft.EntityFrameworkCore;

using Web.Data;
using Web.Models;

namespace Web.Services
{
    public class RecipeService
    {
        private readonly DataContext _dataContext;

        public RecipeService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        //public static List<Recipe> AllRecipes = new()
        //{
        //    new Recipe{ recipeName = "test recipe 1", ingredients = "0", ingredientAmount = "500", difficulty = 2, duration = 15 },
        //    new Recipe{ recipeName = "test recipe 2", ingredients = "0", ingredientAmount = "500", difficulty = 4, duration = 30 }
        //};

        public List<Recipe> GetAll()
        {
            return _dataContext.Recipes.ToList(); //from db
            //return AllRecipes.ToList(); //test data
        }

        //public async Task<List<Recipe>> GetAll()
        //{
        //    return await _dataContext.Recipes.ToListAsync();
        //}
        public Recipe? GetRecipeById(string id) //asp-route-id
        {
            Recipe? recipe = _dataContext.Recipes.FirstOrDefault(r => r.recipeName.Equals(id)); //from db
            //Recipe? recipe = AllRecipes.FirstOrDefault(r => r.recipeName.Equals(id)); //test data
            return recipe;
        }
        //public async Task<Recipe?> GetRecipeById(string id)
        //{
        //    Recipe? recipe = await _dataContext.Recipes.FirstOrDefaultAsync(r => r.recipeName.Equals(id));
        //    return recipe;
        //}
        public void AddRecipe(Recipe recipe)
        {
            _dataContext.Recipes.Add(recipe);
            _dataContext.SaveChanges();
        }

        //public async Task<Recipe> AddRecipe(Recipe recipe)
        //{
        //    await _dataContext.Recipes.AddAsync(recipe);
        //    await _dataContext.SaveChangesAsync();
        //}
        public void UpdateRecipe(Recipe recipe)
        {
            _dataContext.Recipes.Update(recipe);
            _dataContext.SaveChanges();
        }
        public void DeleteRecipe(Recipe recipe)
        {
            _dataContext.Recipes.Remove(recipe);
            _dataContext.SaveChanges();
        }
    }
}
