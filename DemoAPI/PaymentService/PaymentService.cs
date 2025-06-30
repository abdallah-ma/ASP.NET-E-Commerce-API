using PaymentService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Stripe;
using PaymentService.Models;

using Product = PaymentService.Models.Product;


namespace PaymentService
{
    [Authorize(AuthenticationSchemes ="Bearer")]
    public class PaymentService : IPaymentService
    {
        
        private readonly IBasketClient _basketClient;

        private readonly IProductClient _productClient;
        private readonly IOrderClient _orderClient;
        private readonly IConfiguration _configuration;

        public PaymentService(
            IBasketClient basketRepository,
            IProductClient productClient,
            IOrderClient orderClient,
            IConfiguration configuration)
        {
            _basketClient = basketRepository;
            _productClient = productClient;
            _orderClient = orderClient;
            _configuration = configuration;
        }

        
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntentAsync(string basketId)
        {

            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var Basket = await _basketClient.GetBasketAsync(basketId);

            if (Basket is null) return null;

            if(Basket?.Items?.Count > 0)
            {
                foreach(var item in Basket.Items)
                {
                    var Product = await _productClient.GetProductAsync(item.Id);
                    if(item.Price != Product.Price)
                    {
                        item.Price = Product.Price;
                    }
                }
            }

            decimal ShippingPrice = 0;

            if (Basket.DeliveryMethodId.HasValue)
            {
                var DeliveryMethod = await _orderClient.GetDeliveryMethodAsync(Basket.DeliveryMethodId.Value);
                ShippingPrice = DeliveryMethod.Cost;
            }

            PaymentIntent PaymentIntent;
            PaymentIntentService PaymentIntentService = new PaymentIntentService();

            if (String.IsNullOrEmpty(Basket.PaymentIntentId))
            {
                var CreateOptions = new PaymentIntentCreateOptions()
                {
                    Amount = (long)((Basket.Items.Sum(item => item.Price * item.Quantity) + ShippingPrice) * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                    
                };

                PaymentIntent = await PaymentIntentService.CreateAsync(CreateOptions);
                Basket.PaymentIntentId = PaymentIntent.Id;
                Basket.ClientSecret = PaymentIntent.ClientSecret;
            }
            else
            {
                var UpdateOptions = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)((Basket.Items.Sum(item => item.Price * item.Quantity) + ShippingPrice) * 100),
                };


               await PaymentIntentService.UpdateAsync(Basket.PaymentIntentId, UpdateOptions);
               

            }



            await _basketClient.UpdateBasketAsync(Basket);
            return Basket;
        }


        public async Task<Order> UpdatePaymentIntentSucceededOrFailed(string PaymentIntentId , bool Successful)
        {

            var Order = await _orderClient.GetOrderByPaymentIntentIdAsync(PaymentIntentId);


            if (Successful)
            {
                Order.OrderStatus = OrderStatus.PaymentReceived;
            }
            else Order.OrderStatus = OrderStatus.PaymentFailed;

            await _orderClient.UpdateOrderAsync(Order);

            return Order;

        }

    }
}
