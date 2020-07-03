using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TradeBook.Models
{
  public class TradeCategories
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    public string CategoryName { get; set; }

    [BsonElement("min")]
    public double Min { get; set; }

    [BsonElement("limit")]
    public double Limit { get; set; }

    [BsonElement("clientSector")]
    public string ClientSector { get; set; }
  }
}
