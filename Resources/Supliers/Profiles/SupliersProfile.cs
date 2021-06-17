using AutoMapper;
using hondaerp.Supliers.Dtos;
using hondaerp.Supliers.Models;

namespace hondaerp.Supliers.Profiles
{
    public class SuplierProfile : Profile
    {
        public SuplierProfile()
        {
            //Source -> Target
            CreateMap<Suplier, SuplierReadDto>();
            CreateMap<SuplierCreateDto, Suplier>();
            CreateMap<SuplierUpdateDto, Suplier>();
            CreateMap<Suplier, SuplierUpdateDto>();
        }

    }
}