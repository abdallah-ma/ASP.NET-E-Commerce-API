using PaymentService.Models;
using PaymentService.Interfaces;

namespace PaymentService.Interfaces
{
    public class ProductClient : IProductClient
    {
        private readonly HttpClient _httpClient;

        public ProductClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Products/{id}");
            return await response.Content.ReadFromJsonAsync<Product>();    
        }
    }
}