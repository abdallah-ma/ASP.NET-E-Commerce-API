using PaymentService.Models;
using DemoAPI.Common;

namespace PaymentService.Specifications
{
    public class OrderPaymentIntentSpec : BaseSpecifications<Order>
    {

        public OrderPaymentIntentSpec(string PaymentIntentId)
            : base(o => o.PaymentIntentId == PaymentIntentId)
        {
        }

    }
}
