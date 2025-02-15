
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p=>p.Id);
        builder.Property(p=>p.Price)
               .HasColumnName("decimal(182,)");
        builder.Property(x => x.Description)
               .HasColumnType("VARCHAR")
               .HasMaxLength(100)
               .IsRequired();    
        builder.Property(x => x.Name)
        
               .HasColumnType("VARCHAR")
               .HasMaxLength(20)
               .IsRequired();    
    }
}