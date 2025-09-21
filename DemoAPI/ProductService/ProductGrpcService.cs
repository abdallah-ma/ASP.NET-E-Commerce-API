using Grpc.Core;
using ProductMicroService;
using DemoAPI.Common;
using DemoAPI.Common.Interfaces;
using ProductMicroService.Grpc;

using ProductMicroServiceBase = ProductMicroService.Grpc.ProductGrpcService.ProductGrpcServiceBase;
using ProductService.Interfaces;
using ProductService.Models;
using ProductService.Specifications;

namespace ProductService
{

   public class ProductGrpcService : ProductMicroServiceBase
    {


        private readonly IUnitOfWork _unitOfWork;

        public ProductGrpcService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async override Task<GrpcProduct> GetProduct(GrpcProductId request, ServerCallContext context)
        {

            var repo = _unitOfWork.Repository<Product>();

            var spec = new ProductSpecifications(request.Id);
            var product = await repo.GetAsyncWithSpec(spec);

            if (product == null)
            {
                Console.WriteLine($"Product with id {request.Id} not found.");
                return null;
            }

            return new GrpcProduct
            {
                Id = product.Id,
                Name = product.Name,
                Price = (double)product.Price,
                PictureUrl = product.PictureUrl,
                Brand = new GrpcBrand { Id = product.Brand.Id, Name = product.Brand.Name },
                Category = new GrpcCategory { Id = product.Category.Id, Name = product.Category.Name }
            };
        }

        
    }

}