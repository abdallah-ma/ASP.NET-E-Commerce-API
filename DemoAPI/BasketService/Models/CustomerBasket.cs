using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Models
{
    public class CustomerBasket
    {
        public CustomerBasket(string id)
        {
            Id = id;
        }

        public CustomerBasket()
        {
        }

        public string Id { get; set; }

        public string? PaymentIntentId { get; set; }

        public string? ClientSecret { get; set; }

        public int? DeliveryMethodId { get; set; }

        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

    }
}
