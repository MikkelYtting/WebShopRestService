using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopRestService.Models
{
    public class OrderTable
    {
        [Key] // Marks 'OrderId' as the primary key.
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Order date is required.")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Total amount is required.")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 10000000.00, ErrorMessage = "Total amount must be between 0.01 and 10,000,000.00.")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Customer ID is required.")]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Delivery address ID is required.")]
        [ForeignKey("DeliveryAddress")]
        public int DeliveryAddressId { get; set; }
        public Address DeliveryAddress { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } // An Order can have many OrderItems.

        public OrderTable()
        {
            OrderItems = new HashSet<OrderItem>();
        }
    }
}
