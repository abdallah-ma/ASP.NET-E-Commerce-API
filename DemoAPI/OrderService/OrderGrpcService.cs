

using DemoAPI.Common.Interfaces;
using Grpc.Core;
using OrderService.Models;
using OrderService.Specifications;

namespace OrderService
{
    public class OrderMicroService : OrderGrpcService.OrderGrpcServiceBase
    {

        private readonly IUnitOfWork _unitOfWork;


        public OrderMicroService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<GrpcDeliveryMethod> GetDeliveryMethod(GrpcDeliveryMethodId request, ServerCallContext context)
        {
            var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(request.Id);
            return new GrpcDeliveryMethod()
            {
                Id = DeliveryMethod.Id,
                ShortName = DeliveryMethod.ShortName,
                Description = DeliveryMethod.Description,
                Cost = (double)DeliveryMethod.Cost
            };

        }

        public override async Task<GrpcOrder> GetOrderByPaymentIntentId(GrpcPaymentIntentId request, ServerCallContext context)
        {
            var spec = new OrderPaymentIntentSpec(request.PaymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetAsyncWithSpec(spec);

            if (order == null) throw new RpcException(new Status(StatusCode.NotFound, $"Order with PaymentIntentId {request.PaymentIntentId} not found"));

            var grpcOrder = new GrpcOrder()
            {
                Id = order.Id,
                BuyerEmail = order.BuyerEmail,
                OrderDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(order.OrderDate),
                OrderStatus = (GrpcOrderStatus)order.OrderStatus,
                Subtotal = (double)order.SubTotal,
                PaymentIntentId = order.PaymentIntentId ?? string.Empty,
                DeliveryMethod = new GrpcDeliveryMethod()
                {
                    Id = order.DeliveryMethod.Id,
                    ShortName = order.DeliveryMethod.ShortName,
                    Description = order.DeliveryMethod.Description,
                    Cost = (double)order.DeliveryMethod.Cost
                },
                ShippingAddress = new GrpcAddress()
                {
                    FirstName = order.ShippingAddress.FirstName,
                    LastName = order.ShippingAddress.LastName,
                    Street = order.ShippingAddress.Street,
                    City = order.ShippingAddress.City,
                    Country = order.ShippingAddress.Country
                }
            };

            foreach (var item in order.Items)
            {
                grpcOrder.Items.Add(new GrpcOrderItem()
                {
                    Product = new GrpcProductItemOrdered()
                    {
                        ProductId = item.Product.ProductId,
                        Name = item.Product.Name,
                        PictureUrl = item.Product.PictureUrl
                    },
                    Price = (double)item.Price,
                    Quantity = item.Quantity
                });
            }

            return grpcOrder;
        }


        public override async Task<GrpcOrderUpdated> UpdateOrder(GrpcOrder order, ServerCallContext context)
        {

            var Order = new Order
            {
                Id = order.Id,
                BuyerEmail = order.BuyerEmail,
                OrderDate = order.OrderDate.ToDateTimeOffset(),
                OrderStatus = (OrderStatus)order.OrderStatus,
                SubTotal = (decimal)order.Subtotal,
                PaymentIntentId = order.PaymentIntentId,
                DeliveryMethod = new DeliveryMethod
                {
                    Id = order.DeliveryMethod.Id,
                    ShortName = order.DeliveryMethod.ShortName,
                    Description = order.DeliveryMethod.Description,
                    Cost = (decimal)order.DeliveryMethod.Cost
                },
                ShippingAddress = new Address
                {
                    FirstName = order.ShippingAddress.FirstName,
                    LastName = order.ShippingAddress.LastName,
                    Street = order.ShippingAddress.Street,
                    City = order.ShippingAddress.City,
                    Country = order.ShippingAddress.Country
                },
                Items = order.Items.Select(i => new OrderItem
                {
                    Product = new ProductItemOrdered
                    {
                        ProductId = i.Product.ProductId,
                        Name = i.Product.Name,
                        PictureUrl = i.Product.PictureUrl
                    },
                    Price = (decimal)i.Price,
                    Quantity = i.Quantity
                }).ToList()
            };
            var result = await _unitOfWork.Repository<Order>().UpdateAsync(Order);
            return new GrpcOrderUpdated { IsUpdated = result > 0 };
        }

    }
}