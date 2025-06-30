using OrderService.Interfaces;
using OrderService.Models;
using DemoAPI.Common.Interfaces;
using OrderService.Specifications;

namespace OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IBasketClient _basketClient;
        private readonly IPaymentClient _paymentClient;

        private readonly IProductClient _productClient;
        private readonly IUnitOfWork _unitOfWork;


        public OrderService(IBasketClient basketClient,
            IPaymentClient paymentClient,
            IProductClient productClient,
            IUnitOfWork unitOfWork)
        {
            _basketClient = basketClient;
            _paymentClient = paymentClient;
            _productClient = productClient;
            _unitOfWork = unitOfWork;
        }


        public async Task<Order> CreateOrderAsync(string BuyerEmail, Address shippingAddress, int deliveryMethodId, string basketId)
        {

            var Basket = await _basketClient.GetBasketAsync(basketId);

            var OrderItems = new List<OrderItem>();

            if (Basket?.Items?.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var Product = await _productClient.GetProductAsync(item.Id);

                    var OrderItem = new OrderItem
                    {
                        Product = new ProductItemOrdered
                        {
                            ProductId = Product.Id,
                            Name = Product.Name,
                            PictureUrl = Product.PictureUrl
                        },
                        Quantity = item.Quantity,
                        Price = Product.Price
                    };

                    OrderItems.Add(OrderItem);
                }
            }

            var OrderRepo = _unitOfWork.Repository<Order>();

            decimal subTotal = OrderItems.Sum(item => item.Price * item.Quantity);

            var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);

            var ExistingOrder = await OrderRepo.GetAsyncWithSpec(new OrderPaymentIntentSpec(Basket?.PaymentIntentId));


            if (ExistingOrder != null)
            {
                OrderRepo.Delete(ExistingOrder);
                await _paymentClient.CreateOrUpdatePaymentIntentAsync(Basket.Id);
            }

            var order = new Order
            {
                BuyerEmail = BuyerEmail,
                ShippingAddress = shippingAddress,
                DeliveryMethod = DeliveryMethod,
                Items = OrderItems,
                SubTotal = subTotal,
                PaymentIntentId = Basket?.PaymentIntentId
            };

            await OrderRepo.Add(order);

            var result = await _unitOfWork.CompleteAsync();

            return result > 0 ? order : null;
        }

        public Task<Order> GetUserOrderByIdAsync(int id, string buyerEmail)
        {
            OrderSpecifications spec = new OrderSpecifications(id, buyerEmail);
            return _unitOfWork.Repository<Order>().GetAsyncWithSpec(spec);

        }

        public Task<IReadOnlyList<Order>> GetUserOrdersAsync(string buyerEmail)
        {
            OrderSpecifications spec = new OrderSpecifications(buyerEmail);

            return _unitOfWork.Repository<Order>().GetAllAsyncWithSpec(spec);

        }

        public async Task UpdateOrderAsync(Order order)
        {
            _unitOfWork.Repository<Order>().Update(order);
            var result = await _unitOfWork.CompleteAsync();
        
        }    

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
        }

        public async Task<DeliveryMethod> GetDeliveryMethodAsync(int id)
        {
            return await _unitOfWork.Repository<DeliveryMethod>().GetAsync(id);
        }

    }
}
