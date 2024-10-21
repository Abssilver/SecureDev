using DAL.Abstractions;
using DAL.Abstractions.Entities;
using Microsoft.AspNetCore.Mvc;
using Validation.Abstractions;

namespace SecureDev.Controllers.Client;

[Route("api/client")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientRepository _repository;
    private readonly ILogger<ClientController> _logger;
    private readonly IBusinessLogicOperationFailureFactory _failureFactory;

    public ClientController(
        IClientRepository repository,
        ILogger<ClientController> logger,
        IBusinessLogicOperationFailureFactory failureFactory
        )
    {
        _repository = repository;
        _logger = logger;
        _failureFactory = failureFactory;
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateClientRequest request)
    {
        try
        {
            var clientId = await _repository.CreateAsync(new ClientEntity
            {
                FirstName = request.FirstName,
                Surname = request.Surname,
                Patronymic = request.Patronymic
            });
            return Ok(new CreateClientResponse(clientId, ArraySegment<IOperationFailure>.Empty));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Create client error");
            return Ok(new CreateClientResponse(-1, _failureFactory.CreateClientCreationFailure()));
        }
    }
}