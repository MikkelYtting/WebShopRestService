using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebShopRestService.Models.Neo4j
{
    public class PaymentNeo
    {
        public int PaymentID { get; set; }

        public string PaymentMethod { get; set; }

        public DateTime PaymentDate { get; set; }

        public decimal Amount { get; set; }

        public int OrderID { get; set; }
    }
}
