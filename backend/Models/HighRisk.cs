namespace TradeBook.Models
{
  public class HighRisk : TradeRisk
  {
    public HighRisk(double value, string clientSector) : base(value, clientSector, nameof(HighRisk))
    {
    }
  }
}
