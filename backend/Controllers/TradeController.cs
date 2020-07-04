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
    public ActionResult<Trade> Post(Trade trade)
    {
      try
      {
        List<RiskEvaluator> categories = _tradeCategoriesService.GetRiskCategories();

        RiskEvaluator selectedTradeRisk = categories.Find(r => r.VerifyRules(trade));

        if (selectedTradeRisk == null)
          throw new ArgumentException("The Category compatible with these parameters was not found");

        TradeFactory factory = new TradeRiskFactory();

        TradeRisk myTrade = factory.CreateTrade(selectedTradeRisk.CategoryName, trade.Value, trade.ClientSector);

        return Ok(myTrade.Category);
      }
      catch (ArgumentException ex)
      {
        return NotFound(new { message = ex.Message });
      }
      catch (Exception)
      {
        return BadRequest();
      }
    }
  }
}
