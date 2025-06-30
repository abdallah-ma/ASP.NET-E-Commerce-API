using DemoAPI.Common;

namespace PaymentService.Models
{
    public class Order : BaseEntity
    {

        public Order()
        {
            
        }

        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, decimal subTotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; }

        public Address ShippingAddress { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

        public DeliveryMethod DeliveryMethod { get; set; }

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        public decimal SubTotal { get; set; }

        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;

        public string PaymentIntentId { get; set; }

    }
}
