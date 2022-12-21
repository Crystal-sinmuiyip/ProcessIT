using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Restaurant.MongoModel
{
    public class MenuItem
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
        // see category sort below----- used to define what order the categories are to be sorted

        [BsonElement("availableToday")]
        [JsonPropertyName("availableToday")]
        public bool AvailableToday { get; set; } = true;


        [BsonElement("vegan")]
        [JsonPropertyName("vegan")]
        public bool Vegan { get; set; } = false;

        [BsonElement("glutenFree")]
        [JsonPropertyName("glutenFree")]
        public bool GlutenFree { get; set; } = false;

        [BsonElement("categorySort")]
        [JsonPropertyName("categorySort")]
        public int? CategorySort { get; set; } = 0;
    }
    public enum Category
    {
        entree, main, dessert, drink

    }
}
