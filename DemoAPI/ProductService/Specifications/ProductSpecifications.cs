using DemoAPI.Common;
using ProductService.Models;

namespace ProductService.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product>
    {


        public ProductSpecifications(ProductSpecParams spec) :
            base(P =>
                (string.IsNullOrEmpty(spec.Search) || P.Name.ToLower().Contains(spec.Search)) &&
                (!spec.BrandId.HasValue || spec.BrandId == P.BrandId) &&
                (!spec.CategoryId.HasValue || spec.CategoryId == P.CategoryId)
            )

        {

            AddIncludes();
            if (string.IsNullOrEmpty(spec.sort))
            {
                OrderBy = x => x.Name;
            }
            else
            {
                switch (spec.sort)
                {
                    case "PriceAsc":
                        OrderBy = x => x.Price;
                        break;
                    case "PriceDesc":
                        OrderByDesc = x => x.Price;
                        break;
                    default:
                        OrderBy = x => x.Name;
                        break;
                }
            }

            ApplyPagination((spec.PageIndex - 1) * spec.PageSize, spec.PageSize);


        }

        public ProductSpecifications(int id) : base(x => x.Id == id)
        {
            AddIncludes();
        }

        public void AddIncludes()
        {
            Includes.Add(x => x.Category);
            Includes.Add(x => x.Brand);
        }




    }
}
