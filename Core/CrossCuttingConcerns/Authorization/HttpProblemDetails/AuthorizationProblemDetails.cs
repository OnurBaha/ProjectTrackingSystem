using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Authorization.HttpProblemDetails;

public class AuthorizationProblemDetails : ProblemDetails
{
    public AuthorizationProblemDetails(string detail)
    {
        Title = "Authorization Violation";
        Detail = detail;
        Status = StatusCodes.Status403Forbidden;
        Type = "https://example.com/probs/authorization";
    }
}