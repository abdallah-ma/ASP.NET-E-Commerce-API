using Microsoft.EntityFrameworkCore;
using ProductService.Models;

namespace DemoAPI.Repository.Data.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        }

    }
}
