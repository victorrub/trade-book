using System;
using Microsoft.AspNetCore.Mvc;

namespace TradeBook.Models.Operations.Core
{
  public abstract class ResponseBase
  {
    protected string Status { get; }
    protected DateTime RequestedAt { get; }

    public ResponseBase(string status)
    {
      Status = status;
      RequestedAt = DateTime.Now;
    }

    public abstract ActionResult GetResponse();
  }
}
