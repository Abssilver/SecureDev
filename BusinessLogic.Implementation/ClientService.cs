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
    private readonly IValidationService<ClientDto> _validator;

    public ClientService(
        ILogger<ClientService> logger,
        IClientRepository repository,
        IMapper mapper,
        IValidationService<ClientDto> validator
        )
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<IOperationResult<int>> CreateAsync(ClientDto dto)
    {
        var failures = _validator.ValidateEntity(dto);
        if (failures.Count != 0)
        {
            _logger.LogError(string.Join("/n", failures.Select(x => x.Description)));
            return new OperationResult<int>(-1, failures);
        }
        
        var entity = _mapper.Map<ClientEntity>(dto);
        var id = await _repository.CreateAsync(entity);
        return new OperationResult<int>(id, ArraySegment<IOperationFailure>.Empty);
    }
}