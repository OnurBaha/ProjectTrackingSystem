using Core.CrossCuttingConcerns.Authorization;
using Core.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using AuthorizationException = Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails.Types.AuthorizationException;


public class AuthorizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AuthorizationMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
    {
        _next = next;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task Invoke(HttpContext context)
    {
        var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
        var attribute = endpoint?.Metadata.GetMetadata<SecuredOperationAttribute>();

        if (attribute != null)
        {
            bool hasPermission = false;
            

            if (!hasPermission)
            {
                throw new AuthorizationException(CoreMessages.NoPermission);
            }
        }
        await _next(context);
    }
}
