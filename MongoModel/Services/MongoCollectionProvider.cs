
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace Restaurant.MongoModel.Services
{
    public class MongoCollectionProvider
    {
        private readonly IMongoClient _client;

        public MongoCollectionProvider(IMongoClient client)
        {
            _client = client;
        }

        public IMongoCollection<T> GetCollection<T>(string database, string collection)
        {
            var db = _client.GetDatabase(database);
            return db.GetCollection<T>(collection);
        }
    }
}
