
using System;
using System.Net;

namespace Global.Exceptions
{
  public class CustomException : Exception
  {

    public CustomException(HttpStatusCode statusCode, string message = null, Exception innerException = null)
        : base(message, innerException)
    {
      StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; set; }
  }
}