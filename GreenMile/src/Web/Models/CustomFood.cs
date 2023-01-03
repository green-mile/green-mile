using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class CustomFood
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [MaxLength(50)]
    public string? Image { get; set; }
    

    [Required]
    [DataType(DataType.Date)]
    public DateTime ExpiredDate { get; set; } = DateTime.Today;


}
