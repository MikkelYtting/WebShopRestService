using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopRestService.Models
{
    public class ProductAudit
    {
        [Key]
        public int AuditId { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        [Range(0, 1000000.00, ErrorMessage = "Old price must be between 0 and 1000000.00")]
        public decimal OldPrice { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        [Range(0, 1000000.00, ErrorMessage = "New price must be between 0 and 1000000.00")]
        public decimal NewPrice { get; set; }

        [Required(ErrorMessage = "Change date is required.")]
        [DataType(DataType.DateTime)]
        [DateNotInTheFuture(ErrorMessage = "Change date cannot be in the future.")] // uses same method as in paymentaudit
        public DateTime ChangeDate { get; set; }

        [Required(ErrorMessage = "Product ID is required.")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

