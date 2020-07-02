using System;
using Microsoft.AspNetCore.Mvc;
using TradeBook.Models;

namespace TradeBook.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TradeController : ControllerBase
  {
    [HttpGet]
    public ActionResult<string> Get()
    {
      return Ok("Hello Trade!");
    }

    [HttpPost]
    public ActionResult<Trade> Post(TradeRequest data)
    {
      try
      {
        TradeFactory factory = new TradeRiskFactory();

        Trade myTrade = factory.CreateTrade(data.Value, data.ClientSector);

        return Ok(myTrade);
      }
      catch (Exception)
      {
        return BadRequest();
      }
    }
  }
}
