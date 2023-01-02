using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class FoodItem
    {

        [Key]
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }

        public DateOnly ExpiryDate { get; set; } = new DateOnly();

    }
}
