using PaymentService.Interfaces;
using PaymentService.Models;



namespace PaymentService.Clients
{
    public class BasketClient : IBasketClient
    {
        private readonly HttpClient _httpClient;

        public BasketClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CustomerBasket?> GetBasketAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"api/baskets?id={userId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CustomerBasket>();
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var response = await _httpClient.PostAsJsonAsync("api/baskets", basket);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CustomerBasket>();
        }
    }
}