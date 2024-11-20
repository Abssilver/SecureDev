using AutoMapper;
using BusinessLogic.Abstractions;
using BusinessLogic.Abstractions.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecureDev.Controllers.Client;

[Authorize]
[Route("api/client")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientService _service;
    private readonly IMapper _mapper;

    public ClientController(IClientService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateClientRequest request)
    {
        var result = await _service.CreateAsync(_mapper.Map<ClientDto>(request));
        return Ok(new CreateClientResponse(result.Result, result.Failures));
    }
}