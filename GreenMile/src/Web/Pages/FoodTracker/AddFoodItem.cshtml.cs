using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Data;
using Web.Lib;
using Web.Models;
using Web.Services;

namespace Web.Pages.FoodTracker
{
    public class AddFoodItemModel : PageModel
    {

        private readonly FoodItemService _fooditemService;

        private readonly IHttpContextAccessor _http;
        private readonly IHouseholdService _householdService;
        private readonly FoodCategoryService _foodcategoryService;
        private IWebHostEnvironment _environment;

        public AddFoodItemModel(IHttpContextAccessor http, IHouseholdService householdService, FoodItemService fooditemService, IWebHostEnvironment environment, FoodCategoryService foodCategoryService)
        {
            _http = http;
            _householdService = householdService;
            _fooditemService = fooditemService;
            _environment = environment;
            _foodcategoryService = foodCategoryService;
        }

        public static List<Category> Categories { get; set; } = new();

        [BindProperty, Required]
        public string Category { get; set; }

        //[BindProperty, Required]
        //public string userHousedhold { get; set; }
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


        public void OnGet()
        {
            Categories = _foodcategoryService.GetAll();

        }


        public async Task<IActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {
                if (ImageFilePath != null)
                {
                    if (ImageFilePath.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("Upload", "File size cannot exceed 2MB.");
                        return Page();
                    }

                    var uploadsFolder = "uploads";
                    var imageFile = Guid.NewGuid() + Path.GetExtension(ImageFilePath.FileName);
                    var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    await ImageFilePath.CopyToAsync(fileStream);
                    var ImageURL = string.Format("/{0}/{1}", uploadsFolder, imageFile);

                    var householdName = _http.HttpContext?.Session.GetString(SessionVariable.HousholdName);
                    var household = (await _householdService.RetrieveHouseholdDetailsByName(householdName)).Value;
                    var category = _foodcategoryService.GetCategoryByName(Category);
                    var newfood = new FoodItem()
                    {
                        Household = household,
                        Name = Name,
                        Description = Description,
                        Quantity = Quantity,
                        ExpiryDate = ExpiryDate,
                        ImageFilePath = ImageURL,
                        Category = category

                    };

                    _fooditemService.AddFoodItem(newfood);
                    return Redirect("/FoodTracker");
                }

                return Page();

            }
            return Page();

        }
    }

}