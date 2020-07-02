namespace TradeMaster.Models
{
  public class Trade : ITrade
  {
    public double Value { get; }

    public string ClientSector { get; }

    public string Category { get; }

    public Trade(double value, string clientSector, string category)
    {
      Value = value;
      ClientSector = clientSector;
      Category = category;
    }
  }
}
