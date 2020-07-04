using System;
using TradeBook.Models.Risks;

namespace TradeBook.Models.Factories
{
  public class TradeRiskFactory : TradeFactory
  {
    public override TradeRisk CreateTrade(string categoryName, double value, string clientSector)
    {
      switch (categoryName)
      {
        case "HighRisk":
          return new HighRisk(value, clientSector);
        case "MediumRisk":
          return new MediumRisk(value, clientSector);
        case "LowRisk":
          return new LowRisk(value, clientSector);
        default:
          throw new ArgumentException("The Category compatible with these parameters was not found");
      }
    }
  }
}
