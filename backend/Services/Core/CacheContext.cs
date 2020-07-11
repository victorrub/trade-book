using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace TradeBook.Services.Core
{
  public class CacheContext
  {
    private IDistributedCache _cache { get; }
    private ICacheContextOptions _options { get; }
    private string _topic { get; }

    public CacheContext(IDistributedCache cache, ICacheContextOptions options, string topic)
    {
      _cache = cache;
      _options = options;
      _topic = topic;
    }

    public async Task<string> GetCachedData()
    {
      return await _cache.GetStringAsync(_topic);
    }
    public async Task SaveDataInCache(string data)
    {
      DistributedCacheEntryOptions cacheSettings = new DistributedCacheEntryOptions();
      cacheSettings.SetAbsoluteExpiration(TimeSpan.FromMinutes(_options.ExpirationMinutes));

      await _cache.SetStringAsync(_topic, data, cacheSettings);
    }
  }
}
