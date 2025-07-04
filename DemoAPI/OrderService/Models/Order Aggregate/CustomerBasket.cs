﻿using OrderService.Models;

namespace OrderService.Models
{
    public class CustomerBasket
    {
        public CustomerBasket(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public string? PaymentIntentId { get; set; }

        public string? ClientSecret { get; set; }

        public int? DeliveryMethodId { get; set; }

        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

    }
}
