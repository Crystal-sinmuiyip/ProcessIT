using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Restaurant.MongoModel
{
    public class TableStatusCurrent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [BsonElement("area")]
        
        [JsonPropertyName("area")]
        public string? Area { get; set; } = "Main";

        [BsonElement("table")]
       
        [JsonPropertyName("table")]
        public string? Table { get; set; } = "M1";

        [BsonElement("tableStatus")]
       
        [JsonPropertyName("tableStatus")]
        public string? TableStatus { get; set; } = "Available";

       

        [BsonElement("created")]
        [JsonPropertyName("created")]
        public DateTime Created { get; set; } = DateTime.UtcNow;
        [BsonElement("completed")]
        [JsonPropertyName("completed")]
        public DateTime? Completed { get; set; }



    }
    public enum TableStatus
    {
         Seated, Ordering, Pending, Completed, Paid, Empty

    }

}