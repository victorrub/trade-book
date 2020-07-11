namespace TradeBook.Models.Rules
{
  public class ClientSectorRule : IRule
  {
    private string _clientSector { get; }

    public ClientSectorRule(string clientSector)
    {
      _clientSector = clientSector;
    }

    public bool IsMatch(Trade trade) => trade.ClientSector == _clientSector;
  }
}
