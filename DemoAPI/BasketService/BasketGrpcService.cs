using BasketService.Interfaces;
using Grpc.Core;
using BasketService.Models;

namespace BasketService
{
    public class BasketService : BasketGrpcService.BasketGrpcServiceBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketService(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public override async Task<GrpcBasket> GetBasket(GrpcBasketId BasketId, ServerCallContext context)
        {
            var basket = await _basketRepository.GetBasketAsync(BasketId.Id);
            var result = new GrpcBasket { Id = basket.Id };

            result.DeliveryMethodId = basket.DeliveryMethodId.HasValue ? basket.DeliveryMethodId.Value: null;
            result.Items.AddRange(basket.Items.Select(item => new GrpcBasketItem
            {
                Id = item.Id,
                ProductName = item.ProductName,
                Quantity = item.Quantity
            }));

            return result;
        }

        public override async Task<GrpcBasket> UpdateBasket(GrpcBasket basket, ServerCallContext context)
        {
            var updatedBasket = new CustomerBasket
            {
                Id = basket.Id,
                Items = basket.Items.Select(item => new BasketItem
                {
                    Id = item.Id,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity
                }).ToList()
            };

            await _basketRepository.UpdateBasketAsync(updatedBasket);
            return basket;
        }

        
        
    }

}