using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebShopRestService.Models.Neo4j
{
    public class ProductAuditNeo
    {
        public int AuditID { get; set; }

        public decimal OldPrice { get; set; }

        public decimal NewPrice { get; set; }

        public DateTime ChangeDate { get; set; }

        public int ProductID { get; set; }
    }
}
