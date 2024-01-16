using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebShopRestService.Models.Neo4j
{
    public class OrderItemNeo
    {
        public int OrderItemID { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int OrderID { get; set; }

        public int ProductID { get; set; }
    }
}
