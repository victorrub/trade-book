namespace TradeBook.Data.Core
{
  public class DbContextOptions : IDbContextOptions
  {
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
  }
}
