using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TradeBook.Models;
using TradeBook.Models.Factories;
using TradeBook.Models.Risks;
using TradeBook.Services;
using TradeBook.Models.Operations.Responses;

namespace TradeBook.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TradesController : ControllerBase
  {
    private readonly ILogger<TradesController> _logger;
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

        TradeListResponse successResponse = new TradeListResponse(trades);
        return successResponse.GetResponse();
      }
      catch (Exception ex)
      {
        ErrorResponse<TradesController> errorResponse = new ErrorResponse<TradesController>(_logger, ex);
        return errorResponse.GetResponse();
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Trade> Get(string id)
    {
      try
      {
        Trade trade = _tradeRiskService.Get(id);

        if (trade == null)
        {
          StatusResponse notFoundResponse = new StatusResponse("Not Found", "Trade not found");
          return notFoundResponse.GetResponse();
        }

        TradeResponse successResponse = new TradeResponse(trade);
        return successResponse.GetResponse();
      }
      catch (Exception ex)
      {
        ErrorResponse<TradesController> errorResponse = new ErrorResponse<TradesController>(_logger, ex);
        return errorResponse.GetResponse();
      }
    }

    [HttpPost]
    public ActionResult Post(List<Trade> portfolio)
    {
      try
      {
        List<RiskEvaluator> categories = _tradeCategoriesService.GetRiskCategories();
        TradeFactory factory = new TradeRiskFactory();

        List<TradeRisk> tradeRisks = new List<TradeRisk>();
        List<Object> invalidTrades = new List<Object>();

        foreach (Trade trade in portfolio)
        {
          RiskEvaluator selectedTradeRisk = categories.Find(rules => rules.VerifyRules(trade));

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

        StoreTradesResponse successResponse = new StoreTradesResponse(portfolio, tradeRisks, invalidTrades);
        return successResponse.GetResponse();
      }
      catch (Exception ex)
      {
        ErrorResponse<TradesController> errorResponse = new ErrorResponse<TradesController>(_logger, ex);
        return errorResponse.GetResponse();
      }
    }

    [HttpPut("{id}")]
    public ActionResult Put(string id, [FromBody] Trade tradeIn)
    {
      try
      {
        Trade registeredTrade = _tradeRiskService.Get(id);

        if (registeredTrade == null)
        {
          StatusResponse notFoundResponse = new StatusResponse("Not Found", "Trade not found");
          return notFoundResponse.GetResponse();
        }

        List<RiskEvaluator> categories = _tradeCategoriesService.GetRiskCategories();
        RiskEvaluator selectedTradeRisk = categories.Find(rules => rules.VerifyRules(tradeIn));

        if (selectedTradeRisk == null)
        {
          StatusResponse notFoundResponse = new StatusResponse("Not Found", "The category compatible with these parameters was not found.");
          return notFoundResponse.GetResponse();
        }

        tradeIn.Category = selectedTradeRisk.Category;
        _tradeRiskService.Update(id, tradeIn);

        TradeResponse successResponse = new TradeResponse(tradeIn);
        return successResponse.GetResponse();
      }
      catch (Exception ex)
      {
        ErrorResponse<TradesController> errorResponse = new ErrorResponse<TradesController>(_logger, ex);
        return errorResponse.GetResponse();
      }
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(string id)
    {
      try
      {
        Trade registeredTrade = _tradeRiskService.Get(id);

        if (registeredTrade == null)
        {
          StatusResponse notFoundResponse = new StatusResponse("Not Found", "Trade not found");
          return notFoundResponse.GetResponse();
        }

        _tradeRiskService.Remove(registeredTrade);

        StatusResponse successResponse = new StatusResponse("Success", "Trade successfully deleted");
        return successResponse.GetResponse();
      }
      catch (Exception ex)
      {
        ErrorResponse<TradesController> errorResponse = new ErrorResponse<TradesController>(_logger, ex);
        return errorResponse.GetResponse();
      }
    }

  }
}
