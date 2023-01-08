using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Models;
using Web.Services;

namespace Web.Pages.Recipes
{
    public class AddModel : PageModel
    {
        private readonly RecipeService _recipeService;
        private IWebHostEnvironment _webHostEnvironment;

        public AddModel(RecipeService recipeService, IWebHostEnvironment webHostEnvironment)
        {
            _recipeService = recipeService;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Recipe CurrentRecipe { get; set; } = new();
        public IFormFile? image { get; set; }

        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                if (image != null)
                {

                    var imagesFolder = "uploads";
                    var imageFile = Guid.NewGuid() + Path.GetExtension(image.FileName);
                    var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath,"wwwroot",imagesFolder, imageFile);
                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    image.CopyTo(fileStream);
                    CurrentRecipe.imageFilePath = String.Format("/" + imagesFolder + "/" + imageFile);

                }
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
