using PaymentService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Stripe;
using PaymentService.Models;

using Product = PaymentService.Models.Product;
using BasketGrpcClient = PaymentService.BasketGrpcService.BasketGrpcServiceClient;
using ProductGrpcClient = PaymentService.ProductGrpcService.ProductGrpcServiceClient;
using OrderGrpcClient = PaymentService.OrderGrpcService.OrderGrpcServiceClient;
using Grpc.Core;


namespace PaymentService
{
    public class PaymentMicroService : PaymentGrpcService.PaymentGrpcServiceBase
    {

        private readonly BasketGrpcClient _basketClient;

        private readonly ProductGrpcClient _productClient;
        private readonly OrderGrpcClient _orderClient;
        private readonly IConfiguration _configuration;


        public PaymentMicroService(
        BasketGrpcClient basketClient,
        ProductGrpcClient productClient,
        OrderGrpcClient orderClient,
        IConfiguration configuration)
        {
            _basketClient = basketClient;
            _productClient = productClient;
            _orderClient = orderClient;
            _configuration = configuration;
        }


        public override async Task<GrpcBasket> CreateOrUpdatePaymentIntent(GrpcBasketId basketId , ServerCallContext context )
        {
            
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var GrpcBasketId = new GrpcBasketId { Id = basketId.Id };

            var Basket = await _basketClient.GetBasketAsync(GrpcBasketId);

            Console.WriteLine($"\n\n\n\n------------{Basket}-----------");

            if (Basket is null) return null;

            if (Basket?.Items?.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var GrpcProductId = new GrpcProductId { Id = item.Id };
                    var Product = await _productClient.GetProductAsync(GrpcProductId);
                    if (item.Price != Product.Price)
                    {
                        item.Price = Product.Price;
                    }
                }
            }

            decimal ShippingPrice = 0;
            Console.WriteLine($"\n\n\n\n------------{Basket.DeliveryMethodId.Value}-----------");
            
            if (Basket.DeliveryMethodId.HasValue)
            {
                var GrpcDeliveryMethodId = new GrpcDeliveryMethodId { Id = Basket.DeliveryMethodId.Value };
                var DeliveryMethod = await _orderClient.GetDeliveryMethodAsync(GrpcDeliveryMethodId);
                ShippingPrice = (decimal)DeliveryMethod.Cost;
            }

            PaymentIntent PaymentIntent;
            PaymentIntentService PaymentIntentService = new PaymentIntentService();

            if (String.IsNullOrEmpty(Basket.PaymentIntentId))
            {
                var CreateOptions = new PaymentIntentCreateOptions()
                {
                    Amount = (long)((Basket.Items.Sum(item => item.Price * item.Quantity) + (double)ShippingPrice) * 100),
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
                    Amount = (long)((Basket.Items.Sum(item => item.Price * item.Quantity) + (double)ShippingPrice) * 100),
                };


                await PaymentIntentService.UpdateAsync(Basket.PaymentIntentId, UpdateOptions);


            }


            
            var IsUpdated = await _basketClient.UpdateBasketAsync(Basket);

            return new GrpcBasket
            {
                Id = Basket.Id,
                Items = { Basket.Items },
                PaymentIntentId = Basket.PaymentIntentId,
                ClientSecret = Basket.ClientSecret,
                DeliveryMethodId = Basket.DeliveryMethodId.Value
            };
        }


        
        public override async Task<GrpcOrder> UpdatePaymentIntentSucceededOrFailed(GrpcUpdateRequest request, ServerCallContext context)
        {

            var Order = await _orderClient.GetOrderByPaymentIntentIdAsync(new GrpcPaymentIntentId { PaymentIntentId = request.PaymentIntentId });


            if (request.Successful)
            {
                Order.OrderStatus = (GrpcOrderStatus)OrderStatus.PaymentReceived;
            }
            else Order.OrderStatus = (GrpcOrderStatus)OrderStatus.PaymentFailed;

            await _orderClient.UpdateOrderAsync(Order);

            return Order;

        }

    }
}
