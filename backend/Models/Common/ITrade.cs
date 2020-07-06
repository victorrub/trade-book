namespace TradeBook.Models.Commom
{
  public interface ITrade
  {
    double Value { get; }
    string ClientSector { get; }
  }
}
