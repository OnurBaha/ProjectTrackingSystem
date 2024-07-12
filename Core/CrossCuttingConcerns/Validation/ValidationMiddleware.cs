using Core.CrossCuttingConcerns.Validation;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using ValidationException = Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails.Types.ValidationException;

public class ValidationMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
        var attribute = endpoint?.Metadata.GetMetadata<CustomValidationAttribute>();

        if (attribute != null)
        {
            var requestBody = await GetRequestBody(context.Request);
            if (requestBody != null)
            {
                var validatorType = attribute.ValidatorType;
                var validator = (IValidator)Activator.CreateInstance(validatorType);
                var entityType = validatorType.BaseType.GetGenericArguments()[0];
                var model = JsonConvert.DeserializeObject(requestBody, entityType);
                var validationResult = await ValidateModelAsync(validator, model);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }
            }
        }

        await _next(context);
    }

    private async Task<string> GetRequestBody(HttpRequest request)
    {
        if (request.ContentLength == 0 || request.Body == null || !request.Body.CanRead)
        {
            return null;
        }

        request.EnableBuffering();
        var body = await new StreamReader(request.Body).ReadToEndAsync();
        request.Body.Seek(0, SeekOrigin.Begin);
        return body;
    }

    private async Task<ValidationResult> ValidateModelAsync(IValidator validator, object model)
    {
        dynamic modelValidator = validator;
        return await modelValidator.ValidateAsync((dynamic)model);
    }
}