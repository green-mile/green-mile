using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace Web.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? HouseholdId { get; set; } = null;
    public Household? Household { get; set; } = null;
    public bool Disabled { get; set; } = false;
    public Household? OwnerOf { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    [MaxLength(100)]
    public string? ImageURL { get; set; } 


}