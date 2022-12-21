using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Restaurant.MongoModel
{
    public class TableOrder
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
        public string? WaitStaff { get; set; } = "";

        [BsonElement("created")]
        [JsonPropertyName("created")]
        public DateTime? Created { get; set; } = DateTime.UtcNow;

        [BsonElement("completed")]
        [JsonPropertyName("completed")]
        public DateTime? Completed { get; set; }

        [BsonElement("orderItem")]
        [JsonPropertyName("orderItemArray")]
       // public orderItem[]? OrderItemArray;
        public List<OrderItem>? OrderItemList { get; set; }


    }
    public class OrderItem
    {
        [BsonElement("category")]
        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [BsonElement("menuItem")]
        [JsonPropertyName("menuItemName")]
        public string MenuItemName { get; set; } = "";

        [BsonElement("quantity")]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; } = 0;

        [BsonElement("price")]
        [JsonPropertyName("price")]
        public decimal Price { get; set; } = 0;

        [BsonElement("itemStatus")]
        [JsonPropertyName("itemStatus")]
        public string? ItemStatus { get; set; } 

        [BsonElement("notes")]
        [JsonPropertyName("notes")]
        public string? Notes { get; set; } = "";

    }
    //public class TableAreaArray
    //{
    //    AreaTableValue[,,] WholeRestaurantAreaTable =
    //    {{Area.Main,Table.M1, Area.Main, Table.M2}
        
        
        
    //    }
    //     //   { { 1, 2, 3 }, { 4, 5, 6 } };
    //   // AreaTableValue[,,] WholeRestaurantAreaTable = new ;
    //    class AreaTableValue
    //    {
    //        public Area area { get; set; }
    //        public Table table { get; set; }
    //    }
    //}
    public enum Area
    {
        [BsonRepresentation(BsonType.String)]
        Main,
        [BsonRepresentation(BsonType.String)]
        Outside,
        [BsonRepresentation(BsonType.String)]
        Balcony

    }
    public enum TableAreaMain
    {
        M1, M2, M3, M4, M5, M6, M7, M8, M9, M10
    }
    public enum TableAreaOutside
    { 
        O1, O2, O3, O4, O5, O6, O7, O8, O9, O10
    }
    public enum TableAreaBalcony
    {
        B1, B2, B3, B4, B5, B6, B7, B8, B9, B10
    }
    public enum Table
    {
        [BsonRepresentation(BsonType.String)] M1,
        [BsonRepresentation(BsonType.String)] M2,
        [BsonRepresentation(BsonType.String)] M3,
        [BsonRepresentation(BsonType.String)] M4,
        [BsonRepresentation(BsonType.String)] M5,
        [BsonRepresentation(BsonType.String)] M6,
        [BsonRepresentation(BsonType.String)] M7,
        [BsonRepresentation(BsonType.String)] M8,
        [BsonRepresentation(BsonType.String)] M9,
        [BsonRepresentation(BsonType.String)] M10,
        O1, O2, O3, O4, O5, O6, O7, O8, O9, O10,
        B1, B2, B3, B4, B5, B6, B7, B8, B9, B10
    }
    public enum OrderStatus
    {
        [BsonRepresentation(BsonType.String)]
        Seated,
        [BsonRepresentation(BsonType.String)]
        Ordering,
        [BsonRepresentation(BsonType.String)] 
        Pending,
        [BsonRepresentation(BsonType.String)]
        Completed,
        [BsonRepresentation(BsonType.String)]
        Paid,
        [BsonRepresentation(BsonType.String)]
        Available

    }
    public enum ItemStatus
    {
        [BsonRepresentation(BsonType.String)]  Ordered,
        [BsonRepresentation(BsonType.String)] Cooking,
        [BsonRepresentation(BsonType.String)] Served,
        [BsonRepresentation(BsonType.String)] Completed

    }
    
}