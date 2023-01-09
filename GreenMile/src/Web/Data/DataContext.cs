using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Web.Models;

namespace Web.Data;

public class DataContext : IdentityDbContext<User>
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<CustomFood> CustomFoods { get; set; }
    public DbSet<Donation> Donations { get; set; }
    public DbSet<FoodItem> FoodItems { get; set; }
    public DbSet<FoodItemCategory> FoodItemCategories { get; set; }
    public DbSet<Household> Household { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Recipe> Recipes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Household>().HasMany(household => household.Users).WithOne(user => user.Household); //explicitely mention foreign key
        builder.Entity<User>()
    .HasOne(u => u.OwnerOf)
    .WithOne(h => h.Owner)
    .HasForeignKey<Household>(h => h.OwnerId).IsRequired(false);
    }

    public DataContext(DbContextOptions options) : base(options)
    {
    }
}