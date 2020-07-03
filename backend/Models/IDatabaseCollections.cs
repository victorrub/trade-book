namespace TradeBook.Models
{
  public interface IDatabaseCollections
  {
    string TradesCollectionName { get; set; }
    string TradeCategoriesCollectionName { get; set; }
  }
}
