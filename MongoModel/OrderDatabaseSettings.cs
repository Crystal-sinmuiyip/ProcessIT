using Microsoft.Extensions.Options;

namespace Restaurant.MongoModel
{
    public class OrderDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string MenuItemsCollectionName { get; set; } = null!;

        public string TableOrdersCollectionName { get; set; } = null!;

       

    }
   
}
