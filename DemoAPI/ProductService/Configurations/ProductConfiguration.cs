using ProductService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace DemoAPI.Repository.Data.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {


       public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
                
            builder.Property(p => p.Price).IsRequired();

            builder.Property(p => p.PictureUrl).IsRequired();

            builder.HasOne(p => p.Brand).WithMany()
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Category).WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
