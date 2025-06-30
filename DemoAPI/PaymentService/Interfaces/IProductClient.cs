using PaymentService.Models;


namespace PaymentService.Interfaces
{
    public interface IProductClient
    {
        Task<Product> GetProductAsync(int id);

    }
}