using PaymentService.Models;

namespace PaymentService.Interfaces
{
    public interface IBasketClient
    {
        Task<CustomerBasket> GetBasketAsync(string userId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        
    }
}