using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopRestService.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required(ErrorMessage = "Payment method is required.")]
        [StringLength(100, ErrorMessage = "Payment method cannot be longer than 100 characters.")]
        public string PaymentMethod { get; set; }

        [Required(ErrorMessage = "Payment date is required.")]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Column(TypeName = "decimal(10, 2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Order ID is required.")]
        [ForeignKey("OrderTable")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public int OrderId { get; set; }

        public OrderTable OrderTable { get; set; }
    }
}