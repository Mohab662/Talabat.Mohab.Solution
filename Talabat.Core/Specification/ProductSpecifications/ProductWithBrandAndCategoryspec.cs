using Talabat.Core.Entities;

namespace Talabat.Core.Specification.ProductSpecifications
{
    public class ProductWithBrandAndCategoryspec : BaseSpecification<Product>
    {
        public ProductWithBrandAndCategoryspec(ProductSpecPrams specPrams) : base(p =>
        (string.IsNullOrEmpty(specPrams.Search)||p.Name.ToLower().Contains(specPrams.Search.ToLower()))&&

        (!specPrams.BrandId.HasValue||p.BrandId==specPrams.BrandId.Value)&&
        (!specPrams.CategoryId.HasValue||p.CategoryId==specPrams.CategoryId.Value)
            )
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
            if (!string.IsNullOrEmpty(specPrams.Sort))
            {
                switch (specPrams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;

                    case "priceDes":
                        AddOrderByDesc(p => p.Price);
                        break;

                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }

            ApplyPagination((specPrams.PageIndex-1)*specPrams.PageSize, specPrams.PageSize);
        }
        public ProductWithBrandAndCategoryspec(int id) : base(P => P.Id==id)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }

    }
}
