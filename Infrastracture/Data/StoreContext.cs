using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastracture.Data;
public class StoreContext : IdentityDbContext<AppUser>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<DeliveryMethod> Deliveries { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }

    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }

}