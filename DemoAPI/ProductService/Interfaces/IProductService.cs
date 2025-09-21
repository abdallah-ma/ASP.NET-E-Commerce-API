using ProductMicroService.Grpc;
using ProductService.Models;
using ProductService.Specifications;


namespace ProductService.Interfaces
{
    public interface IProductService
    {

        public Task<GrpcProduct> GetProductAsync(int productId);

        public Task<IEnumerable<GrpcProduct>> GetProductsAsync(ProductSpecParams spec);

        public Task<int> GetCountAsync(FilteredProductsCountSpec spec);

        public Task<IEnumerable<Brand>> GetBrandsAsync();

        public Task<IEnumerable<Category>> GetCategoriesAsync();

    }
}
