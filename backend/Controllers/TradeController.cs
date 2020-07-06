using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger _logger;
    private readonly TradeCategoriesService _tradeCategoriesService;
    private readonly TradeRiskService _tradeRiskService;

    public TradeController(ILogger<TradeController> logger, TradeCategoriesService tradeCategoriesService, TradeRiskService tradeRiskService)
    {
      _logger = logger;
      _tradeCategoriesService = tradeCategoriesService;
      _tradeRiskService = tradeRiskService;
    }

    [HttpGet]
    public ActionResult<List<Trade>> Get() => _tradeRiskService.Get();

    [HttpPost]
    public ActionResult Post(List<Trade> trades)
    {
      try
      {
        List<RiskEvaluator> categories = _tradeCategoriesService.GetRiskCategories();
        TradeFactory factory = new TradeRiskFactory();

        List<TradeRisk> tradeRisks = new List<TradeRisk>();
        List<Object> invalidTrades = new List<Object>();

        foreach (Trade trade in trades)
        {
          RiskEvaluator selectedTradeRisk = categories.Find(r => r.VerifyRules(trade));

          if (selectedTradeRisk == null)
          {
            invalidTrades.Add(new
            {
              Value = trade.Value,
              ClientSector = trade.ClientSector,
              Message = "The category compatible with these parameters was not found."
            });

            continue;
          }

          TradeRisk tradeRisk = factory.CreateTrade(selectedTradeRisk.Category, trade.Value, trade.ClientSector);

          _tradeRiskService.Store(tradeRisk);
          tradeRisks.Add(tradeRisk);
        }

        var response = new
        {
          Status = "Success",
          RequestedAt = DateTime.Now,
          Trades = new
          {
            Received = trades.Count,
            Success = new
            {
              Total = tradeRisks.Count,
              Cases = tradeRisks
            },
            Invalid = new
            {
              Total = invalidTrades.Count,
              Cases = invalidTrades
            }
          }
        };

        return Ok(response);
      }
      catch (Exception ex)
      {
        _logger.LogError($"> [Trade] Exception : {ex.Message}");
        return BadRequest(new
        {
          Status = "Error",
          Message = "An error occurred while processing your request."
        });
      }
    }
  }
}
