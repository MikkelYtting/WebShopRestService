using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebShopRestService.Models.MongoDB
{
    public class UserCredentialMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("hashedPassword")]
        public string HashedPassword { get; set; }

        [BsonElement("role")]
        public RoleMongo Role { get; set; }

    }
}
