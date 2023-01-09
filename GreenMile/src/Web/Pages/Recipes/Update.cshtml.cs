using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Models;
using Web.Services;

namespace Web.Pages.Recipes
{
    public class UpdateModel : PageModel
    {
        private readonly RecipeService _recipeService;
        private IWebHostEnvironment _webHostEnvironment;
        public UpdateModel(RecipeService recipeService, IWebHostEnvironment webHostEnvironment)
        {
            _recipeService = recipeService;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Recipe CurrentRecipe { get; set; } = new();
        public IFormFile? image { get; set; }
        public List<String> ingredients { get; set; } = new();


        public IActionResult OnGet(string id)
        {
            Recipe? recipe = _recipeService.GetRecipeById(id);
            if(recipe == null)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = "Recipe not found!";
                return Redirect("/Recipes/BackendView");
            }
            CurrentRecipe = recipe;
            //get ingredients here
            return Page();


        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            { 
                if (image != null)
                {

                    var imagesFolder = "uploads";
                    var imageFile = Guid.NewGuid() + Path.GetExtension(image.FileName);
                    var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", imagesFolder, imageFile);
                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    image.CopyTo(fileStream);
                    CurrentRecipe.imageFilePath = String.Format("/" + imagesFolder + "/" + imageFile);

                }
                _recipeService.UpdateRecipe(CurrentRecipe);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = CurrentRecipe.recipeName + " successfully updated!";
                return Redirect("/Recipes/Index");
            }
            return Page();
        }
    }
}
