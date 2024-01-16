using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebShopRestService.Models.MongoDB
{
    public class CustomerMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }

        [BsonElement("address")]
        public AddressMongo Address { get; set; }

        [BsonElement("userCredential")]
        public UserCredentialMongo UserCredential { get; set; }

    }
}
