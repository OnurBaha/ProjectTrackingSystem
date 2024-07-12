namespace Core.CrossCuttingConcerns.Caching;

public class CacheAttribute : Attribute
{

    public int Duration { get; }
    public CacheAttribute()
    {
    }

    public CacheAttribute(int duration)
    {
        Duration = duration;
    }
}
