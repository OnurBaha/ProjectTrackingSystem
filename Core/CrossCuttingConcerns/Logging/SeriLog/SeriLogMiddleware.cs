using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Serilog.Context;
using System.Security.Claims;
using System.Text.Json;

namespace Core.CrossCuttingConcerns.Logging.SeriLog;

public class SeriLogMiddleware
{
    readonly RequestDelegate _next;

    public SeriLogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
        var loggingAttributes = endpoint?.Metadata.ToList().Where(c => c.ToString().Contains(nameof(LoggingAttribute))).ToList();

        if (loggingAttributes !=null)
        {
            foreach (var loggingAttribute in loggingAttributes)
            {
                var loggingAttributeType = (LoggingAttribute)loggingAttribute;
                var loggerType = loggingAttributeType.LoggingType;
                var loggerService = (LoggerServiceBase)context.RequestServices.GetService(loggerType);

                if (loggerService != null)
                {
                    var logDetail = GetLogDetail(context);
                    loggerService.Info(logDetail);
                }
            }
        }

        await _next(context);
    }

    private string GetLogDetail(HttpContext context)
    {

        var MethodName = context.Request.Path;
        var user = context.User;
        var username = user.Identity.IsAuthenticated ? user.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Email)?.Value : "Unauthorized";
        var logParameters = new List<LogParameter>();


        var queryStringParams = context.Request.Query;
        foreach (var param in queryStringParams)
        {
            logParameters.Add(new LogParameter()
            {
                Name = param.Key,
                Value = param.Value,
                Type = "QueryString"
            });
        }

        var logDetail = new LogDetail()
        {
            MethodName = MethodName,
            Parameters = logParameters,
            Username = username
        };
        LogContext.PushProperty("Username", username);
        LogContext.PushProperty("MethodName", MethodName);

        var options = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        return JsonSerializer.Serialize(logDetail,options);
    }
}
