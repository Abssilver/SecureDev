using AutoMapper;
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
    private readonly IMapper _mapper;

    public CardController(ICardService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(CreateCardResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateCardRequest request)
    {
        var result = await _service.CreateAsync(_mapper.Map<CardDto>(request));
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