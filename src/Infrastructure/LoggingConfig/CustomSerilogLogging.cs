using Application.Common.Interfaces;
using Serilog;

namespace Infrastructure.LoggingConfig;
public class CustomSerilogLogging : ILogging
{
    public void LogInformation(string message)
    {
        Log.Information(message);
    }
}
