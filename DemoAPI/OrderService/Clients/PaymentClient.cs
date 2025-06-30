using OrderService.Interfaces;
using OrderService.Dtos;
using System.Net.Http.Headers;

namespace OrderService.Clients
{
    public class PaymentClient : IPaymentClient
    {
        private readonly HttpClient _httpClient;

        public PaymentClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        
        public async Task CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            var response = await _httpClient.GetAsync($"api/Payments/{BasketId}");
            response.EnsureSuccessStatusCode();
        }
    }
}