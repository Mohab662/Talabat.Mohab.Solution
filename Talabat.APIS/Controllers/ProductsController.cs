using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specification;
using Talabat.Core.Specification.ProductSpecifications;

namespace Talabat.APIS.Controllers
{
    public class ProductsController : BaseApiController
    {

        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> ProductRepository,IMapper mapper)
        {
            _productRepository = ProductRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var spec = new ProductWithBrandAndCategoryspec();
            var products = await _productRepository.GetAllWithSpecAsync(spec);
            
            return Ok(_mapper.Map<IEnumerable<Product>,IEnumerable<ProductDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndCategoryspec(id);
            var product = await _productRepository.GetWithSpecAsync(spec);
            if (product is null)
            {
                return NotFound(new {Message="Not Found",StatusCode=404});
            }
            return Ok(_mapper.Map<Product, ProductDto>(product));
        }
    }
}