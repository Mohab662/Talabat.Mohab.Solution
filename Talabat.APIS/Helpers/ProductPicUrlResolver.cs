using AutoMapper;
using AutoMapper.Execution;
using Talabat.APIS.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIS.Helpers
{
    public class ProductPicUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPicUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
