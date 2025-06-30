using PaymentService.Models;

namespace PaymentService.Interfaces
{
    public interface IOrderClient
    {

        Task<DeliveryMethod> GetDeliveryMethodAsync(int id);

        Task<Order> GetOrderByPaymentIntentIdAsync(string paymentIntentId);
        Task<Order> UpdateOrderAsync(Order order);

    }
}
