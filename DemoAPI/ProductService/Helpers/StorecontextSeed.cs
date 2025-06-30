using System.Text.Json;
using ProductService.Models;

namespace ProductService.Helpers
{
    public class StorecontextSeed
    {

        public async static Task SeedAsync(AppDbContext _dbContext)
        {

            
            if (!_dbContext.Brands.Any())
            {
                var BrandsData = File.ReadAllText("/SeedData/brands.json");
                var Brands = JsonSerializer.Deserialize<List<Brand>>(BrandsData);



                foreach (var item in Brands)
                {
                    _dbContext.Brands.Add(item);
                }
                await _dbContext.SaveChangesAsync();
            }

            if (! _dbContext.Categories.Any())
            {
                var CategoriesData = File.ReadAllText("SeedData/categories.json");
                var Categories = JsonSerializer.Deserialize<List<Category>>(CategoriesData);

                

                foreach (var item in Categories)
                {
                    _dbContext.Categories.Add(item);
                }
                await _dbContext.SaveChangesAsync();
            }


            if (! _dbContext.Products.Any())
            {
                Console.WriteLine("------------------------------------------------------------------------\n\n\n\n");

            Console.WriteLine(Directory.GetCurrentDirectory());
                var ProductsData = File.ReadAllText("SeedData/products.json");
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
