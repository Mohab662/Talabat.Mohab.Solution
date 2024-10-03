using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specification.ProductSpecifications
{
    public class ProductWithBrandAndCategoryspec:BaseSpecification<Product>
    {
        public ProductWithBrandAndCategoryspec():base()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }
        public ProductWithBrandAndCategoryspec(int id):base(P=>P.Id==id)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }
    }
}
