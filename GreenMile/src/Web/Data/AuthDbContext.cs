using System.Reflection.Emit;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Web.Models;

namespace Web.Data;

public class AuthDbContext : IdentityDbContext<User>
{
    public DbSet<Household> Household { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Household>().HasMany(household => household.Users).WithOne(user => user.Household); //explicitely mention foreign key
        builder.Entity<User>()
            .HasOne(u => u.OwnerOf)
            .WithOne(h => h.Owner)
            .HasForeignKey<Household>(h => h.OwnerId).IsRequired(false);
    }

    public AuthDbContext(DbContextOptions options) : base(options)
    {
    }
}