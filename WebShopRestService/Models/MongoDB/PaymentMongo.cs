using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebShopRestService.Models.MongoDB
{
    public class PaymentMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("paymentMethod")]
        public string PaymentMethod { get; set; }

        [BsonElement("paymentDate")]
        public DateTime PaymentDate { get; set; }

        [BsonElement("amount")]
        public decimal Amount { get; set; }

        [BsonElement("order")]
        public OrderTable Order { get; set; }

    }
}
