using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TradeBook.Models.Operations.Core;
using TradeBook.Models.Risks;

namespace TradeBook.Models.Operations.Responses
{
  public class StoreTradesResponse : ResponseBase
  {
    protected List<Trade> Portfolio { get; }
    protected List<TradeRisk> TradeRisks { get; }
    protected List<Object> InvalidTrades { get; }

    public StoreTradesResponse(List<Trade> portfolio, List<TradeRisk> tradeRisks, List<Object> invalidTrades) : base("Success")
    {
      Portfolio = portfolio;
      TradeRisks = tradeRisks;
      InvalidTrades = invalidTrades;
    }

    public override ActionResult GetResponse()
    {
      return new OkObjectResult(new
      {
        Status,
        RequestedAt,
        Trades = new
        {
          Received = Portfolio.Count,
          Success = new
          {
            Total = TradeRisks.Count,
            Cases = TradeRisks
          },
          Invalid = new
          {
            Total = InvalidTrades.Count,
            Cases = InvalidTrades
          }
        }
      });
    }
  }
}
