using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using WebShopRestService.Models.MongoDB;

namespace WebShopRestService.Repositories.MongoDB
{
    public class CategoriesRepository
    {
        private readonly IMongoCollection<CategoryMongo> _categoriesCollection;

        public CategoriesRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _categoriesCollection = database.GetCollection<CategoryMongo>("categories");
        }

        public async Task CreateAsync(CategoryMongo category)
        {
            await _categoriesCollection.InsertOneAsync(category);
            return;
        }

        public async Task<List<CategoryMongo>> GetAsync()
        {
            return await _categoriesCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateAsync(string id, string name)
        {
            FilterDefinition<CategoryMongo> filter = Builders<CategoryMongo>.Filter.Eq("Id", id);
            UpdateDefinition<CategoryMongo> update = Builders<CategoryMongo>.Update.Set(category => category.Name, name);
            await _categoriesCollection.UpdateOneAsync(filter, update);
            return;
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<CategoryMongo> filter = Builders<CategoryMongo>.Filter.Eq("Id", id);
            await _categoriesCollection.DeleteOneAsync(filter);
            return;
        }
    }
}
