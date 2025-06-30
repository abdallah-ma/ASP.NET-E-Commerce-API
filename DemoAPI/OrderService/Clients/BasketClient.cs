using OrderService.Models;
using OrderService.Interfaces;


namespace OrderService.Clients
{


    public class BasketClient : IBasketClient
    {
        private readonly HttpClient _httpClient;

        public BasketClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CustomerBasket?> GetBasketAsync(string Id)
        {
            var response = await _httpClient.GetAsync($"api/Baskets?id={Id}");
            if (!response.IsSuccessStatusCode)
            {   Console.WriteLine($"---------------------------------------------------\n\n");
                Console.WriteLine($"Error fetching basket: {response.StatusCode}");
                return null;
            }
            return await response.Content.ReadFromJsonAsync<CustomerBasket>();
        }


    }

}