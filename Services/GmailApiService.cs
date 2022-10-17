using Global.WebContracts.Inputs;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Services.Interfaces;
namespace Services;
public class GmailApiService : IGMailApiService
{
  string[] _scopes = { GmailService.Scope.MailGoogleCom };
  string _applicationName = "Email_Reader_Servive";
  public GmailApiService()
  {

  }

  public async Task<string[]> GetEmails(EmailsFiltersIn filters)
  {
    return new string[] { "Email 1", "Email 2" };
  }

  public GmailService GetService()
  {

    UserCredential credential;
    using (FileStream stream = new FileStream("./credentials.json",
        FileMode.Open,
        FileAccess.Read))
    {
      string credPath = "APITokenCredentials";
      credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
          GoogleClientSecrets.FromStream(stream).Secrets,
          _scopes,
          "user",
          CancellationToken.None,
          new FileDataStore(credPath, true)).Result;
    }

    // Create Gmail API service.
    GmailService service = new GmailService(new BaseClientService.Initializer()
    {
      HttpClientInitializer = credential,
      ApplicationName = _applicationName,
    });
    return service;
  }
}
