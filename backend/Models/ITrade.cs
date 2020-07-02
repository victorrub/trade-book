namespace TradeBook.Models
{
  public interface ITrade
  {
    double Value { get; }
    string ClientSector { get; }
  }
}
