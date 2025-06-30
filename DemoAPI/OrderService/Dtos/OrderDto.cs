
using System.ComponentModel.DataAnnotations;

namespace OrderService.Dtos
{
    public class OrderDto
    {


        [Required]
        public AddressDto ShipToAddress { get; set; }

        public int DeliveryMethodId { get; set; }

        public string BasketId { get; set; }

    }
}
