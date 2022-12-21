
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace Restaurant.MongoModel.Services
{
    public class MenuItemsService
    {
       
        private readonly IMongoCollection<MenuItem> _menuItemsCollection;

        public MenuItemsService(
            IOptions<OrderDatabaseSettings> menuStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(menuStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(menuStoreDatabaseSettings.Value.DatabaseName);

            _menuItemsCollection = mongoDatabase.GetCollection<MenuItem>(menuStoreDatabaseSettings.Value.MenuItemsCollectionName);
        }

        public async Task<List<MenuItem>> GetAsync() 
        {
            var result = await _menuItemsCollection.Find(_ => true).ToListAsync();
           // result.Sort((x,y) => x.Category.CompareTo(y.Category));
           List<MenuItem> menuItemsSorted = result.OrderBy(x => x.CategorySort).ToList();
           // List<MenuItem> menuItemsSorted = result.OrderBy(x => x.Category).ToList();
            return menuItemsSorted;
           // result.Sort((x, y) => x.CategorySort.CompareTo(y.CategorySort));
           // return result; 
        }

        public async Task<MenuItem?> GetAsync(string id)
        {
            return await _menuItemsCollection.Find(filter: x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(MenuItem newMenuItem) =>
            await _menuItemsCollection.InsertOneAsync(newMenuItem);

        public async Task UpdateAsync(string id, MenuItem updatedMenuItem) =>
            await _menuItemsCollection.ReplaceOneAsync(x => x.Id == id, updatedMenuItem);

        public async Task RemoveAsync(string id) =>
            await _menuItemsCollection.DeleteOneAsync(x => x.Id == id);
    }
}

