using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Restaurant.MongoModel
{
    public class TableOrderSummary
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [BsonElement("area")]
        [JsonPropertyName("area")]
        public string? Area { get; set; } 

        [BsonElement("table")]
        [JsonPropertyName("table")]
        public string? Table { get; set; } 

        [BsonElement("orderStatus")]
        [JsonPropertyName("orderStatus")]
        public string? OrderStatus { get; set; }

        [BsonElement("waitStaff")]
        [JsonPropertyName("waitStaff")]
        public string? WaitStaff { get; set; }

        [BsonElement("created")]
        [JsonPropertyName("created")]
        public DateTime? Created { get; set; } 

        [BsonElement("completed")]
        [JsonPropertyName("completed")]
        public DateTime? Completed { get; set; }

       
        [BsonElement("totalQuantity")]
        [JsonPropertyName("totalQuantity")]
        public int TotalQuantity { get; set; } = 0;

        [BsonElement("totalPrice")]
        [JsonPropertyName("totalPrice")]
        public decimal TotalPrice { get; set; } = 0;

       

    }
   
    
}