using AutoMapper;
using hondaerp.Dealerships.Dtos;
using hondaerp.Dealerships.Models;

namespace hondaerp.Dealerships.Profiles
{
    public class SuplierProfile : Profile
    {
        public SuplierProfile()
        {
            //Source -> Target
            CreateMap<Dealership, DealershipReadDto>();
            CreateMap<DealershipCreateDto, Dealership>();
            CreateMap<DealershipUpdateDto, Dealership>();
            CreateMap<Dealership, DealershipUpdateDto>();
        }

    }
}