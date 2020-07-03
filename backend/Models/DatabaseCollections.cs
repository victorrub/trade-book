namespace TradeBook.Models
{
  public class DatabaseCollections : IDatabaseCollections
  {
    public string TradesCollectionName { get; set; }
    public string TradeCategoriesCollectionName { get; set; }
  }
}
