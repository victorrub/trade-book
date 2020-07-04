using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TradeBook.Models
{
  public class Trade : ITrade
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("value")]
    public double Value { get; set; }

    [BsonElement("clientSector")]
    public string ClientSector { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }
  }
}
