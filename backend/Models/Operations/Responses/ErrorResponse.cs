using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TradeBook.Models.Operations.Core;

namespace TradeBook.Models.Operations.Responses
{
  public class ErrorResponse<T> : ResponseBase where T : ControllerBase
  {
    private readonly ILogger _logger;
    protected string Message { get; }

    public ErrorResponse(ILogger<T> logger, Exception ex) : base("Error")
    {
      _logger = logger;
      _logger.LogError($"> [{typeof(T).Name} | {ex.Source}] Exception : {ex.Message}");

      Message = "An error occurred while processing your request.";
    }

    public override ActionResult GetResponse()
    {
      return new BadRequestObjectResult(new
      {
        Status,
        RequestedAt,
        Message
      });
    }
  }
}
