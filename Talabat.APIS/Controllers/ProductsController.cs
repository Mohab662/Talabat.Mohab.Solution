using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.APIS.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specification;
using Talabat.Core.Specification.ProductSpecifications;

namespace Talabat.APIS.Controllers
{
    public class ProductsController : BaseApiController
    {

        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductCategory> _categoryRepository;
        private readonly IGenericRepository<ProductBrand> _brandRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> ProductRepository, IGenericRepository<ProductCategory> CategoryRepository, IGenericRepository<ProductBrand> BrandRepository, IMapper mapper)
        {
            _productRepository = ProductRepository;
            _categoryRepository = CategoryRepository;
            _brandRepository = BrandRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var spec = new ProductWithBrandAndCategoryspec();
            var products = await _productRepository.GetAllWithSpecAsync(spec);
            
            return Ok(_mapper.Map<IEnumerable<Product>,IEnumerable<ProductDto>>(products));
        }

        [ProducesResponseType(typeof(ProductDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndCategoryspec(id);
            var product = await _productRepository.GetWithSpecAsync(spec);
            if (product is null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<Product, ProductDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrand() 
        {
            var Brands = await _brandRepository.GetAllAsync();

            return Ok(Brands);
        }

        [HttpGet("category")]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetCategory()
        {
            var Category = await _categoryRepository.GetAllAsync();

            return Ok(Category);


        }
    }
}