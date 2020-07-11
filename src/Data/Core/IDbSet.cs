using System;

namespace TradeBook.Data.Core
{
  public interface IDbSet
  {
    string Id { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
  }
}
