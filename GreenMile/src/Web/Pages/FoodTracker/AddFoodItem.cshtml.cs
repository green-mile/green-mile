using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Web.Models;
using Web.Services;

namespace Web.Pages.FoodTracker
{
    public class AddFoodItemModel : PageModel
    {
<<<<<<< HEAD

=======
>>>>>>> 26b665f86e3f69afc3f8516503f69e14f5b03d50
        private readonly FoodItemService _fooditemService;
        private readonly UserManager<User> _userManager;
        private readonly IHostEnvironment _environment;

<<<<<<< HEAD
        private readonly IHttpContextAccessor _http;
        private readonly IHouseholdService _householdService;
        private readonly FoodCategoryService _foodcategoryService;
        private IWebHostEnvironment _environment;

        public AddFoodItemModel(IHttpContextAccessor http, IHouseholdService householdService, FoodItemService fooditemService, IWebHostEnvironment environment, FoodCategoryService foodCategoryService)
=======
        public AddFoodItemModel(FoodItemService fooditemService, UserManager<User> userManager, IHostEnvironment environment)
>>>>>>> 26b665f86e3f69afc3f8516503f69e14f5b03d50
        {
            _fooditemService = fooditemService;
<<<<<<< HEAD
            _environment = environment;
            _foodcategoryService = foodCategoryService;
        }

        public static List<Category> Categories { get; set; } = new();

        [BindProperty, Required]
        public string Category { get; set; }

        //[BindProperty, Required]
        //public string userHousedhold { get; set; }
=======
            _userManager = userManager;
            _environment = environment;
        }
        
>>>>>>> 26b665f86e3f69afc3f8516503f69e14f5b03d50
        [BindProperty, Required, MinLength(1), MaxLength(20)]
        public string Name { get; set; }
        [BindProperty, Required, MinLength(0), MaxLength(100)]
        public string Description { get; set; }
        [BindProperty, Required, Range(1, 100, ErrorMessage = " Choose between 1 - 100")]
        public int Quantity { get; set; }
        [BindProperty, Required]
<<<<<<< HEAD
        public string ExpiryDate { get; set; }

        [BindProperty, Required]
        public IFormFile? ImageFilePath { get; set; }


        public void OnGet()
        {
            Categories = _foodcategoryService.GetAll();

        }
=======
        public DateTime ExpiryDate { get; set; }

        [BindProperty, Required]
        public IFormFile Image { get; set; }
>>>>>>> 26b665f86e3f69afc3f8516503f69e14f5b03d50

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
<<<<<<< HEAD

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
=======
            if (!ModelState.IsValid)
            {
                return Page();
            }
>>>>>>> 26b665f86e3f69afc3f8516503f69e14f5b03d50

            var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            var user = await _userManager.Users
                .Include(u => u.Household)
                .FirstAsync(u => u.Id == userId);

            var household = user.Household;

            var newFood = new FoodItem()
            {
                Household = household,
                Name = Name,
                Description = Description,
                Quantity = Quantity,
                ExpiryDate = ExpiryDate
            };

            var uploadFolder = "Uploads";
            var imageFile = Guid.NewGuid() + Path.GetExtension(Image.FileName);
            var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadFolder, imageFile);
            using var fileStream = new FileStream(imagePath, FileMode.Create);
            await Image.CopyToAsync(fileStream);
            newFood.ImageFilePath = string.Format("/{0}/{1}", uploadFolder, imageFile);

            _fooditemService.AddFoodItem(newFood);
            return Redirect("/FoodTracker");
        }
    }

}