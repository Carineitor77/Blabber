using Application.Common.DTO.Auth;
using Application.Common.DTO.User;
using AutoMapper;
using Domain;

namespace Application.Common.AutoMapper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<RegisterDto, AuthUser>();
        CreateMap<AuthUser, UserDto>();
    }
}