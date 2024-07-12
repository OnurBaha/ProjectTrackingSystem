using Serilog.Core;
using Serilog.Events;

namespace Core.CrossCuttingConcerns.Logging.SeriLog.CustomColumnEvents;

public class CustomMethodNameColumnEvent : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (logEvent.Properties is not null)
        {
            var (methodName, value) = logEvent.Properties.FirstOrDefault(x => x.Key == "MethodName");
            if (value is not null)
            {
                var getValue = propertyFactory.CreateProperty(methodName, value);
                logEvent.AddPropertyIfAbsent(getValue);
            }
        }
    }
}
