using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Orders_Aggregate;

namespace Talabat.Repository.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext _dbcontext) 
        {
            var BrandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/brands.json");
            var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
            if (Brands.Count()>0)
            {
                //Brands = Brands.Select(B => new ProductBrand()
                //{
                //    Name = B.Name
                //}).ToList();
                if (_dbcontext.ProductBrands.Count()==0)
                {
                    foreach (var brand in Brands)
                    {
                        _dbcontext.Set<ProductBrand>().Add(brand);
                    }
                    await _dbcontext.SaveChangesAsync();
                }

            }


            var CategoriesData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/categories.json");
            var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);
            if (Categories.Count() > 0)
            {
                if (_dbcontext.ProductCategories.Count() == 0)
                {
                    foreach (var Category in Categories)
                    {
                        _dbcontext.Set<ProductCategory>().Add(Category);
                    }
                    await _dbcontext.SaveChangesAsync();
                }

            }

            var ProductsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/products.json");
            var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
            if (Products.Count() > 0)
            {
                if (_dbcontext.Products.Count() == 0)
                {
                    foreach (var Product in Products)
                    {
                        _dbcontext.Set<Product>().Add(Product);
                    }
                    await _dbcontext.SaveChangesAsync();
                }

            }

            var DeliveryData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/delivery.json");
            var Deliverys = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);
            if (Deliverys.Count() > 0)
            {
                if (_dbcontext.Orders.Count() == 0)
                {
                    foreach (var Delivery in Deliverys)
                    {
                        _dbcontext.Set<DeliveryMethod>().Add(Delivery);
                    }
                    await _dbcontext.SaveChangesAsync();
                }

            }
        }
    }
}
