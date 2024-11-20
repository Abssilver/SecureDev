using AutoMapper;
using BusinessLogic.Abstractions;
using BusinessLogic.Abstractions.Dto;
using DAL.Abstractions;
using DAL.Abstractions.Entities;
using Microsoft.Extensions.Logging;
using Validation.Abstractions;
using Validation.Implementation;

namespace BusinessLogic.Implementation;

public class CardService: ICardService
{
    private readonly ILogger<CardService> _logger;
    private readonly ICardRepository _repository;
    private readonly IMapper _mapper;
    private readonly IBusinessLogicOperationFailureFactory _failureFactory;
    private readonly IValidationService<CardDto> _validator;

    public CardService(
        ILogger<CardService> logger,
        ICardRepository repository,
        IMapper mapper,
        IBusinessLogicOperationFailureFactory failureFactory,
        IValidationService<CardDto> validator
        )
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
        _failureFactory = failureFactory;
        _validator = validator;
    }

    public async Task<IOperationResult<IEnumerable<CardDto>>> GetByClientIdAsync(int id)
    {
        if (id < 0)
        {
            var failure = _failureFactory.CreateCardsGettingFailure();
            _logger.LogError(failure.Description);
            return new OperationResult<IEnumerable<CardDto>>(ArraySegment<CardDto>.Empty, failure);
        }

        var cards = await _repository.GetByClientId(id);
        var result = _mapper.Map<IEnumerable<CardEntity>, IEnumerable<CardDto>>(cards)
            .ToList();
        return new OperationResult<IEnumerable<CardDto>>(result, ArraySegment<IOperationFailure>.Empty);
    }

    public async Task<IOperationResult<Guid>> CreateAsync(CardDto dto)
    {
        var failures = _validator.ValidateEntity(dto);
        if (failures.Count != 0)
        {
            _logger.LogError(string.Join("/n", failures.Select(x => x.Description)));
            return new OperationResult<Guid>(Guid.Empty, failures);
        }
        
        var entity = _mapper.Map<CardEntity>(dto);
        var id = await _repository.CreateAsync(entity);
        return new OperationResult<Guid>(id, ArraySegment<IOperationFailure>.Empty);
    }
}