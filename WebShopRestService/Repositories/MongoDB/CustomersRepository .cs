using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using WebShopRestService.Models.MongoDB;

namespace WebShopRestService.Repositories.MongoDB
{
    public class CustomersRepository
    {
        private readonly IMongoCollection<CustomerMongo> _customersCollection;

        public CustomersRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _customersCollection = database.GetCollection<CustomerMongo>("customer");
        }

        public async Task CreateAsync(CustomerMongo customer)
        {
            await _customersCollection.InsertOneAsync(customer);
            return;
        }

        public async Task<List<CustomerMongo>> GetAsync()
        {
            return await _customersCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateAsync(string id, string firstName)
        {
            FilterDefinition<CustomerMongo> filter = Builders<CustomerMongo>.Filter.Eq("Id", id);
            UpdateDefinition<CustomerMongo> update = Builders<CustomerMongo>.Update.Set(customer => customer.FirstName, firstName);
            await _customersCollection.UpdateOneAsync(filter, update);
            return;
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<CustomerMongo> filter = Builders<CustomerMongo>.Filter.Eq("Id", id);
            await _customersCollection.DeleteOneAsync(filter);
            return;
        }
    }
}
