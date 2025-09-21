using BasketService.Models;

namespace BasketService.Dtos
{
    public class CustomerBasketDto
    {


        public string Id { get; set; }

        public string? PaymentIntentId { get; set; }

        public string? ClientSecret { get; set; }

        public int? DeliveryMethodId { get; set; }

        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();

    }
}
