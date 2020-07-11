namespace TradeBook.Services.Core
{
  public class CacheContextOptions : ICacheContextOptions
  {
    public string ConnectionString { get; set; }
    public RedisTopics Topics { get; set; }
    public int ExpirationMinutes { get; set; }
  }

  public class RedisTopics
  {
    public string TradeCategories { get; set; }
  }
}
