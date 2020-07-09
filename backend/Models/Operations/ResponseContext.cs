using Microsoft.AspNetCore.Mvc;
using TradeBook.Models.Operations.Core;

namespace TradeBook.Models.Operations
{
  public class ResponseContext
  {
    private ResponseBase _responseStrategy;

    public ResponseContext()
    {
    }

    public ResponseContext(ResponseBase responseStrategy)
    {
      _responseStrategy = responseStrategy;
    }

    public void SetResponse(ResponseBase responseStrategy)
    {
      _responseStrategy = responseStrategy;
    }

    public ActionResult GetResponse() => _responseStrategy.GetResponse();
  }
}
