using Domain.Models;
using Domain.Models.Person;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataBase;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions opt) : base(opt)
    {
    }

    public DbSet<Post> Posts { get; set; }

    public DbSet<PersonModel> PersonModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonModel>()
            .OwnsOne(userprofile => userprofile.UserInfo, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
                ownedNavigationBuilder.OwnsOne(userinfo => userinfo.Name);
                ownedNavigationBuilder.OwnsMany(userinfo => userinfo.Address);
            });
    }
}