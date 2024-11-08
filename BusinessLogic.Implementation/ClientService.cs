using AutoMapper;
using BusinessLogic.Abstractions;
using BusinessLogic.Abstractions.Dto;
using DAL.Abstractions;
using DAL.Abstractions.Entities;
using Microsoft.Extensions.Logging;
using Validation.Abstractions;
using Validation.Implementation;

namespace BusinessLogic.Implementation;

public class ClientService: IClientService
{
    private readonly ILogger<ClientService> _logger;
    private readonly IClientRepository _repository;
    private readonly IMapper _mapper;
    private readonly IBusinessLogicOperationFailureFactory _failureFactory;

    public ClientService(
        ILogger<ClientService> logger,
        IClientRepository repository,
        IMapper mapper,
        IBusinessLogicOperationFailureFactory failureFactory
        )
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
        _failureFactory = failureFactory;
    }

    public async Task<IOperationResult<int>> CreateAsync(ClientDto dto)
    {
        try
        {
            var entity = _mapper.Map<ClientEntity>(dto);
            var id = await _repository.CreateAsync(entity);
            return new OperationResult<int>(id, ArraySegment<IOperationFailure>.Empty);
        }
        catch (Exception ex)
        {
            var failure = _failureFactory.CreateClientCreationFailure();
            _logger.LogError(ex, failure.Description);
            return new OperationResult<int>(-1, failure);
        }
    }
}