using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Restaurant.MongoModel
{
    public class AreaTable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("area")]
        [JsonPropertyName("area")]
        public string? Area { get; set; } 

        [BsonElement("table")]
        [JsonPropertyName("table")]
       // public List<Table>? Table { get; set; }
        public string[]? Table { get; set; }

    }
}
