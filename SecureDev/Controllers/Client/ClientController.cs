using BusinessLogic.Abstractions;
using BusinessLogic.Abstractions.Dto;
using Microsoft.AspNetCore.Mvc;

namespace SecureDev.Controllers.Client;

[Route("api/client")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientService _service;

    public ClientController(IClientService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateClientRequest request)
    {
        var result = await _service.CreateAsync(new ClientDto
        {
            FirstName = request.FirstName,
            Surname = request.Surname,
            Patronymic = request.Patronymic,
        });
        
        return Ok(new CreateClientResponse(result.Result, result.Failures));
    }
}