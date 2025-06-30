using ProductService.Interfaces;
using ProductService.Models;
using ProductService.Specifications;
using DemoAPI.Common.Interfaces;

namespace ProductService
{
    public class ProductService : IProductService
    {

        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Brand>> GetBrandsAsync()
        {
            return await _unitOfWork.Repository<Brand>().GetAllAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _unitOfWork.Repository<Category>().GetAllAsync();
        }

        public async Task<int> GetCountAsync(ProductSpecParams spec)
        {
            var ProductSpec = new FilteredProductsCountSpec(spec);

           return await _unitOfWork.Repository<Product>().CountAsync(ProductSpec);
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            return await _unitOfWork.Repository<Product>().GetAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(ProductSpecParams spec)
        {
            var ProductSpec = new ProductSpecifications(spec);
            return await _unitOfWork.Repository<Product>().GetAllAsyncWithSpec(ProductSpec);  
        }
    }
}
