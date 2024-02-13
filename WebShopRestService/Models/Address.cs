using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; // Required for ICollection

namespace WebShopRestService.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Required(ErrorMessage = "Street name is required.")]
        [StringLength(50, ErrorMessage = "Street name cannot be longer than 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s,.'-]{3,}$", ErrorMessage = "Invalid street name format.")]
        public string? Street { get; set; }

        [Required(ErrorMessage = "City name is required.")]
        [StringLength(50, ErrorMessage = "City name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s-]+$", ErrorMessage = "Invalid city name format.")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Postal code is required.")]
        [StringLength(10, ErrorMessage = "Postal code cannot be longer than 10 characters.")]
        [RegularExpression(@"^\d{4,5}(-\d{4})?$", ErrorMessage = "Invalid postal code format.")]
        public string? PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(56, ErrorMessage = "Country name cannot be longer than 56 characters.")] // Assuming a max length for country names
        [RegularExpression(@"^[a-zA-Z\s-]+$", ErrorMessage = "Invalid country name format.")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "At least one customer is required.")]
        public ICollection<Customer>? Customers { get; set; }
    }
}