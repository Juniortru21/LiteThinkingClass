using Serilog.Core;
using Serilog.Events;

namespace Infrastructure.LoggingConfig;
public class LoggingEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
               "ThreadId", Thread.CurrentThread.ManagedThreadId));

        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
               "Date", DateTime.UtcNow));
    }
}
