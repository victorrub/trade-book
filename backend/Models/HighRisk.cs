namespace TradeBook.Models
{
  public class HighRisk : Trade
  {
    public HighRisk(double value, string clientSector) : base(value, clientSector, nameof(HighRisk))
    {
    }
  }
}
