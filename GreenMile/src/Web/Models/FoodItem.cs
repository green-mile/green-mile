﻿using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class FoodItem
    {

        [Key]
        public int Id { get; set; }
        public Household? Household { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ExpiryDate { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string ImageFilePath { get; set; } = string.Empty;

        public Category? Category { get; set; }


    }
}