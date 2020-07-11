using System.Threading.Tasks;
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

    public async Task Store(TradeRisk tradeRisk)
    {
      Trade trade = new Trade
      {
        Category = tradeRisk.Category,
        Value = tradeRisk.Value,
        ClientSector = tradeRisk.ClientSector
      };

      await Create(trade);
    }

  }
}
