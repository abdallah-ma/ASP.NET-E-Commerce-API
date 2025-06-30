using DemoAPI.Common;
using ProductService.Models;

namespace ProductService.Specifications
{
    public class FilteredProductsCountSpec : BaseSpecifications<Product>
    {

        public FilteredProductsCountSpec(ProductSpecParams spec)
        : base(P =>
                 (string.IsNullOrEmpty(spec.Search) || P.Name.ToLower().Contains(spec.Search)) &&
                 (!spec.BrandId.HasValue || P.BrandId == spec.BrandId) &&
                 (!spec.CategoryId.HasValue || P.CategoryId == spec.CategoryId)
              )
        {



        }

    }
}
