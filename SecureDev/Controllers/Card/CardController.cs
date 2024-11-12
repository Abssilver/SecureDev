using System.Globalization;
using BusinessLogic.Abstractions;
using BusinessLogic.Abstractions.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecureDev.Controllers.Card;

[Authorize]
[Route("api/card")]
[ApiController]
public class CardController : ControllerBase
{
    private readonly ICardService _service;

    public CardController(ICardService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(CreateCardResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateCardRequest request)
    {
        var result = await _service.CreateAsync(new CardDto
        {
            CardNumber = request.CardNumber,
            Name = request.Name,
            CVV2 = request.CVV2,
            ExpDate = request.ExpDate.ToString(CultureInfo.InvariantCulture),
        });

        return Ok(new CreateCardResponse(result.Result.ToString(), result.Failures));
    }

    [HttpGet("get-by-client-id")]
    [ProducesResponseType(typeof(GetCardsResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByClientId([FromQuery] int clientId)
    {
        var result = await _service.GetByClientIdAsync(clientId);
        return Ok(new GetCardsResponse(result.Result ?? ArraySegment<CardDto>.Empty, result.Failures));
    }
}