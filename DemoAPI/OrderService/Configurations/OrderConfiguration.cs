using OrderService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderService.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {

        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.OwnsOne(O => O.ShippingAddress, ShippingAdress => ShippingAdress.WithOwner());

            builder.Property(O => O.OrderStatus).HasConversion(
                    v => v.ToString(),
                    v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v) );

            builder
            .HasMany(o => o.Items)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        }


    }
}
