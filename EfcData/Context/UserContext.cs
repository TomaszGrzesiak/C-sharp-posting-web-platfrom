using Domain.ModelClasses;
using Microsoft.EntityFrameworkCore;

namespace EfcData;

public class UserContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../BusinessLogicWithRestApi/Assignment3.db");
    }
    
    public void Seed()
    {
        if (Users.Any()) return;

        User[] sampleUsers =
        {
            new User("tomaszg", "tomaszg"),
            new User("maciejg", "maciejg"),
            new User("bill","gates"),
            new User("elon", "musk"),
            new User("steve","jobs")
        };
        Users.AddRange(sampleUsers);
        SaveChanges();
    }
}