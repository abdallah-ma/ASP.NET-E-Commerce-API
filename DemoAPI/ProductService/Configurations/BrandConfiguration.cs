using ProductService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoAPI.Repository.Data.Configurations
{
    internal class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
        
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name);

        }


    }
    
}
