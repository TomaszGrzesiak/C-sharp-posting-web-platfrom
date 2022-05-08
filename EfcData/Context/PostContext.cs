using Domain.ModelClasses;
using Microsoft.EntityFrameworkCore;

namespace EfcData;

public class PostContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../BusinessLogicWithRestApi/Assignment3.db");
    }
}