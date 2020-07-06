using TradeBook.Data;
using TradeBook.Models;
using TradeBook.Models.Risks;

namespace TradeBook.Services
{
  public class TradeRiskService : TradeService
  {
    public TradeRiskService(TradeBookContext context) : base(context)
    {
    }

    public void Store(TradeRisk tradeRisk)
    {
      Trade trade = new Trade
      {
        Category = tradeRisk.Category,
        Value = tradeRisk.Value,
        ClientSector = tradeRisk.ClientSector
      };

      Create(trade);
    }

  }
}
