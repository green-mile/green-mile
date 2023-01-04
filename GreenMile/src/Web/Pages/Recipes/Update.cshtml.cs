using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Models;
using Web.Services;

namespace Web.Pages.Recipes
{
    public class UpdateModel : PageModel
    {
        private readonly RecipeService _recipeService;
        public UpdateModel(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [BindProperty]
        public Recipe CurrentRecipe { get; set; } = new();
        public IActionResult OnGet(string id)
        {
            Recipe? recipe = _recipeService.GetRecipeById(id);
            if(recipe == null)
            {
                TempData["Flash.Type"] = "danger";
                TempData["Flash.Text"] = "Recipe not found!";
                return Redirect("/Recipes/BackendView");
            }
            CurrentRecipe = recipe;
            return Page();


        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _recipeService.UpdateRecipe(CurrentRecipe);
                TempData["Flash.Type"] = "success";
                TempData["Flash.Text"] = CurrentRecipe.recipeName + " successfully updated!";
                return Redirect("/Recipes/Index");
            }
            return Page();
        }
    }
}
