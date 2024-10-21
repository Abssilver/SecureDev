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

    public CardService(
        ILogger<CardService> logger,
        ICardRepository repository,
        IMapper mapper,
        IBusinessLogicOperationFailureFactory failureFactory
        )
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
        _failureFactory = failureFactory;
    }

    public async Task<IOperationResult<IEnumerable<CardDto>>> GetByClientIdAsync(int id)
    {
        try
        {
            var cards = await _repository.GetByClientId(id);
            var result = _mapper.Map<IEnumerable<CardEntity>, IEnumerable<CardDto>>(cards)
                .ToList();
            return new OperationResult<IEnumerable<CardDto>>(result, ArraySegment<IOperationFailure>.Empty);
        }
        catch (Exception ex)
        {
            var failure = _failureFactory.CreateCardsGettingFailure();
            _logger.LogError(ex, failure.Description);
            return new OperationResult<IEnumerable<CardDto>>(ArraySegment<CardDto>.Empty, failure);
        }
    }

    public async Task<IOperationResult<Guid>> CreateAsync(CardDto dto)
    {
        try
        {
            var entity = _mapper.Map<CardEntity>(dto);
            var id = await _repository.CreateAsync(entity);
            return new OperationResult<Guid>(id, ArraySegment<IOperationFailure>.Empty);
        }
        catch (Exception ex)
        {
            var failure = _failureFactory.CreateCardCreationFailure();
            _logger.LogError(ex, failure.Description);
            return new OperationResult<Guid>(Guid.Empty, failure);
        }
    }
}