using System.Net;
using Global.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using Web.Extensions;

namespace Web.Filters
{
  public class ExceptionFilter : ExceptionFilterAttribute
  {
    private readonly IWebHostEnvironment _hostingEnvironment;

    public ExceptionFilter(IWebHostEnvironment environment)
    {
      _hostingEnvironment = environment;
    }

    //TODO: Error codes to be handled 
    public override void OnException(ExceptionContext context)
    {
      Log.Error(context.Exception, context.Exception.Message);
      var obj = new
      {
        Message = context.Exception.Message,
        Data = _hostingEnvironment.IsDevelopment() ? context.Exception.GetMessage() : null
      };
      ObjectResult response = new ObjectResult(obj);

      switch (context.Exception)
      {
        case CustomException ex:
          {
            response.StatusCode = (int)ex.StatusCode;
            break;
          }

        default:
          {
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            break;
          }
      }

      context.Result = response;
    }
  }
}