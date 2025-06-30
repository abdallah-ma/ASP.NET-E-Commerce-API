using Microsoft.AspNetCore.Mvc;
using DemoAPI.Common.Interfaces;
using ProductService.Models;
using ProductService.Specifications;
using AutoMapper;
using ProductService.Dtos;
using DemoAPI.Common;

namespace DemoAPI.Controllers
{


    
    public class ProductsController : BaseAPIController 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Cached(600)]
        public async Task<ActionResult   < IReadOnlyList<Pagination<ProductToReturnDto> > > > GetProducts([FromQuery] ProductSpecParams spec)
        {   
            var ProductSpec = new ProductSpecifications(spec);

            var Products = await _unitOfWork.Repository<Product>().GetAllAsyncWithSpec(ProductSpec);

            var CountSpec = new FilteredProductsCountSpec(spec);

            var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products);

            var Count = await _unitOfWork.Repository<Product>().CountAsync(CountSpec);

            return Ok(new Pagination<ProductToReturnDto> (spec.PageIndex, spec.PageSize,Count,Data));
        }

        [HttpGet("{id}")]
        public async Task< ActionResult <Product> > GetProduct(int id)
        {
            var ProductSpecification = new ProductSpecifications(id);
            var Product = await _unitOfWork.Repository<Product>().GetAsyncWithSpec(ProductSpecification);

            

            return Ok( Product );
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<Brand>>> GetBrands()
        {
            var Brands = await _unitOfWork.Repository<Brand>().GetAllAsync();
            return Ok(Brands);
        }

        [HttpGet("Categories")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories()
        {
            var Categories = await _unitOfWork.Repository<Category>().GetAllAsync();
            return Ok(Categories);
        }


    }
}
