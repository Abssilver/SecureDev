using System.Net.Http.Headers;
using Authentication.Abstractions.Dto;
using Authentication.Abstractions.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Authentication.Implementation.Controllers;


[ApiController]
[Authorize]
[Route("api/auth")]
public class AuthenticateController : ControllerBase
{
    private readonly IAuthenticationService _service;
    private readonly IMapper _mapper;

    public AuthenticateController(
        IAuthenticationService service,
        IMapper mapper
    )
    {
        _service = service;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] CreateAccountRequest request)
    {
        var result = await _service.CreateUser(_mapper.Map<CreateAccountDto>(request));
        return result.IsSucceed
            ? Ok(result)
            : BadRequest(result);
    }
    
    [AllowAnonymous]
    [HttpPost]
    [Route("authenticate")]
    public async Task<IActionResult> Login([FromBody] AuthRequest request)
    {
        var result = await _service.Authenticate(_mapper.Map<AuthRequestDto>(request));
        if (result.IsSucceed)
            Response.Headers.Add("X-Session-Token", result.Result?.SessionInfo.Token);
        
        return result.IsSucceed
            ? Ok(result)
            : Unauthorized(result);
    }

    [HttpGet]
    [Route("session")]
    public async Task<IActionResult> GetSessionInfo()
    {
        var authorization = Request.Headers[HeaderNames.Authorization];
        if (!AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            return Unauthorized();
        
        var sessionToken = headerValue.Parameter;
        if (string.IsNullOrEmpty(sessionToken))
            return Unauthorized();

        var result = await _service.GetSessionInfo(sessionToken);
        return result.IsSucceed
            ? Ok(result)
            : Unauthorized(result);
    }
}