using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using TradeBook.Models;
using TradeBook.Data;
using TradeBook.Services.Core;
using System.Threading.Tasks;

namespace TradeBook.Services
{
  public class TradeCategoriesService : DatabaseServices, ITradeCategoriesService
  {
    public TradeCategoriesService(TradeBookContext context) : base(context)
    {
    }

    public async Task<List<RiskEvaluator>> GetRiskCategories()
    {
      var categories = await Context.TradeCategories.Find(category => true).ToListAsync();

      var risks = categories.Select(category => Task.Factory.StartNew(() =>
      {
        return new RiskEvaluator(
          category.Category,
          category.MinimumValue,
          category.LimitValue,
          category.ClientSector,
          category.UpdatedAt
        );
      }));

      return (await Task.WhenAll(risks)).ToList();
    }
  }
}
