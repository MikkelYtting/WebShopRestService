using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebShopRestService.Models.MongoDB
{
    public class OrderTableMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("orderDate")]
        public DateTime OrderDate { get; set; }

        [BsonElement("totalAmount")]
        public decimal TotalAmount { get; set; }

        [BsonElement("customer")]
        public CustomerMongo Customer { get; set; }

        [BsonElement("deliveryAddress")]
        public AddressMongo Address { get; set; }

        [BsonElement("orderItems")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string[]? OrderItems { get; set; }
    }
}
