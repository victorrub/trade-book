namespace TradeBook.Services.Core
{
  public interface ICacheContextOptions
  {
    string ConnectionString { get; set; }
    RedisTopics Topics { get; set; }
    int ExpirationMinutes { get; set; }
  }
}
