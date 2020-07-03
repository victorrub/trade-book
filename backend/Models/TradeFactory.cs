namespace TradeBook.Models
{
  public abstract class TradeFactory
  {
    public abstract TradeRisk CreateTrade(double value, string clientSector);
  }
}
