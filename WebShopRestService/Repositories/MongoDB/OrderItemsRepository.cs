using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using WebShopRestService.Models.MongoDB;

namespace WebShopRestService.Repositories.MongoDB
{
    public class OrderItemsRepository
    {
        private readonly IMongoCollection<OrderItemMongo> _itemsCollection;

        public OrderItemsRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _itemsCollection = database.GetCollection<OrderItemMongo>("orderitem");
        }

        public async Task CreateAsync(OrderItemMongo item)
        {
            await _itemsCollection.InsertOneAsync(item);
            return;
        }

        public async Task<List<OrderItemMongo>> GetAsync()
        {
            return await _itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateAsync(string id, int quantity)
        {
            FilterDefinition<OrderItemMongo> filter = Builders<OrderItemMongo>.Filter.Eq("Id", id);
            UpdateDefinition<OrderItemMongo> update = Builders<OrderItemMongo>.Update.Set(item => item.Quantity, quantity);
            await _itemsCollection.UpdateOneAsync(filter, update);
            return;
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<OrderItemMongo> filter = Builders<OrderItemMongo>.Filter.Eq("Id", id);
            await _itemsCollection.DeleteOneAsync(filter);
            return;
        }
    }
}
