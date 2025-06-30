using ProductService.Models;
using ProductService.Specifications;


namespace ProductService.Interfaces
{
    public interface IProductService
    {

        public Task<Product> GetProductAsync(int productId);

        public Task<IEnumerable<Product>> GetProductsAsync(ProductSpecParams spec);

        public Task<int> GetCountAsync(ProductSpecParams spec);

        public Task<IEnumerable<Brand>> GetBrandsAsync();

        public Task<IEnumerable<Category>> GetCategoriesAsync();

    }
}
