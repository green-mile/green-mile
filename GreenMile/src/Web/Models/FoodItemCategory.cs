using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class FoodItemCategory
{
    [Key]
    public int Id { get; set; }
    public FoodItem? FoodItem { get; set; }
    public Category? Category { get; set; }
}