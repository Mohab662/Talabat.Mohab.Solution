using Talabat.Core.Entities;

namespace Talabat.Core.Specification.ProductSpecifications
{
    public class ProductWithFilterationForCountSpec : BaseSpecification<Product>
    {
        public ProductWithFilterationForCountSpec(ProductSpecPrams specPrams) : base(p =>

            (string.IsNullOrEmpty(specPrams.Search)||p.Name.ToLower().Contains(specPrams.Search.ToLower()))&&

            (!specPrams.BrandId.HasValue||p.BrandId==specPrams.BrandId.Value)&&
            (!specPrams.CategoryId.HasValue||p.CategoryId==specPrams.CategoryId.Value)
            )


        {

        }
    }
}
