using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using WebShopRestService.Models.MongoDB;

namespace WebShopRestService.Repositories.MongoDB
{
    public class ProductsRepository
    {
        private readonly IMongoCollection<ProductMongo> _productsCollection;

        public ProductsRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _productsCollection = database.GetCollection<ProductMongo>(mongoDBSettings.Value.CollectionName);
        }

        public async Task CreateAsync(ProductMongo product)
        {
            await _productsCollection.InsertOneAsync(product);
            return;
        }

        public async Task<List<ProductMongo>> GetAsync()
        {
            return await _productsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateAsync(string id, decimal price)
        {
            FilterDefinition<ProductMongo> filter = Builders<ProductMongo>.Filter.Eq("Id", id);
            UpdateDefinition<ProductMongo> update = Builders<ProductMongo>.Update.Set(product => product.Price, price);
            await _productsCollection.UpdateOneAsync(filter, update);
            return;
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<ProductMongo> filter = Builders<ProductMongo>.Filter.Eq("Id", id);
            await _productsCollection.DeleteOneAsync(filter);
            return;
        }
    }
}
