
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p=>p.Id);
        builder.Property(p=>p.Price)
                .HasColumnName("Price")
               .HasColumnType("decimal(18,2)");
        builder.Property(x => x.Description)
               .HasColumnType("VARCHAR")
               .HasMaxLength(255)
               .IsRequired();    
        builder.Property(x => x.Name)
        
               .HasColumnType("VARCHAR")
               .HasMaxLength(255)
               .IsRequired();    
    }
}