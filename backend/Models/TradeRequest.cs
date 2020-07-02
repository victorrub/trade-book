namespace TradeMaster.Models
{
  public class TradeRequest : ITrade
  {
    public double Value { get; set; }

    public string ClientSector { get; set; }
  }
}
