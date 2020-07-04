namespace TradeBook.Models.Rules
{
  public interface IRule
  {
    bool IsMatch(Trade trade);
  }
}
