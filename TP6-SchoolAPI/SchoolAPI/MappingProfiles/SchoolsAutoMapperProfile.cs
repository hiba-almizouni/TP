using AutoMapper;
using SchoolAPI.DTOs;
using SchoolAPI.Models;

namespace SchoolAPI.MappingProfiles;

public class SchoolsAutoMapperProfile : Profile
{
    public SchoolsAutoMapperProfile()
    {
        // School → SchoolDto
        CreateMap<School, SchoolDto>();

        // SchoolDto → School (Director = "" par défaut)
        CreateMap<SchoolDto, School>()
            .ForMember(dest => dest.Director,
                       opt => opt.MapFrom(src => ""));
    }
}
