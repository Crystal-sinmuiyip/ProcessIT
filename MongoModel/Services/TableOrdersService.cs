
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
namespace Restaurant.MongoModel.Services
{
    public class TableOrdersService
    {

        private readonly IMongoCollection<TableOrder> _tableOrdersCollection;

        public TableOrdersService(
            IOptions<OrderDatabaseSettings> tableOrderStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                tableOrderStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                tableOrderStoreDatabaseSettings.Value.DatabaseName);

            _tableOrdersCollection = mongoDatabase.GetCollection<TableOrder>(
                tableOrderStoreDatabaseSettings.Value.TableOrdersCollectionName);
        }

        public async Task<List<TableOrder>> GetAsync() =>
            await _tableOrdersCollection.Find(_ => true).ToListAsync();
        public async Task<TableOrder> GetIdAsync(string id)
        {

            //  var workSearchTimeRange = DateTime.UtcNow.AddMinutes(-60);
            return await _tableOrdersCollection
                .Find(x => x.Id == id
                    // && x.Created >= workSearchTimeRange
                    && x.Completed == null

                    )
                .FirstOrDefaultAsync();
        }
        public async Task<TableOrder> CurrentOrderForTable(string table)
        {

            List<TableOrder> tableOrders = await _tableOrdersCollection
               .Find(x => x.Table == table
                    && x.Completed == null)
                .ToListAsync();

            var orderedList = tableOrders
                .OrderByDescending(x => x.Created)
                .ToList();

            return orderedList.FirstOrDefault();

        }
        public async Task<TableOrder> CurrentOrderForTableWithTableOrderId(string id)
        {
            return await _tableOrdersCollection
                .Find(x => x.Id == id)
                //       && x.Completed == null)
                //  .SortByDescending(x => x.Created)
                .FirstOrDefaultAsync();
        }
        public async Task<TableOrder> GetAsync(string table)
        {
            // possible order status Seated, Ordering, Pending, Completed, Paid, Available


            var workSearchTimeRange = DateTime.UtcNow.AddMinutes(-60);
            return await _tableOrdersCollection
                .Find(x => x.Table == table
                    && x.Created >= workSearchTimeRange
                    && x.Completed == null
                    && x.OrderStatus != "Available")
                .FirstOrDefaultAsync();
        }

        public async Task<TableOrder> GetBasicAsync(string id)
        {

            return await _tableOrdersCollection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }


        public async Task CreateAsync(TableOrder newTableOrder) =>
            await _tableOrdersCollection.InsertOneAsync(newTableOrder);

        public async Task UpdateAsync(string id, TableOrder updatedTableOrder) =>
            await _tableOrdersCollection.ReplaceOneAsync(x => x.Id == id, updatedTableOrder);

        public async Task RemoveAsync(string id) =>
            await _tableOrdersCollection.DeleteOneAsync(x => x.Id == id);

        public async Task DeleteManyAsync()
        {
            await _tableOrdersCollection.DeleteManyAsync(new BsonDocument());
          //  await _tableOrdersCollection.DeleteManyAsync({ "Created" : {$lt: ISODate("2022-10-18"} } });
        }

        public async Task<List<TableOrder>> GetCurrentTableOrdersByAreaAsync(string area)
        {

            var workDateTime = DateTime.UtcNow;
            workDateTime = workDateTime.AddMinutes(-60);

            List<TableOrder> tableOrders = await _tableOrdersCollection
                         .Find(filter: p =>
                                p.Created >= workDateTime
                         //    && p.Area == area
                             && p.Completed == null
                             && p.OrderStatus != "Completed")
                         .ToListAsync();
            return tableOrders;
        }

       
    }
}