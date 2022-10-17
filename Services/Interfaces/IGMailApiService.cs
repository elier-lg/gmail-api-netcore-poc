using Global.WebContracts.Inputs;

namespace Services.Interfaces
{
  public interface IGMailApiService
  {
    Task<string[]> GetEmails(EmailsFiltersIn filters);
  }
}