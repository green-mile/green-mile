using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Web.Models;

namespace Web.Data;

public class DataContext : IdentityDbContext<User>
{
    public DbSet<Household> Household { get; set; }
    public DbSet<FoodItem> FoodItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<FoodItemCategory> FoodItemCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Household>().HasMany(household => household.Users).WithOne(user => user.Household); //explicitely mention foreign key
    }

    public DataContext(DbContextOptions options) : base(options)
    {
    }
}