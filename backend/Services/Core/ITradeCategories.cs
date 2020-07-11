using System.Collections.Generic;
using System.Threading.Tasks;
using TradeBook.Models;

namespace TradeBook.Services.Core
{
  public interface ITradeCategoriesService
  {
    Task<List<RiskEvaluator>> GetRiskCategories();
  }
}
