using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using TradeBook.Models;
using TradeBook.Services.Core;

namespace TradeBook.Services
{
  public class CachedTradeCategories : CacheContext, ITradeCategoriesService
  {
    private TradeCategoriesService _tradeCategoriesService;

    public CachedTradeCategories(IDistributedCache cache,
      ICacheContextOptions options,
      TradeCategoriesService tradeCategoriesService)
      : base(cache, options, options.Topics.TradeCategories)
    {
      _tradeCategoriesService = tradeCategoriesService;
    }

    public async Task<List<RiskEvaluator>> GetRiskCategories()
    {
      string dataCache = await GetCachedData();

      if (string.IsNullOrEmpty(dataCache))
      {
        var categoriesFromDatabase = await _tradeCategoriesService.GetRiskCategories();

        string categoriesString = JsonConvert.SerializeObject(categoriesFromDatabase);

        await SaveDataInCache(categoriesString);

        return categoriesFromDatabase;
      }

      var categoriesFromCache = JsonConvert.DeserializeObject<List<RiskEvaluator>>(dataCache);

      return await Task.FromResult(categoriesFromCache);
    }
  }
}
