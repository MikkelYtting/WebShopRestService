using MongoDB.Bson.Serialization.Attributes;
using WebShopRestService.Models.MongoDB;

namespace WebShopRestService.Models.Neo4j
{
    public class ProductNeo
    {
        public int ProductID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public string Img { get; set; }
 
        public int CategoryID { get; set; }
    }
}
