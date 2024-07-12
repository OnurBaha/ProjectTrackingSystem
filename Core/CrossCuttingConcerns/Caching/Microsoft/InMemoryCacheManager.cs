using Microsoft.Extensions.Caching.Memory;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Core.CrossCuttingConcerns.Caching.Microsoft;

public class InMemoryCacheManager : ICachingService
{
    private IMemoryCache _cache;

    public InMemoryCacheManager(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void Add(string key, object value, int duration)
    {
        _cache.Set(key, value, TimeSpan.FromMinutes(duration));
    }

    public T Get<T>(string key)
    {
        return _cache.Get<T>(key);
    }

    public object Get(string key)
    {
        return _cache.Get(key);
    }

    public bool IsAdded(string key)
    {
        // discard operator => _
        return _cache.TryGetValue(key, out _);
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }

    public void RemoveByPattern(string pattern)
    {
        var coherentState = typeof(MemoryCache).GetField("_coherentState", BindingFlags.NonPublic | BindingFlags.Instance);

        var coherentStateValue = coherentState.GetValue(_cache);
        var cacheEntriesCollectionDefinition = coherentStateValue.GetType().GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);


        var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(coherentStateValue) as ICollection;

        var cacheCollectionValues = new List<string>();

        if (cacheEntriesCollection != null)
        {
            foreach (var item in cacheEntriesCollection)
            {
                var methodInfo = item.GetType().GetProperty("Key");

                var val = methodInfo.GetValue(item);

                cacheCollectionValues.Add(val.ToString());
            }
        }

        var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d)).Select(d => d)
            .ToList();
        foreach (var key in keysToRemove)
        {
            _cache.Remove(key);
        }
    }
}
