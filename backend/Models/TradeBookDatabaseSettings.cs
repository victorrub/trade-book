namespace TradeBook.Models
{
  public class TradeBookDatabaseSettings : ITradeBookDatabaseSettings
  {
    public DatabaseCollections DatabaseCollections { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
  }
}
