using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using WebShopRestService.Models.MongoDB;

namespace WebShopRestService.Repositories.MongoDB
{
    public class OrderTablesRepository
    {
        private readonly IMongoCollection<OrderTableMongo> _ordersCollection;

        public OrderTablesRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _ordersCollection = database.GetCollection<OrderTableMongo>("order");
        }

        public async Task CreateAsync(OrderTableMongo order)
        {
            await _ordersCollection.InsertOneAsync(order);
            return;
        }

        public async Task<List<OrderTableMongo>> GetAsync()
        {
            return await _ordersCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateAsync(string id, decimal totalAmount)
        {
            FilterDefinition<OrderTableMongo> filter = Builders<OrderTableMongo>.Filter.Eq("Id", id);
            UpdateDefinition<OrderTableMongo> update = Builders<OrderTableMongo>.Update.Set(order => order.TotalAmount, totalAmount);
            await _ordersCollection.UpdateOneAsync(filter, update);
            return;
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<OrderTableMongo> filter = Builders<OrderTableMongo>.Filter.Eq("Id", id);
            await _ordersCollection.DeleteOneAsync(filter);
            return;
        }
    }
}
