using Authentication.Abstractions.Dto;
using Authentication.Implementation.Controllers;
using AutoMapper;
using DAL.Abstractions.Entities;

namespace Authentication.Implementation.Profiles;

public class AuthenticationMapperProfile: Profile
{
    public AuthenticationMapperProfile()
    {
        CreateMap<AccountEntity, AccountDto>();
        CreateMap<CreateAccountRequest, CreateAccountDto>();
        CreateMap<AuthRequest, AuthRequestDto>();
    }
}