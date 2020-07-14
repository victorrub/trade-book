using Microsoft.AspNetCore.Mvc;
using TradeBook.Models.Operations.Core;

namespace TradeBook.Models.Operations.Responses
{
  public class HealthCheckResponse : ResponseBase
  {
    public HealthCheckResponse() : base("Running")
    {
    }

    public override ActionResult GetResponse()
    {
      return new OkObjectResult(new
      {
        Service = "TradeBookAPI",
        Status,
        CheckedAt = RequestedAt,
      });
    }
  }
}
