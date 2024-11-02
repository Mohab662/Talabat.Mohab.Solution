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

            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<AddressDto, ShipAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
           .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
           .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
            .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
            .ForMember(d => d.ProductUrl, o => o.MapFrom(s => s.Product.ProductUrl))
            .ForMember(d => d.ProductUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());




        }
    }
}
