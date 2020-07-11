using TradeBook.Models.Risks;

namespace TradeBook.Models.Factories
{
  public abstract class TradeFactory
  {
    public abstract TradeRisk CreateTrade(string categoryName, double value, string clientSector);
  }
}
