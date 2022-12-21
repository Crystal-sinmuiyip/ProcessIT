using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Restaurant.MongoModel
{
    public class CombinedMenuItemTableOrderSectionList
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //[JsonPropertyName("id")]
        //public string? Id { get; set; }

        [BsonElement("title")]
        [JsonPropertyName("title")]
        public string Title { get; set; } = null!;

        //[BsonElement("sortOrder")]
        //[JsonPropertyName("sortOrder")]
        //public int SortOrder { get; set; } = 0;

        public List<CombinedMenuItemTableOrder>? CombinedMenuItemTableOrder { get; set; }
    }
 
}
