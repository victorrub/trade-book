using TradeBook.Models.Commom;

namespace TradeBook.Models.Risks
{
  public abstract class TradeRisk : ITrade, ICategory
  {
    public double Value { get; }

    public string ClientSector { get; }

    public string Category { get; }

    public TradeRisk(double value, string clientSector, string category)
    {
      Value = value;
      ClientSector = clientSector;
      Category = category;
    }
  }
}
