namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails.Types;

public class AuthorizationException : Exception
{
    public AuthorizationException(string? message) : base(message) { }
}
