using TradeBook.Data;

namespace TradeBook.Services.Core
{
  public class DatabaseServices
  {
    protected TradeBookContext Context { get; }

    public DatabaseServices(TradeBookContext context)
    {
      Context = context;
    }
  }
}
