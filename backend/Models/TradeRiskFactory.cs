using System;

namespace TradeMaster.Models
{
  public class TradeRiskFactory : TradeFactory
  {
    public override Trade CreateTrade(double value, string clientSector)
    {
      if (value >= 1000000.00)
      {
        if (clientSector == "Public")
          return new MediumRisk(value, clientSector);

        return new HighRisk(value, clientSector);
      }

      if (clientSector != "Public")
        throw new ArgumentException();

      return new LowRisk(value, clientSector);
    }
  }
}
