using MongoDB.Driver;
using TradeBook.Models;
using TradeBook.Data.Core;

namespace TradeBook.Data
{
  public class TradeBookContext : DbContext
  {
    public TradeBookContext(IDbContextOptions settings) : base(settings)
    {
    }

    public IMongoCollection<Trade> Trade { get; private set; }
    public IMongoCollection<TradeCategories> TradeCategories { get; private set; }

    protected override void OnModelCreating()
    {
      Trade = ModelBuilder<Trade>();
      TradeCategories = ModelBuilder<TradeCategories>();
    }
  }
}
