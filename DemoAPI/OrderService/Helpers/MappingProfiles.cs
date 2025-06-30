using AutoMapper;
using OrderService.Models;
using OrderService.Dtos;


namespace OrderService.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
           
            CreateMap<OrderItem, OrderItemToReturnDto>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>();


             CreateMap<OrderItem, OrderItemToReturnDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.ProductId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.Product.PictureUrl))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));




      CreateMap<Order, OrderToReturnDto>()
     .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
     .ForMember(dest => dest.DeliveryMethodCost, opt => opt.MapFrom(src => src.DeliveryMethod.Cost))
     .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.OrderStatus.ToString()))
     .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}