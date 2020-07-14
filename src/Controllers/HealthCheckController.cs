using Microsoft.AspNetCore.Mvc;
using TradeBook.Models.Operations;
using TradeBook.Models.Operations.Responses;

namespace TradeBook.Controllers
{
  [ApiController]
  [Route("/")]
  public class HealthCheckController : ControllerBase
  {
    private readonly ResponseContext _responseContext;

    public HealthCheckController()
    {
      _responseContext = new ResponseContext(new HealthCheckResponse());
    }

    [HttpGet]
    public ActionResult Get() => _responseContext.GetResponse();
  }
}
