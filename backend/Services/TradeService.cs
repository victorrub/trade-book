using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using TradeBook.Models;
using TradeBook.Data;
using TradeBook.Services.Core;

namespace TradeBook.Services
{
  public class TradeService : DatabaseServices
  {
    public TradeService(TradeBookContext context) : base(context)
    {
    }

    public List<Trade> Get() => Context.Trade.Find(trade => true).ToList();

    public Trade Get(string id) => Context.Trade.Find(trade => trade.Id == id).FirstOrDefault();

    public void Create(Trade trade) => Context.Trade.InsertOne(trade);

    public void Update(string id, Trade tradeIn) => Context.Trade.ReplaceOne(trade => trade.Id == id, tradeIn);

    public void Remove(Trade tradeIn) => Context.Trade.DeleteOne(trade => trade.Id == tradeIn.Id);
  }
}
