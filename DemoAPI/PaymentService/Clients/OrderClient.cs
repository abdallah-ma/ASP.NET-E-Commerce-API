using PaymentService.Interfaces;
using PaymentService.Models;


namespace PaymentService.Clients
{
    public class OrderClient : IOrderClient
    {
        
        private readonly HttpClient _httpClient;

        public OrderClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DeliveryMethod> GetDeliveryMethodAsync(int id)
        {
            var response = await _httpClient.GetAsync($"deliverymethod/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<DeliveryMethod>();
        }

        public async Task<Order> GetOrderByPaymentIntentIdAsync(string paymentIntentId)
        {
            var response = await _httpClient.GetAsync($"{paymentIntentId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Order>();
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            var response = await _httpClient.PutAsJsonAsync(_httpClient.BaseAddress,order);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Order>();
        }

    }
}