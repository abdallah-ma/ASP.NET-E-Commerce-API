using OrderService.Models;
using DemoAPI.Common;

namespace OrderService.Specifications
{
    public class OrderPaymentIntentSpec : BaseSpecifications<Order>
    {

        public OrderPaymentIntentSpec(string PaymentIntentId)
            : base(o => o.PaymentIntentId == PaymentIntentId)
        {
        }

    }
}
