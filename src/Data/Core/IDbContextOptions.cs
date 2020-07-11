namespace TradeBook.Data.Core
{
  public interface IDbContextOptions
  {
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
  }
}
