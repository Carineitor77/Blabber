using Application.Common.DTO;
using AutoMapper;
using Domain;

namespace Application.Common.AutoMapper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<RegisterDto, User>();
    }
}