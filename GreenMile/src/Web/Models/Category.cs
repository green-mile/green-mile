using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
<<<<<<< HEAD

    public List<FoodItem>? FoodItems { get; set; }

=======
>>>>>>> 26b665f86e3f69afc3f8516503f69e14f5b03d50
}