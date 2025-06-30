using OrderService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderService.Models
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {

        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {

            builder.OwnsOne(O => O.Product, P => P.WithOwner());

            builder.Property(O => O.Price).HasColumnType("decimal(6,2)");


        }


    }
}
