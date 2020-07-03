using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using TradeBook.Models;
using TradeBook.Data;

namespace TradeBook.Services
{
  public class TradeCategoriesService
  {
    private readonly TradeBookContext _context;

    public TradeCategoriesService(TradeBookContext context)
    {
      _context = context;
    }

    public List<TradeCategories> Get() => _context.TradeCategories.Find(trade => true).ToList();
  }
}
