namespace Talabat.Core.Specification.ProductSpecifications
{
    public class ProductSpecPrams
    {
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

        private const int maxPageSize = 10;

        private int pageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value>maxPageSize ? maxPageSize : value; }
        }

        public int PageIndex { get; set; } = 1;

        public string? Search { get; set; }

    }
}
