using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using TradeBook.Models;

namespace TradeBook.Services
{
  public class TradeCategoriesService
  {
    private readonly IMongoCollection<TradeCategories> _categories;

    public TradeCategoriesService(ITradeBookDatabaseSettings settings)
    {
      var client = new MongoClient(settings.ConnectionString);
      var database = client.GetDatabase(settings.DatabaseName);

      _categories = database.GetCollection<TradeCategories>(
        settings.DatabaseCollections.TradeCategoriesCollectionName
      );
    }

    public List<TradeCategories> Get() => _categories.Find(trade => true).ToList();
  }
}
