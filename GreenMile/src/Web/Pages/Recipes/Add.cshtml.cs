using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Models;
using Web.Services;

namespace Web.Pages.Recipes
{
    public class AddModel : PageModel
    {
        private readonly RecipeService _recipeService;

        public AddModel(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [BindProperty]
        public Recipe CurrentRecipe { get; set; } = new();

        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                Recipe? recipe = _recipeService.GetRecipeById(CurrentRecipe.recipeName);
                if(recipe != null)
                {
                    TempData["Flash.Type"] = "danger";
                    TempData["Flash.Text"] = CurrentRecipe.recipeName + " already exists!";
                    return Page();
                }
                _recipeService.AddRecipe(CurrentRecipe);
                TempData["Flash.Type"] = "success";
                TempData["Flash.Text"] = CurrentRecipe.recipeName + " successfully added!";
                return Redirect("/Recipes/Index");
            }
            return Page();
        }
    }
}
