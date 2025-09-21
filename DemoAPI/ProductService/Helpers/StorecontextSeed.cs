using System.Text.Json;
using ProductService.Models;
using System.IO;

namespace ProductService.Helpers
{
    public class StorecontextSeed
    {
        public async static Task SeedAsync(AppDbContext _dbContext)
        {
            if (!_dbContext.Brands.Any())
            {
                var brandsPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "brands.json");
                var BrandsData = File.ReadAllText(brandsPath);
                var Brands = JsonSerializer.Deserialize<List<Brand>>(BrandsData);

                foreach (var item in Brands)
                {
                    _dbContext.Brands.Add(item);
                }
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.Categories.Any())
            {
                var categoriesPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "categories.json");
                var CategoriesData = File.ReadAllText(categoriesPath);
                var Categories = JsonSerializer.Deserialize<List<Category>>(CategoriesData);

                foreach (var item in Categories)
                {
                    _dbContext.Categories.Add(item);
                }
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.Products.Any())
            {
                var productsPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "products.json");
                var ProductsData = File.ReadAllText(productsPath);
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                foreach (var item in Products)
                {
                    _dbContext.Products.Add(item);
                }
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
