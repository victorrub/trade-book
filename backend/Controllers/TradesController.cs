using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TradeBook.Models;
using TradeBook.Models.Factories;
using TradeBook.Models.Risks;
using TradeBook.Models.Operations;
using TradeBook.Models.Operations.Responses;
using TradeBook.Services;
using System.Threading.Tasks;

namespace TradeBook.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TradesController : ControllerBase
  {
    private readonly ILogger<TradesController> _logger;
    private readonly CachedTradeCategories _tradeCategoriesService;
    private readonly TradeRiskService _tradeRiskService;
    private readonly ResponseContext _responseContext;

    public TradesController(ILogger<TradesController> logger,
        CachedTradeCategories tradeCategoriesService,
        TradeRiskService tradeRiskService)
    {
      _logger = logger;
      _tradeCategoriesService = tradeCategoriesService;
      _tradeRiskService = tradeRiskService;
      _responseContext = new ResponseContext();
    }

    [HttpGet]
    public async Task<ActionResult<List<Trade>>> Get()
    {
      try
      {
        List<Trade> trades = await _tradeRiskService.Get();

        _responseContext.SetResponseContext(new TradeListResponse(trades));
        return _responseContext.GetResponse();
      }
      catch (Exception ex)
      {
        _responseContext.SetResponseContext(new ErrorResponse<TradesController>(_logger, ex));
        return _responseContext.GetResponse();
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Trade>> Get(string id)
    {
      try
      {
        Trade trade = await _tradeRiskService.Get(id);

        if (trade == null)
        {
          _responseContext.SetResponseContext(new StatusResponse("Not Found", "Trade not found"));
          return _responseContext.GetResponse();
        }

        _responseContext.SetResponseContext(new TradeResponse(trade));
        return _responseContext.GetResponse();
      }
      catch (Exception ex)
      {
        _responseContext.SetResponseContext(new ErrorResponse<TradesController>(_logger, ex));
        return _responseContext.GetResponse();
      }
    }

    [HttpPost]
    public async Task<ActionResult> Post(List<Trade> portfolio)
    {
      try
      {
        List<RiskEvaluator> categories = await _tradeCategoriesService.GetRiskCategories();
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

          await _tradeRiskService.Store(tradeRisk);
          tradeRisks.Add(tradeRisk);
        }

        _responseContext.SetResponseContext(new StoreTradesResponse(portfolio, tradeRisks, invalidTrades));
        return _responseContext.GetResponse();
      }
      catch (Exception ex)
      {
        _responseContext.SetResponseContext(new ErrorResponse<TradesController>(_logger, ex));
        return _responseContext.GetResponse();
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(string id, [FromBody] Trade tradeIn)
    {
      try
      {
        Trade registeredTrade = await _tradeRiskService.Get(id);

        if (registeredTrade == null)
        {
          _responseContext.SetResponseContext(new StatusResponse("Not Found", "Trade not found"));
          return _responseContext.GetResponse();
        }

        List<RiskEvaluator> categories = await _tradeCategoriesService.GetRiskCategories();
        RiskEvaluator selectedTradeRisk = categories.Find(rules => rules.VerifyRules(tradeIn));

        if (selectedTradeRisk == null)
        {
          _responseContext.SetResponseContext(new StatusResponse("Not Found", "The category compatible with these parameters was not found."));
          return _responseContext.GetResponse();
        }

        tradeIn.Category = selectedTradeRisk.Category;
        await _tradeRiskService.Update(id, tradeIn);

        _responseContext.SetResponseContext(new TradeResponse(tradeIn));
        return _responseContext.GetResponse();
      }
      catch (Exception ex)
      {
        _responseContext.SetResponseContext(new ErrorResponse<TradesController>(_logger, ex));
        return _responseContext.GetResponse();
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
      try
      {
        Trade registeredTrade = await _tradeRiskService.Get(id);

        if (registeredTrade == null)
        {
          _responseContext.SetResponseContext(new StatusResponse("Not Found", "Trade not found"));
          return _responseContext.GetResponse();
        }

        await _tradeRiskService.Remove(registeredTrade);

        _responseContext.SetResponseContext(new StatusResponse("Success", "Trade successfully deleted"));
        return _responseContext.GetResponse();
      }
      catch (Exception ex)
      {
        _responseContext.SetResponseContext(new ErrorResponse<TradesController>(_logger, ex));
        return _responseContext.GetResponse();
      }
    }
  }
}
