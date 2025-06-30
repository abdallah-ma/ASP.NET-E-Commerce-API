using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderService.Models;

namespace OrderService.Interfaces
{
    public interface IOrderService
    {

        Task<Order> CreateOrderAsync(string BuyerEmail, Address shippingAddress, int deliveryMethodId, string basketId);

        Task UpdateOrderAsync(Order order);
        Task<Order> GetUserOrderByIdAsync(int id, string buyerEmail);

        Task<IReadOnlyList<Order>> GetUserOrdersAsync(string buyerEmail);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
        Task<DeliveryMethod> GetDeliveryMethodAsync(int id);


    }
}
