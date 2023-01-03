using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class FoodItem
    {

        [Key]
        public string Id { get; set; } 

        public string Household { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }

        public string ExpiryDate { get; set; }

        
    }
}
