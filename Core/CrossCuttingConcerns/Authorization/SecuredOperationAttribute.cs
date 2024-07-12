namespace Core.CrossCuttingConcerns.Authorization;

public class SecuredOperationAttribute : Attribute
{
    public List<string> Roles { get; }
    public SecuredOperationAttribute(params string[] roles)
    {
        Roles = roles.ToList();
    }
}
