using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Web.Models
{
    public class Recipe
    {
        [Required, MaxLength(20, ErrorMessage = "Recipe name too long!")]
        [Key]
        public string recipeName { get; set; } = string.Empty;

        [Required]
        public string ingredients { get; set; } = string.Empty; //convert to string before sending to the database
        //it is a string of ids separated by ","
        [Required]

        public string ingredientAmount { get; set; } = string.Empty; //same as before, match index 1 to 1

        [Required]
        public int duration { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(2,1)")]
        public decimal difficulty { get; set; } = 0;

        public string reviews { get; set; } = string.Empty;

        //image?

    }
}
