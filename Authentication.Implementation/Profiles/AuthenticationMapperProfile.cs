using Authentication.Abstractions.Dto;
using AutoMapper;
using DAL.Abstractions.Entities;

namespace Authentication.Implementation.Profiles;

public class AuthenticationMapperProfile: Profile
{
    public AuthenticationMapperProfile()
    {
        CreateMap<AccountEntity, AccountDto>().ReverseMap();
    }
}