using b1task2.Models.Balance;
using Microsoft.EntityFrameworkCore;

namespace b1task2.Context;

public sealed class ApplicationContext : DbContext
{
    public DbSet<BalanceFile> BalanceFiles => Set<BalanceFile>();
    public DbSet<BalanceSheetClass> BalanceSheetClasses => Set<BalanceSheetClass>();
    public DbSet<BalanceLineBlock> BalanceLineBlocks => Set<BalanceLineBlock>();
    public DbSet<BalanceLine> BalanceLines => Set<BalanceLine>();
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) => Database.EnsureCreated();
}