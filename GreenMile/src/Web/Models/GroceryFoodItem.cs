namespace Web.Models
{
    public class GroceryFoodItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string ExtraNote { get; set; }
        public Boolean InBasket { get; set; }
        public double CarbonFootprintSum { get; set; }
    }
}
