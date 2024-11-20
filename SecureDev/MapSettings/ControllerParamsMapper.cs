using System.Globalization;
using AutoMapper;
using BusinessLogic.Abstractions.Dto;
using SecureDev.Controllers.Card;
using SecureDev.Controllers.Client;

namespace SecureDev.MapSettings;

public class ControllerParamsMapper: Profile
{
    public ControllerParamsMapper()
    {
        CreateMap<CreateCardRequest, CardDto>()
            .ForMember(dto => dto.ExpDate, expression => 
                expression.MapFrom(request => request.ExpDate.ToString(CultureInfo.InvariantCulture)));
        CreateMap<CreateClientRequest, ClientDto>();
    }
}