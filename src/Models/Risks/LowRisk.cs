namespace TradeBook.Models.Risks
{
  public class LowRisk : TradeRisk
  {
    public LowRisk(double value, string clientSector) : base(value, clientSector, nameof(LowRisk))
    {
    }
  }
}
