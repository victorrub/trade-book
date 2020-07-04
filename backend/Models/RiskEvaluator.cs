using System.Collections.Generic;
using TradeBook.Models.Rules;

namespace TradeBook.Models
{
  public class RiskEvaluator : TradeCategories
  {
    private List<IRule> _rules;

    public RiskEvaluator(string categoryName, double minimumValue, double limitValue, string clientSector)
    {
      CategoryName = categoryName;
      MinimumValue = minimumValue;
      LimitValue = limitValue;
      ClientSector = clientSector;

      _rules = new List<IRule>();

      SetRules();
    }

    protected void SetRules()
    {
      _rules.Add(new ClientSectorRule(ClientSector));
      _rules.Add(new MinimumValueRule(MinimumValue));
      _rules.Add(new LimitValueRule(LimitValue));
    }

    public bool VerifyRules(Trade trade)
    {
      foreach (IRule rule in _rules)
      {
        if (!rule.IsMatch(trade))
          return false;
      }

      return true;
    }
  }
}
