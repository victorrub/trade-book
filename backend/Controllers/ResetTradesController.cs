using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TradeBook.Services;
using TradeBook.Models.Operations.Requests;
using TradeBook.Models.Operations.Responses;

namespace TradeBook.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ResetTradesController : ControllerBase
  {
    private readonly ILogger<ResetTradesController> _logger;
    private readonly TradeRiskService _tradeRiskService;

    public ResetTradesController(ILogger<ResetTradesController> logger,
        TradeRiskService tradeRiskService)
    {
      _logger = logger;
      _tradeRiskService = tradeRiskService;
    }

    [HttpPost]
    public ActionResult Post(ConfirmRequest doubleCheck)
    {
      try
      {
        if (!doubleCheck.Confirm)
        {
          StatusResponse unauthorizedResponse = new StatusResponse("Unauthorized", "Operation not confirmed");
          return unauthorizedResponse.GetResponse();
        }

        _tradeRiskService.RemoveAll();

        StatusResponse successResponse = new StatusResponse("Success", "Portfolio successfully deleted");
        return successResponse.GetResponse();
      }
      catch (Exception ex)
      {
        ErrorResponse<ResetTradesController> errorResponse = new ErrorResponse<ResetTradesController>(_logger, ex);
        return errorResponse.GetResponse();
      }
    }

  }
}
