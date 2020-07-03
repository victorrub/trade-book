namespace TradeBook.Models
{
  public class LowRisk : TradeRisk
  {
    public LowRisk(double value, string clientSector) : base(value, clientSector, nameof(LowRisk))
    {
    }
  }
}
