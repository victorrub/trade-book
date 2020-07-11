using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TradeBook.Models.Operations;
using TradeBook.Models.Operations.Requests;
using TradeBook.Models.Operations.Responses;
using TradeBook.Services;

namespace TradeBook.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ResetTradesController : ControllerBase
  {
    private readonly ILogger<ResetTradesController> _logger;
    private readonly TradeRiskService _tradeRiskService;
    private readonly ResponseContext _responseContext;

    public ResetTradesController(ILogger<ResetTradesController> logger,
        TradeRiskService tradeRiskService)
    {
      _logger = logger;
      _tradeRiskService = tradeRiskService;
      _responseContext = new ResponseContext();
    }

    [HttpPost]
    public async Task<ActionResult> Post(ConfirmRequest doubleCheck)
    {
      try
      {
        if (!doubleCheck.Confirm)
        {
          _responseContext.SetResponse(new StatusResponse("Unauthorized", "Operation not confirmed"));
          return _responseContext.GetResponse();
        }

        await _tradeRiskService.RemoveAll();

        _responseContext.SetResponse(new StatusResponse("Success", "Portfolio successfully deleted"));
        return _responseContext.GetResponse();
      }
      catch (Exception ex)
      {
        _responseContext.SetResponse(new ErrorResponse<ResetTradesController>(_logger, ex));
        return _responseContext.GetResponse();
      }
    }

  }
}
