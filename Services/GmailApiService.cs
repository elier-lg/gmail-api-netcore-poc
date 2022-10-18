using AutoMapper;
using Global.WebContracts.Inputs;
using Global.WebContracts.Outputs;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Services.Interfaces;
namespace Services;

public class GmailApiService : IGMailApiService
{
  private readonly IMapper _mapper;
  string[] _scopes = { GmailService.Scope.MailGoogleCom };
  string _applicationName = "Email_Reader_Servive";
  public GmailApiService(IMapper mapper)
  {
    _mapper = mapper;
  }

  public async Task<List<EmailOut>> GetEmails(EmailsFiltersIn filters)
  {
    List<Message> gmailData = new List<Message> { getDummyEmail(), getDummyEmail() };
    // Use automapper for abstracting third party types 
    return _mapper.Map<List<EmailOut>>(gmailData);
  }



  public async Task<EmailOut> GetEmail(string id)
  {
    var gmailApiMessage = getDummyEmail();
    // Use automapper for abstracting third party types 
    return _mapper.Map<EmailOut>(gmailApiMessage);

  }

  Message getDummyEmail()
  {
    var apiMessage = new Message();
    var id = Guid.NewGuid();
    apiMessage.Id = id.ToString();
    apiMessage.Raw = $"some raw data {id}";

    return apiMessage;
  }

  // public GmailService GetService()
  // {

  //   UserCredential credential;
  //   using (FileStream stream = new FileStream("./credentials.json",
  //       FileMode.Open,
  //       FileAccess.Read))
  //   {
  //     string credPath = "APITokenCredentials";
  //     credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
  //         GoogleClientSecrets.FromStream(stream).Secrets,
  //         _scopes,
  //         "user",
  //         CancellationToken.None,
  //         new FileDataStore(credPath, true)).Result;
  //   }

  //   // Create Gmail API service.
  //   GmailService service = new GmailService(new BaseClientService.Initializer()
  //   {
  //     HttpClientInitializer = credential,
  //     ApplicationName = _applicationName,
  //   });
  //   return service;
  // }

}
