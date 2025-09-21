using PaymentService;
using PaymentService.Models;

namespace PaymentService.Interfaces
{
    public interface IPaymentService
    {


        public Task<Order> UpdatePaymentIntentSucceededOrFailed(string PaymentIntentId, bool Successful);

    }
}
