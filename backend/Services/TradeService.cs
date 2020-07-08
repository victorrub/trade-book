using MongoDB.Driver;
using System;
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

    public void Create(Trade trade)
    {
      trade.CreatedAt = DateTime.Now;
      trade.UpdatedAt = DateTime.Now;

      Context.Trade.InsertOne(trade);
    }

    public void Update(string id, Trade tradeIn)
    {
      Trade trade = Get(id);

      if (trade == null) throw new ArgumentException("Trade not found");

      tradeIn.Id = trade.Id;
      tradeIn.CreatedAt = trade.CreatedAt;
      tradeIn.UpdatedAt = DateTime.Now;

      Context.Trade.ReplaceOne(trade => trade.Id == id, tradeIn);
    }

    public void Remove(Trade tradeIn) => Context.Trade.DeleteOne(trade => trade.Id == tradeIn.Id);

    public void RemoveAll() => Context.Trade.DeleteMany(trade => true);
  }
}
