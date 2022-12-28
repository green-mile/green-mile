using Microsoft.AspNetCore.Identity;

namespace Web.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Household? Household { get; set; }
}