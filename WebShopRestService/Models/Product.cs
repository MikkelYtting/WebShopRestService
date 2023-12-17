using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis; // Required for ICollection

namespace WebShopRestService.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [StringLength(500)]
        public string Img { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Column(TypeName = "decimal(10, 2)")]
        [Range(0.01, 1000000.00, ErrorMessage = "Price must be between 0.01 and 1000000.00")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative.")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Category ID must be a positive number.")] // Updated range to start from 1
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
    }

    public class ProductExtra : Product
    {
        [AllowNull]
        public Category Category { get; set; }
        [AllowNull]
        // Assuming there can be products not yet ordered
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}