namespace TradeBook.Models
{
  public class MediumRisk : TradeRisk
  {
    public MediumRisk(double value, string clientSector) : base(value, clientSector, nameof(MediumRisk))
    {
    }
  }
}
