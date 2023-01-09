namespace Web.Models
{
    public class Template
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<GroceryFoodItem> GroceryFoodItems { get; set; }

    }
}
