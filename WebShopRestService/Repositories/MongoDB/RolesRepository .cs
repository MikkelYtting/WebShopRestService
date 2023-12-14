using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using WebShopRestService.Models.MongoDB;

namespace WebShopRestService.Repositories.MongoDB
{
    public class RolesRepository
    {
        private readonly IMongoCollection<RoleMongo> _rolesCollection;

        public RolesRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _rolesCollection = database.GetCollection<RoleMongo>("role");
        }

        public async Task CreateAsync(RoleMongo role)
        {
            await _rolesCollection.InsertOneAsync(role);
            return;
        }

        public async Task<List<RoleMongo>> GetAsync()
        {
            return await _rolesCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateAsync(string id, string name)
        {
            FilterDefinition<RoleMongo> filter = Builders<RoleMongo>.Filter.Eq("Id", id);
            UpdateDefinition<RoleMongo> update = Builders<RoleMongo>.Update.Set(role => role.Name, name);
            await _rolesCollection.UpdateOneAsync(filter, update);
            return;
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<RoleMongo> filter = Builders<RoleMongo>.Filter.Eq("Id", id);
            await _rolesCollection.DeleteOneAsync(filter);
            return;
        }
    }
}
