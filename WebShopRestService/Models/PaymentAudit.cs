using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopRestService.Models
{
    public class PaymentAudit
    {
        // PaymentAudit skal have en unik nøgle, som ikke er defineret i den nuværende skema.
        [Key]
        public int PaymentAuditId { get; set; } // Dette felt skal tilføjes til databasen, hvis det ikke allerede eksisterer.

        [ForeignKey("OrderTable")]
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(9, 2)")]
        public decimal Amount { get; set; }
        public string ActionType { get; set; }
        public DateTime ActionDate { get; set; }

        public OrderTable OrderTable { get; set; }
    }
}
