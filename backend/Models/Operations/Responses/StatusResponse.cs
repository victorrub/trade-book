using Microsoft.AspNetCore.Mvc;
using TradeBook.Models.Operations.Core;

namespace TradeBook.Models.Operations.Responses
{
  public class StatusResponse : ResponseBase
  {
    protected string Message { get; }

    public StatusResponse(string status, string message) : base(status)
    {
      Message = message;
    }

    public override ActionResult GetResponse()
    {
      var responseObject = new
      {
        Status,
        RequestedAt,
        Message
      };

      switch (Status)
      {
        case "Success":
          return new OkObjectResult(responseObject);
        case "Not Found":
          return new NotFoundObjectResult(responseObject);
        case "Unauthorized":
          return new UnauthorizedObjectResult(responseObject);
        default:
          return new BadRequestObjectResult(responseObject);
      }
    }
  }
}
