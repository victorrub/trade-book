using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TradeBook.Models;
using TradeBook.Services;

namespace TradeBook.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TradeController : ControllerBase
  {
    private readonly TradeCategoriesService _tradeCategoriesService;

    public TradeController(TradeCategoriesService tradeCategoriesService)
    {
      _tradeCategoriesService = tradeCategoriesService;
    }

    [HttpGet]
    public ActionResult<List<TradeCategories>> Get() => _tradeCategoriesService.Get();

    [HttpPost]
    public ActionResult<Trade> Post(Trade data)
    {
      try
      {
        TradeFactory factory = new TradeRiskFactory();

        TradeRisk myTrade = factory.CreateTrade(data.Value, data.ClientSector);

        return Ok(myTrade);
      }
      catch (Exception)
      {
        return BadRequest();
      }
    }
  }
}
