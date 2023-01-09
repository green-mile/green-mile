using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Models;
using Web.Services;

namespace Web.Pages.FoodTracker;

public class UpdateModel : PageModel
{
    private readonly IHostEnvironment _environment;
    private readonly FoodItemService _foodItemService;

    public UpdateModel(FoodItemService foodItemService, IHostEnvironment environment)
    {
        _foodItemService = foodItemService;
        _environment = environment;
    }

    [BindProperty]
    public FoodItem FoodItem { get; set; }

    [BindProperty]
    public string? Name { get; set; }

    [BindProperty]
    public string? Description { get; set; }

    [BindProperty]
    public int Quantity { get; set; }

    [BindProperty]
    public IFormFile? Image { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        FoodItem = await _foodItemService.GetFoodItemById(id);

        if (FoodItem is null)
        {
            return NotFound();
        }

        Name = FoodItem.Name;
        Description = FoodItem.Description;
        Quantity = FoodItem.Quantity;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (FoodItem is null)
        {
            return NotFound();
        }

        if (Image is not null)
        {
            var uploadFolder = "Uploads";
            var imageFile = Guid.NewGuid() + Path.GetExtension(Image.FileName);
            var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadFolder, imageFile);
            using var fileStream = new FileStream(imagePath, FileMode.Create);
            await Image.CopyToAsync(fileStream);
            FoodItem.ImageFilePath = string.Format("/{0}/{1}", uploadFolder, imageFile);
        }

        FoodItem.Name = Name ?? FoodItem.Name;
        FoodItem.Description = Description ?? FoodItem.Description;
        FoodItem.Quantity = Quantity;

        await _foodItemService.Update(FoodItem);

        return Redirect("/Index");
    }
}
