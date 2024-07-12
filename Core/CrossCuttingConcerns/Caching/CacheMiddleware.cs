using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;


namespace Core.CrossCuttingConcerns.Caching;

public class CacheMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ICachingService _cachingService;

    public CacheMiddleware(RequestDelegate next)
    {
        _next = next;
        _cachingService = ServiceTool.ServiceProvider.GetService<ICachingService>();
    }

    public async Task Invoke(HttpContext context)
    {
        var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
        var cacheAttribute = endpoint?.Metadata.GetMetadata<CacheAttribute>();
        var cacheRemoveAttribute = endpoint?.Metadata.GetMetadata<CacheRemoveAttribute>();

        if (cacheAttribute != null)
        {
            int duration = cacheAttribute.Duration > 0 ? cacheAttribute.Duration : 60;

            await AddCache(context, duration);
        }
        else if (cacheRemoveAttribute != null)
        {
            await RemoveCache(context, cacheRemoveAttribute);
        }
        else
        {
            await _next(context);
        }
    }
    private async Task AddCache(HttpContext context, int duration)
    {
        var methodName = $"{context.Request.Method}.{context.Request.Path}";
        var arguments = context.Request.Query.ToList();

        var cacheKey = $"{methodName}.{string.Join(",", arguments.Select(x => x.ToString() ?? ""))}";

        if (_cachingService.IsAdded(cacheKey))
        {
            var cachedValue = _cachingService.Get(cacheKey);

            var jsonBody = JsonConvert.DeserializeObject((string)cachedValue);
            context.Response.ContentType = "application/json";
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            await context.Response.WriteAsync(JsonConvert.SerializeObject(jsonBody));
            return;
        }

        using (var memoryStream = new MemoryStream())
        {
            var originalResponseBody = context.Response.Body;
            context.Response.Body = memoryStream;

            await _next(context);

            if (context.Response.StatusCode == 200)
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
                var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

                _cachingService.Add(cacheKey, responseBody, duration);

                memoryStream.Seek(0, SeekOrigin.Begin);
                await memoryStream.CopyToAsync(originalResponseBody);
            }
            context.Response.Body = originalResponseBody;
        }
    }

    private async Task RemoveCache(HttpContext context, CacheRemoveAttribute cacheRemoveAttribute)
    {
        _cachingService.RemoveByPattern(cacheRemoveAttribute.CacheType);

        await _next(context);
    }
}