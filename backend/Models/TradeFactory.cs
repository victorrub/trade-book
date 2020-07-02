namespace TradeBook.Models
{
  public abstract class TradeFactory
  {
    public abstract Trade CreateTrade(double value, string clientSector);
  }
}
