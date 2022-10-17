using System.Text.Json;
using Global.WebContracts.Inputs;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Interfaces;

namespace Web.Controllers
{
  [Route("[controller]")]
  public class EmailController : ControllerBase
  {
    private readonly IGMailApiService _gmailService;

    public EmailController(IGMailApiService gMailService)
    {
      _gmailService = gMailService;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmails(EmailsFiltersIn filters)
    {
      Log.Information($"Get emails:  {JsonSerializer.Serialize(filters)}");
      var resp = await _gmailService.GetEmails(filters);
      return Ok(resp);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetEmail(string id)
    {
      Log.Information($"Get email: {id}");
      return Ok(id);
    }
  }
}