using OrderService.Models;
using Microsoft.EntityFrameworkCore;


namespace OrderService.Data.Configurations
{
    internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DeliveryMethod> builder)
        {
            
            builder.Property(D => D.Cost).HasColumnType("decimal(6,2)");


        }


    }
}
