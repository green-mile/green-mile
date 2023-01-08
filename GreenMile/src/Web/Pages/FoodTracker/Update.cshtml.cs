using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Models;
using Web.Services;

namespace Web.Pages.FoodTracker
{
    public class UpdateModel : PageModel
    {
        private readonly FoodItemService _fooditemService;
        private readonly IHttpContextAccessor _http;
        private readonly IHouseholdService _householdService;
        private readonly FoodCategoryService _foodcategoryService;
        private readonly IWebHostEnvironment _environment;

        public UpdateModel(IHttpContextAccessor http, IHouseholdService householdService, FoodItemService fooditemService, FoodCategoryService foodcategoryService, IWebHostEnvironment environment)
        {
            _http = http;
            _householdService = householdService;
            _fooditemService = fooditemService;
            _foodcategoryService = foodcategoryService;
            _environment = environment;
        }

        public static List<Category>? Categories { get; set; }
        // public static string CurrentCategory { get; set; }

        [BindProperty]
        public FoodItem? CurrentFoodItem { get; set; } = null;

        [BindProperty, Required]
        public string Category { get; set; }

        [BindProperty, Required, MinLength(1), MaxLength(20)]
        public string Name { get; set; }

        [BindProperty, Required, MinLength(0), MaxLength(100)]
        public string Description { get; set; }

        [BindProperty, Required, Range(1, 100, ErrorMessage = " Choose between 1 - 100")]
        public int Quantity { get; set; }

        [BindProperty, Required]
        public string ExpiryDate { get; set; }

        [BindProperty, Required]
        public IFormFile? ImageFilePath { get; set; }

        public IActionResult OnGet(int id)
        {
            Categories = _foodcategoryService.GetAll();

            FoodItem? currentFood = _fooditemService.GetFoodItemById(id);
            //Category = _foodcategoryService.GetCategoryByName(currentFood.Category.Name);
            

            if (currentFood is null)
            {
                return NotFound();
            }

            CurrentFoodItem = currentFood;
            Name = CurrentFoodItem.Name;
            Description = CurrentFoodItem.Description;
            Quantity = CurrentFoodItem.Quantity;
            ExpiryDate = CurrentFoodItem.ExpiryDate;
            //Category = CurrentFoodItem.Category.Name.ToString();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ImageFilePath != null)
            {
                if (ImageFilePath.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("Upload", "File size cannot exceed 2MB.");
                    return Page();
                }

                var uploadsFolder = "uploads";
                if (CurrentFoodItem.ImageFilePath != null)
                {
                    var oldImageFile = Path.GetFileName(CurrentFoodItem.ImageFilePath);
                    var oldImagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, oldImageFile);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    // Console.WriteLine("Success 1");
                }
                // Console.WriteLine("Success 2");

                var imageFile = Guid.NewGuid() + Path.GetExtension(ImageFilePath.FileName);
                var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                using var fileStream = new FileStream(imagePath, FileMode.Create);
                await ImageFilePath.CopyToAsync(fileStream);
                CurrentFoodItem.ImageFilePath = string.Format("/{0}/{1}", uploadsFolder, imageFile);
                // Console.WriteLine("Success 3");
            }

            var category = _foodcategoryService.GetCategoryByName(Category);

            CurrentFoodItem.Name = Name;
            CurrentFoodItem.Description = Description;
            CurrentFoodItem.Quantity = Quantity;
            CurrentFoodItem.ExpiryDate = ExpiryDate;
            CurrentFoodItem.Category = category;
            // Console.WriteLine("Success 4"); // Console.WriteLine($"{CurrentFoodItem.Id}");
            // Console.WriteLine($"{CurrentFoodItem.Name}");
            // Console.WriteLine($"{CurrentFoodItem.ImageFilePath}");

            _fooditemService.UpdateFoodItem(CurrentFoodItem);
            // Console.WriteLine("Success 5");
            // Console.WriteLine($"{Name}");
            // Console.WriteLine($"{ImageFilePath.ToString()}");

            return Redirect("/FoodTracker");
        }
    }
}
