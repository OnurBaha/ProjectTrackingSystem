namespace Core.CrossCuttingConcerns.Logging;

public class LogDetail
{
    public string MethodName { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
    public List<LogParameter> Parameters { get; set; }
    public LogDetail()
    {
        FullName = string.Empty;
        MethodName = string.Empty;
        Username = string.Empty;
        Parameters = new List<LogParameter>();
    }

    public LogDetail(string fullName, string methodName, string user, List<LogParameter> parameters)
    {
        fullName = fullName;
        MethodName = methodName;
        Username = user;
        Parameters = parameters;
    }
}