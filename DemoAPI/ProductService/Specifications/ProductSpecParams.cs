

namespace ProductService.Specifications
{
    public class ProductSpecParams
    {
        private int pageSize;

        public string? sort { get; set; }

        public int? BrandId { get; set; }

        public int? CategoryId { get; set; }

        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }

        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > 10 ? 10 : Math.Max(5,value);
        }

        public int PageIndex { get; set; } = 1;
    }

}
