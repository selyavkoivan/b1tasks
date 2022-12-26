using b1.Model;
using Microsoft.EntityFrameworkCore;

namespace b1.Context;

public sealed class ApplicationContext : DbContext
{
    public DbSet<B1RandomObject> RandomObjects => Set<B1RandomObject>();

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) => Database.EnsureCreated();
    
    public ApplicationContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(
            "Server=DESKTOP-1OH4LVK\\SQLEXPRESS;Database=clotheshop;Trusted_Connection=True;TrustServerCertificate=true");
}