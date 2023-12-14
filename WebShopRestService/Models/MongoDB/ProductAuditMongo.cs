using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebShopRestService.Models.MongoDB
{
    public class ProductAuditMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("oldPrice")]
        public decimal OldPrice { get; set; }

        [BsonElement("newPrice")]
        public decimal NewPrice { get; set; }

        [BsonElement("changeDate")]
        public DateTime ChangeDate { get; set; }

        [BsonElement("product")]
        public ProductMongo Product { get; set; }

    }
}
