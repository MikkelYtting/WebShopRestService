using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using WebShopRestService.Models.MongoDB;

namespace WebShopRestService.Repositories.MongoDB
{
    public class AddressesRepository
    {
        private readonly IMongoCollection<AddressMongo> _addressesCollection;
        
        public AddressesRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _addressesCollection = database.GetCollection<AddressMongo>("address");
        }

        public async Task CreateAsync(AddressMongo product)
        {
            await _addressesCollection.InsertOneAsync(product);
            return;
        }

        public async Task<List<AddressMongo>> GetAsync()
        {
            return await _addressesCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateAsync(string id, AddressMongo address)
        {
            FilterDefinition<AddressMongo> filter = Builders<AddressMongo>.Filter.Eq("Id", id);
            UpdateDefinition<AddressMongo> update = Builders<AddressMongo>.Update.Set(address => address, address);
            await _addressesCollection.UpdateOneAsync(filter, update);
            return;
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<AddressMongo> filter = Builders<AddressMongo>.Filter.Eq("Id", id);
            await _addressesCollection.DeleteOneAsync(filter);
            return;
        }
    }
}
