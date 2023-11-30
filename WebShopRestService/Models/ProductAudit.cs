using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace WebShopRestService.Models
{
    public class ProductAudit
    {
        [Key]
        public int AuditId { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal OldPrice { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal NewPrice { get; set; }
        public DateTime ChangeDate { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
