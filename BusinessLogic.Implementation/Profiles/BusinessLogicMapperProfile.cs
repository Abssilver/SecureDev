using System.Globalization;
using AutoMapper;
using BusinessLogic.Abstractions.Dto;
using DAL.Abstractions.Entities;

namespace BusinessLogic.Implementation.Profiles;

public class BusinessLogicMapperProfile: Profile
{
    private const string DateTimeFormat = "MM/yy";
    public BusinessLogicMapperProfile()
    {
        CreateMap<ClientEntity, ClientDto>().ReverseMap();
        
        CreateMap<CardEntity, CardDto>()
            .ForMember(dto => dto.ExpDate, options =>
                options.MapFrom(source => source.ExpDate.ToString(DateTimeFormat)));
        CreateMap<CardDto, CardEntity>()
            .ForMember(entity => entity.ExpDate, options =>
                options.MapFrom(source => DateTime.ParseExact(source.ExpDate, DateTimeFormat, CultureInfo.InvariantCulture)));
    }
}