using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Models;
using Web.Services;

namespace Web.Pages.Recipes
{
    public class IndexModel : PageModel
    {

        private readonly RecipeService _recipeService;

        public IndexModel(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }
        [BindProperty]
        public List<Recipe> RecipeList { get; set; } = new();
        public void OnGet()
        {
            RecipeList = _recipeService.GetAll();
        }
    }
}
