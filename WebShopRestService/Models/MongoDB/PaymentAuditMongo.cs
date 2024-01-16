using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebShopRestService.Models.MongoDB
{
    public class PaymentAuditMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("orderId")]
        public OrderTable OrderId { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("amount")]
        public decimal Amount { get; set; }

        [BsonElement("actionType")]
        public string ActionType { get; set; }

        [BsonElement("actionDate")]
        public DateTime ActionDate { get; set; }
    }
}
