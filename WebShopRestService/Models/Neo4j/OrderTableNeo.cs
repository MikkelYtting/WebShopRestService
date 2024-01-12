using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebShopRestService.Models.Neo4j
{
    public class OrderTableNeo
    {
        public int OrderID { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public int CustomerID { get; set; }

        public int DeliveryAddressID { get; set; }
    }
}
