using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebShopRestService.Models.MongoDB
{
    public class ProductMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("stockQuantity")]
        public int StockQuantity { get; set; }

        [BsonElement("category")]
        public CategoryMongo Category { get; set; }

    }
}
