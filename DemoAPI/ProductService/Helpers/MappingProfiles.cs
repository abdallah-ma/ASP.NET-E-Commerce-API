using AutoMapper;
using ProductService.Dtos;
using ProductService.Models;


namespace ProductService.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
           
            CreateMap<Product, ProductToReturnDto>()
            .ForMember(dest => dest.Brand , opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
            
            
            
        }
    }
}