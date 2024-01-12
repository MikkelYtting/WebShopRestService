using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using WebShopRestService.Models.MongoDB;
using System.Security.Authentication;

namespace WebShopRestService.Repositories.MongoDB
{
    public class ProductsRepository
    {
        private readonly IMongoCollection<ProductMongo> _productsCollection;

        public ProductsRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var connectionString = mongoDBSettings.Value.ConnectionURI;
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            //MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _productsCollection = database.GetCollection<ProductMongo>("products");
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
