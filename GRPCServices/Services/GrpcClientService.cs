using AutoMapper;
using BusinessLogic.Abstractions;
using BusinessLogic.Abstractions.Dto;
using Grpc.Core;
using GRPCServices.Proto;

namespace GRPCServices.Services;

public class GrpcClientService: ClientService.ClientServiceBase
{
    private readonly IClientService _service;
    private readonly IMapper _mapper;
    
    public GrpcClientService(IClientService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    
    public override async Task<CreateClientResponse> Create(CreateClientRequest request, ServerCallContext context)
    {
        var result = await _service.CreateAsync(_mapper.Map<ClientDto>(request));
        var response = new CreateClientResponse { ClientId = result.Result };
        response.Failures.AddRange(result.Failures.Select(x => new OperationResult
        {
            PropertyName = x.PropertyName,
            ErrorCode = x.ErrorCode,
            ErrorMessage = x.Description,
        }));
        return response;
    }
}