using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Orders_Aggregate;

namespace Talabat.Repository.Data.Configrations
{
    public class OrderConfig : IEntityTypeConfiguration<Core.Entities.Orders_Aggregate.Order>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Orders_Aggregate.Order> builder)
        {
            builder.Property(o => o.Status)
                   .HasConversion(
                 oStatus => oStatus.ToString(),
                 oStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), oStatus)
                );

            builder.Property(o => o.SubTotal)
                   .HasColumnType("decimal(18,2)");
            builder.OwnsOne(o => o.ShppingAddress, ShppingAddress => ShppingAddress.WithOwner());

            // builder.HasOne(o => o.DeliveryMethod)
            // .WithOne();
            //builder.HasIndex(o => o.DeliveryMethodId).IsUnique();

            builder.HasOne(o => o.DeliveryMethod)
                   .WithMany()
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
