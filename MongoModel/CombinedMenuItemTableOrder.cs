using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Restaurant.MongoModel
{
    public class CombinedMenuItemTableOrder
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        

        [BsonElement("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [BsonElement("price")]
        [JsonPropertyName("price")]
        public decimal Price { get; set; } = 0;

        [BsonElement("category")]
        [JsonPropertyName("category")]
        public string? Category { get; set; } 


        [BsonElement("availableToday")]
        [JsonPropertyName("availableToday")]
        public bool AvailableToday { get; set; } = true;


        [BsonElement("vegan")]
        [JsonPropertyName("vegan")]
        public bool Vegan { get; set; } = false;

        [BsonElement("glutenFree")]
        [JsonPropertyName("glutenFree")]
        public bool GlutenFree { get; set; } = false;

        [BsonElement("quantity")]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; } = 0;

        [BsonElement("notes")]
        [JsonPropertyName("notes")]
        public string? Notes { get;set; }
    }
 
}
