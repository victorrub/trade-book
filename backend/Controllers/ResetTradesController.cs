using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TradeBook.Services;
using TradeBook.Helpers.Requests;

namespace TradeBook.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ResetTradesController : ControllerBase
  {
    private readonly ILogger _logger;
    private readonly TradeRiskService _tradeRiskService;

    public ResetTradesController(ILogger<TradesController> logger,
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
          return Unauthorized();

        _tradeRiskService.RemoveAll();

        return Ok();
      }
      catch (Exception ex)
      {
        _logger.LogError($"> [ResetTrades] Exception : {ex.Message}");
        return BadRequest(new
        {
          Status = "Error",
          Message = "An error occurred while processing your request."
        });
      }
    }

  }
}
