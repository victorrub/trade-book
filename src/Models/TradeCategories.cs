using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TradeBook.Data.Core;
using TradeBook.Models.Commom;

namespace TradeBook.Models
{
  public class TradeCategories : ICategory, IDbSet
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    public string Category { get; set; }

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
