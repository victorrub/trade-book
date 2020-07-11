namespace TradeBook.Models.Rules
{
  public class LimitValueRule : IRule
  {
    private double _limitValue { get; }

    public LimitValueRule(double limitValue)
    {
      _limitValue = limitValue;
    }

    public bool IsMatch(Trade trade) => (trade.Value <= _limitValue || _limitValue == 0);
  }
}
