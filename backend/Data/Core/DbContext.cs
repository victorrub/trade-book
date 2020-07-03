using MongoDB.Driver;

namespace TradeBook.Data.Core
{
  public abstract class DbContext
  {
    protected MongoClient Client { get; }
    protected IMongoDatabase Database { get; }

    public DbContext(IDbContextOptions options)
    {
      Client = new MongoClient(options.ConnectionString);
      Database = Client.GetDatabase(options.DatabaseName);
      OnModelCreating();
    }

    protected abstract void OnModelCreating();
  }
}
