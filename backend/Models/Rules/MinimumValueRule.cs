namespace TradeBook.Models.Rules
{
  public class MinimumValueRule : IRule
  {
    protected double _minimumValue { get; }

    public MinimumValueRule(double minimumValue)
    {
      _minimumValue = minimumValue;
    }

    public bool IsMatch(Trade trade) => trade.Value >= _minimumValue;
  }
}
