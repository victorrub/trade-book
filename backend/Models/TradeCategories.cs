using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TradeBook.Data.Core;

namespace TradeBook.Models
{
  public class TradeCategories : IDbSet
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    public string CategoryName { get; set; }

    [BsonElement("minimumValue")]
    public double MinimumValue { get; set; }

    [BsonElement("limitValue")]
    public double LimitValue { get; set; }

    [BsonElement("clientSector")]
    public string ClientSector { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }
  }
}
