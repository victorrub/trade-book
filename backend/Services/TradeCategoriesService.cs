using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using TradeBook.Models;
using TradeBook.Data;
using TradeBook.Services.Core;

namespace TradeBook.Services
{
  public class TradeCategoriesService : DatabaseServices
  {
    public TradeCategoriesService(TradeBookContext context) : base(context)
    {
    }

    public List<TradeCategories> Get() => Context.TradeCategories.Find(category => true).ToList();
  }
}
