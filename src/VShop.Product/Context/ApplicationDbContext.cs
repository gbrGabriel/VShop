using System.Reflection;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer("DefaultConnection");
            
        base.OnConfiguring(optionsBuilder);
    }
    public override void Dispose()
    {
        base.Dispose();
        GC.SuppressFinalize(this);
    }
}
