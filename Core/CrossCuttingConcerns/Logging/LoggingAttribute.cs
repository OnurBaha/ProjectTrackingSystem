using Core.CrossCuttingConcerns.Logging.SeriLog;

namespace Core.CrossCuttingConcerns.Logging;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class LoggingAttribute : Attribute
{
    public Type LoggingType { get; }

    public LoggingAttribute(Type loggerService)
    {
        if (loggerService.BaseType != typeof(LoggerServiceBase))
            throw new Exception("The wrong type of logger was passed.");

        LoggingType = loggerService;
    }
}
