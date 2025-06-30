using OrderService.Models;
using OrderService.Interfaces;


namespace OrderService.Clients
{
    public class ProductClient : IProductClient
    {
        private readonly HttpClient _httpClient;

        public ProductClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Product?> GetProductAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Products/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }
    }
}