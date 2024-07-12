namespace Core.CrossCuttingConcerns.Caching;

public class CacheRemoveAttribute : Attribute
{
    public string CacheType { get; }

    public CacheRemoveAttribute(string cacheType)
    {
        CacheType = cacheType;
    }
}
