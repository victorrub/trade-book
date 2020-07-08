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
  public class TradesController : ControllerBase
  {
    private readonly ILogger _logger;
    private readonly TradeCategoriesService _tradeCategoriesService;
    private readonly TradeRiskService _tradeRiskService;

    public TradesController(ILogger<TradesController> logger,
        TradeCategoriesService tradeCategoriesService,
        TradeRiskService tradeRiskService)
    {
      _logger = logger;
      _tradeCategoriesService = tradeCategoriesService;
      _tradeRiskService = tradeRiskService;
    }

    [HttpGet]
    public ActionResult<List<Trade>> Get()
    {
      try
      {
        List<Trade> trades = _tradeRiskService.Get();

        if (trades == null)
          return NotFound();

        return Ok(trades);
      }
      catch (Exception ex)
      {
        _logger.LogError($"> [Trade | Get | All] Exception : {ex.Message}");
        return BadRequest(new
        {
          Status = "Error",
          Message = "An error occurred while processing your request."
        });
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Trade> Get(string id)
    {
      try
      {
        Trade trade = _tradeRiskService.Get(id);

        if (trade == null)
          return NotFound();

        return Ok(trade);
      }
      catch (Exception ex)
      {
        _logger.LogError($"> [Trade | Get | Once] Exception : {ex.Message}");
        return BadRequest(new
        {
          Status = "Error",
          Message = "An error occurred while processing your request."
        });
      }
    }

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
        _logger.LogError($"> [Trade | Post] Exception : {ex.Message}");
        return BadRequest(new
        {
          Status = "Error",
          Message = "An error occurred while processing your request."
        });
      }
    }

    [HttpPut("{id}")]
    public ActionResult Put(string id, [FromBody] Trade tradeIn)
    {
      try
      {
        Trade registeredTrade = _tradeRiskService.Get(id);

        if (registeredTrade == null)
          return NotFound(new { Message = "Trade not found." });

        List<RiskEvaluator> categories = _tradeCategoriesService.GetRiskCategories();
        RiskEvaluator selectedTradeRisk = categories.Find(r => r.VerifyRules(tradeIn));

        if (selectedTradeRisk == null)
          return NotFound(new { Message = "The category compatible with these parameters was not found." });

        tradeIn.Category = selectedTradeRisk.Category;

        _tradeRiskService.Update(id, tradeIn);

        var response = new
        {
          Status = "Success",
          RequestedAt = DateTime.Now,
          Trade = tradeIn
        };

        return Ok(response);
      }
      catch (Exception ex)
      {
        _logger.LogError($"> [Trade | Put] Exception : {ex.Message}");
        return BadRequest(new
        {
          Status = "Error",
          Message = "An error occurred while processing your request."
        });
      }
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(string id)
    {
      try
      {
        Trade registeredTrade = _tradeRiskService.Get(id);

        if (registeredTrade == null)
          return NotFound();

        _tradeRiskService.Remove(registeredTrade);

        return Ok();
      }
      catch (Exception ex)
      {
        _logger.LogError($"> [Trade | Delete] Exception : {ex.Message}");
        return BadRequest(new
        {
          Status = "Error",
          Message = "An error occurred while processing your request."
        });
      }
    }

  }
}
