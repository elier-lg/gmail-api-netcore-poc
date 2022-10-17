using Global.Settings;
using Services.Interfaces;
namespace Services;
public class AuthService : IAuthService
{
  private readonly ADAuthenticationSettings _authSettings;

  public AuthService(ADAuthenticationSettings authSettings)
  {
    _authSettings = authSettings;
  }

  public async Task<string> GetToken()
  {
    return "some token";
  }

}
