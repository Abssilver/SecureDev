using BusinessLogic.Abstractions.Dto;
using DAL.Abstractions;
using DAL.Abstractions.Entities;
using Microsoft.AspNetCore.Mvc;
using Validation.Abstractions;

namespace SecureDev.Controllers.Card;

[Route("api/card")]
[ApiController]
public class CardController : ControllerBase
{
    private readonly ILogger<CardController> _logger;
    private readonly ICardRepository _repository;
    private readonly IBusinessLogicOperationFailureFactory _failureFactory;

    public CardController(
        ILogger<CardController> logger, 
        ICardRepository repository,
        IBusinessLogicOperationFailureFactory failureFactory
        )
    {
        _logger = logger;
        _repository = repository;
        _failureFactory = failureFactory;
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateCardRequest request)
    {
        try
        {
            var cardId = await _repository.CreateAsync(new CardEntity
            {
                ClientId = request.ClientId,
                CardNumber = request.CardNumber,
                ExpDate = request.ExpDate,
                Name = request.Name,
                CVV2 = request.CVV2
            });
            return Ok(new CreateCardResponse(cardId.ToString(), ArraySegment<IOperationFailure>.Empty));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Create card error");
            return Ok(new CreateCardResponse(string.Empty, _failureFactory.CreateCardCreationFailure()));
        }
    }

    [HttpGet("get-by-client-id")]
    [ProducesResponseType(typeof(GetCardsResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByClientId([FromQuery] int clientId)
    {
        try
        {
            var cards = await _repository.GetByClientId(clientId);
            return Ok(new GetCardsResponse(cards.Select(card => new CardDto
            {
                Id = card.Id,
                CardNumber = card.CardNumber,
                CVV2 = card.CVV2,
                Name = card.Name,
                ExpDate = card.ExpDate.ToString("MM/yy")
            }).ToList(), ArraySegment<IOperationFailure>.Empty));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get cards error");
            return Ok(new GetCardsResponse(ArraySegment<CardDto>.Empty, _failureFactory.CreateCardsGettingFailure()));
        }
    }
}