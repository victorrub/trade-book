using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TradeBook.Models;
using TradeBook.Models.Factories;
using TradeBook.Models.Risks;
using TradeBook.Services;

namespace TradeBook.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TradeController : ControllerBase
  {
    private readonly TradeService _tradeService;
    private readonly TradeCategoriesService _tradeCategoriesService;

    public TradeController(TradeService tradeService, TradeCategoriesService tradeCategoriesService)
    {
      _tradeService = tradeService;
      _tradeCategoriesService = tradeCategoriesService;
    }

    [HttpGet]
    public ActionResult<string> Get() => "Hello Trader!";

    [HttpPost]
    public ActionResult<List<TradeRisk>> Post(List<Trade> trades)
    {
      try
      {
        List<RiskEvaluator> categories = _tradeCategoriesService.GetRiskCategories();
        List<TradeRisk> tradeRisks = new List<TradeRisk>();
        TradeFactory factory = new TradeRiskFactory();

        foreach (Trade trade in trades)
        {
          RiskEvaluator selectedTradeRisk = categories.Find(r => r.VerifyRules(trade));

          if (selectedTradeRisk == null)
          {
            Console.WriteLine($"\n > ArgumentException : {trade.Value} / {trade.ClientSector} - The Category compatible with these parameters was not found");
            continue;
          }

          TradeRisk tradeRisk = factory.CreateTrade(selectedTradeRisk.CategoryName, trade.Value, trade.ClientSector);
          tradeRisks.Add(tradeRisk);
        }

        return Ok(tradeRisks);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"> [Trade] Exception : {ex.Message}");
        return BadRequest();
      }
    }
  }
}
