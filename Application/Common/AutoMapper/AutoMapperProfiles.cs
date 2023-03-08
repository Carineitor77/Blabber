using Application.Common.DTO.Auth;
using Application.Common.DTO.Profile;
using Application.Common.DTO.User;
using Domain;

using AutoMapperProfile = AutoMapper.Profile;

namespace Application.Common.AutoMapper;

public class AutoMapperProfiles : AutoMapperProfile
{
    public AutoMapperProfiles()
    {
        CreateMap<RegisterDto, AuthUser>();
        CreateMap<AuthUser, UserDto>();
        CreateMap<AuthUser, ProfileDto>();
    }
}