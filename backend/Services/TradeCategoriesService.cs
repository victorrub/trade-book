using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using TradeBook.Models;
using TradeBook.Data;
using TradeBook.Services.Core;

namespace TradeBook.Services
{
  public class TradeCategoriesService : DatabaseServices
  {
    public TradeCategoriesService(TradeBookContext context) : base(context)
    {
    }

    public List<RiskEvaluator> GetRiskCategories()
    {
      List<TradeCategories> categories = Context.TradeCategories.Find(category => true).ToList();
      List<RiskEvaluator> risks = new List<RiskEvaluator>();

      foreach (TradeCategories category in categories)
      {
        risks.Add(new RiskEvaluator(
          category.CategoryName,
          category.MinimumValue,
          category.LimitValue,
          category.ClientSector,
          category.UpdatedAt
        ));
      }

      return risks;
    }
  }
}
