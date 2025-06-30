using PaymentService.Models;

namespace PaymentService.Interfaces
{
    public interface IPaymentService
    {

        public Task<CustomerBasket> CreateOrUpdatePaymentIntentAsync(string basketId);

        public Task<Order> UpdatePaymentIntentSucceededOrFailed(string PaymentIntentId, bool Successfuk);

    }
}
