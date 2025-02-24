using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, a => a.WithOwner());
            builder.OwnsOne(o => o.PaymentSummary, a => a.WithOwner());
            builder.Property(x => x.Status)
                .HasConversion(o => o.ToString(), o => (OrderStatus)Enum.Parse(typeof(OrderStatus),o));
            builder.Property(x=>x.Subtotal).HasColumnType("decimal(18,2)");
            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.OrderDate).HasConversion(
                d=>d.ToUniversalTime(),
                       d=>DateTime.SpecifyKind(d,DateTimeKind.Utc));
        }
    }
}
