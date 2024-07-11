using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using ValidationException = Core.CrossCuttingConcerns.Exceptions.Types.ValidationException;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ValidateModelAttribute : ActionFilterAttribute
{
    private readonly Type _validatorType;

    public ValidateModelAttribute(Type validatorType)
    {
        if (validatorType == null)
            throw new ArgumentNullException(nameof(validatorType));

        if (!typeof(IValidator).IsAssignableFrom(validatorType))
            throw new ArgumentException($"{validatorType.Name} is not a valid validator.");

        _validatorType = validatorType; 
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        foreach (var parameter in context.ActionArguments.Values)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var validationContext = new ValidationContext<object>(parameter);
            var validationResult = validator.Validate(validationContext);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.Select(error => new ValidationExceptionModel
                {
                    Property = error.PropertyName,
                    Errors = new List<string> { error.ErrorMessage }
                }));
            }
        }
    }
}
