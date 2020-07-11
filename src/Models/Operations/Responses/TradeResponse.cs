using Microsoft.AspNetCore.Mvc;
using TradeBook.Models.Operations.Core;

namespace TradeBook.Models.Operations.Responses
{
  public class TradeResponse : ResponseBase
  {
    protected Trade Trade { get; }

    public TradeResponse(Trade trade) : base("Success")
    {
      Trade = trade;
    }

    public override ActionResult GetResponse()
    {
      return new OkObjectResult(new
      {
        Status,
        RequestedAt,
        Trade
      });
    }
  }
}
