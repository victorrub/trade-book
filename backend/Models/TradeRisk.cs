namespace TradeBook.Models
{
  public class TradeRisk : ITrade
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
