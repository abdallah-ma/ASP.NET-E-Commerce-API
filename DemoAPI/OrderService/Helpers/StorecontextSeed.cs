using System.Text.Json;
using OrderService.Models;

namespace OrderService.Helpers
{
    public class StorecontextSeed
    {

        public async static Task SeedAsync(AppDbContext _dbContext)
        {


                
            if (!_dbContext.DeliveryMethods.Any())
            {

                var DeliveryMethodsData = File.ReadAllText("SeedData/delivery.json");
                var data = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);

                foreach(var item in data)
                {
                    await _dbContext.DeliveryMethods.AddAsync(item);
                }
                await _dbContext.SaveChangesAsync();

            }

        }
        }

    }

