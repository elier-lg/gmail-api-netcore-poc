using Global.WebContracts.Inputs;
using Global.WebContracts.Outputs;

namespace Services.Interfaces
{
  public interface IGMailApiService
  {
    Task<List<EmailOut>> GetEmails(EmailsFiltersIn filters);
    Task<EmailOut> GetEmail(string id);
  }
}