using AutoMapper;
using BasketService.Models;
using BasketService.Dtos;


namespace BasketService
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
        }
    }
}