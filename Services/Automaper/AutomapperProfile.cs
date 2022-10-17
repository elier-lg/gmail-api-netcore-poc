using Global.WebContracts.Outputs;
using Google.Apis.Gmail.v1.Data;
using Profile = AutoMapper.Profile;

namespace Services.Automapper
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      // Map types in order to abstract anything that should not be exposed out
      CreateMap<Message, EmailOut>();
    }
  }
}