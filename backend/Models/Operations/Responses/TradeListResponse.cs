using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TradeBook.Models.Operations.Core;

namespace TradeBook.Models.Operations.Responses
{
  public class TradeListResponse : ResponseBase
  {
    protected List<Trade> Portfolio { get; }

    public TradeListResponse(List<Trade> portfolio) : base("Success")
    {
      Portfolio = portfolio;
    }

    public override ActionResult GetResponse()
    {
      return new OkObjectResult(new
      {
        Status,
        RequestedAt,
        Portfolio
      });
    }
  }
}
