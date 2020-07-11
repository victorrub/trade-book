using MongoDB.Driver;
using System;
using System.Collections.Generic;
using TradeBook.Models;
using TradeBook.Data;
using TradeBook.Services.Core;
using System.Threading.Tasks;

namespace TradeBook.Services
{
  public class TradeService : DatabaseServices
  {
    public TradeService(TradeBookContext context) : base(context)
    {
    }

    public async Task<List<Trade>> Get() => await Context.Trade.Find(trade => true).ToListAsync();

    public async Task<Trade> Get(string id) => await Context.Trade.Find(trade => trade.Id == id).FirstOrDefaultAsync();

    public async Task Create(Trade trade)
    {
      trade.CreatedAt = DateTime.Now;
      trade.UpdatedAt = DateTime.Now;

      await Context.Trade.InsertOneAsync(trade);
    }

    public async Task Update(string id, Trade tradeIn)
    {
      Trade trade = await Get(id);

      if (trade == null) throw new ArgumentException("Trade not found");

      tradeIn.Id = trade.Id;
      tradeIn.CreatedAt = trade.CreatedAt;
      tradeIn.UpdatedAt = DateTime.Now;

      await Context.Trade.ReplaceOneAsync(trade => trade.Id == id, tradeIn);
    }

    public async Task Remove(Trade tradeIn) => await Context.Trade.DeleteOneAsync(trade => trade.Id == tradeIn.Id);

    public async Task RemoveAll() => await Context.Trade.DeleteManyAsync(trade => true);
  }
}
