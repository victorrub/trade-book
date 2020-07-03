namespace TradeBook.Models
{
  public interface ITradeBookDatabaseSettings
  {
    DatabaseCollections DatabaseCollections { get; set; }
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
  }
}
