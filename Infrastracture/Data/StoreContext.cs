using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastracture.Data;
public class StoreContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }

    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }

}