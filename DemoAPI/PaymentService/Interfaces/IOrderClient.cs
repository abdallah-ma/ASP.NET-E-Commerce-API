using PaymentService.Models;

namespace PaymentService.Interfaces
{
    public interface IOrderClient
    {

        Task<GrpcDeliveryMethod> GetDeliveryMethodAsync(int id);

        Task<GrpcOrder> GetOrderByPaymentIntentIdAsync(GrpcPaymentIntentId paymentIntentId);
        Task<GrpcOrder> UpdateOrderAsync(GrpcOrder order);

    }
}
