using OrderService.Models;


namespace OrderService.Interfaces
{
    public interface IProductClient
    {
        Task<Product?> GetProductAsync(int id);
       
    }
}