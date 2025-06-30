using OrderService.Models;


namespace OrderService.Interfaces
{
    public interface IBasketClient
    {
        Task<CustomerBasket?> GetBasketAsync(string id);
        
    }
}