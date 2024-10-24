using AutoMapper;
using Talabat.APIS.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Orders_Aggregate;

namespace Talabat.APIS.Helpers
{
    public class MappingProfile : Profile
    {


        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
           .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
           .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
           .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPicUrlResolver>());

            CreateMap<CustomerBasket, CustomerBasketDto>();
            CreateMap<BasketItem, BasketItemDto>();
            CreateMap<AddressDto, Address>().ReverseMap();


        }
    }
}
