namespace TradeMaster.Models
{
  public class LowRisk : Trade
  {
    public LowRisk(double value, string clientSector) : base(value, clientSector, nameof(LowRisk))
    {
    }
  }
}
