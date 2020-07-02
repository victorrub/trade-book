namespace TradeMaster.Models
{
  public class MediumRisk : Trade
  {
    public MediumRisk(double value, string clientSector) : base(value, clientSector, nameof(MediumRisk))
    {
    }
  }
}
