using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebShopRestService.Models.MongoDB
{
    [BsonIgnoreExtraElements]
    public class OrderItemMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("product")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Product { get; set; }

    }
}
