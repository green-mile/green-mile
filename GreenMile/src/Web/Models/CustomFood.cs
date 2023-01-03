using System;

namespace Web.Models;

public class CustomFood
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public DateTime ExpiredDate { get; set; }
    public Donation? Donation { get; set; }
}
