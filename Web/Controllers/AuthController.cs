using System.Net;
using System.Text.Json;
using Global.Exceptions;
using Global.WebContracts.Inputs;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Interfaces;

namespace Web.Controllers
{
  [Route("[controller]")]
  public class AuthController : ControllerBase
  {

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
      _authService = authService;
    }

    [HttpGet]
    public async Task<IActionResult> GetToken(ADAuthCredentialsIn creds)
    {
      Log.Information($"Get token call: {JsonSerializer.Serialize(creds)}");
      var resp = await _authService.GetToken();
      return Ok(resp);
    }

    [HttpGet]
    [Route("Refresh")]
    public async Task<IActionResult> RefreshToken(ADAuthCredentialsIn creds)
    {
      throw new CustomException(HttpStatusCode.NotImplemented, "Refresh not implemented");
    }
  }
}