using AutoMapper;
using AccountService.Models.Identity;
using AccountService.Dtos;

namespace AccountService
{

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<Address, AddressDto>()
                .ReverseMap();

        }
    }
}