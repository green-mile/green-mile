using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Models;
using Web.Services;

namespace Web.Pages.Recipes
{
    public class BackendViewModel : PageModel
    {

        private readonly RecipeService _recipeService;

       public BackendViewModel(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [BindProperty]
        public List<Recipe> RecipeList { get; set; } = new();
        public void OnGet()
        {
            RecipeList = _recipeService.GetAll();
        }

        public PageResult OnGetDelete(string id)
        {
            Recipe? recipe = _recipeService.GetRecipeById(id);
            _recipeService.DeleteRecipe(recipe);
            TempData["Flash.Type"] = "success";
            TempData["Flash.Text"] = id + " successfully Deleted!";
            RecipeList = _recipeService.GetAll();
            return Page();
        }

    }
}
