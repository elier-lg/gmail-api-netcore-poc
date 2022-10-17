using System.Text;

namespace Web.Extensions
{
  public static class ExceptionExtensions
  {
    public static string GetMessage(this Exception exception, bool includeStackTrace = false)
    {
      var indent = 0;
      var builder = new StringBuilder();
      var innerException = exception.InnerException;
      while (innerException != null)
      {
        builder.Append(innerException.Message);
        builder.AppendLine();
        innerException = innerException.InnerException;
        indent += 4;
        builder.Append(new string(' ', indent));
      }

      if (includeStackTrace)
        builder.AppendLine(exception.StackTrace);

      return builder.ToString();
    }
  }
}