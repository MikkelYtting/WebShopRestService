using System.ComponentModel.DataAnnotations;

namespace WebShopRestService.Models
{
    public class RegisterCustomer
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s-]+$", ErrorMessage = "Invalid first name format.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s-]+$", ErrorMessage = "Invalid last name format.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        [RegularExpression(@"^(?!.*\.\.)[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(20, ErrorMessage = "Phone number cannot be longer than 20 characters.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Street name is required.")]
        [StringLength(100, ErrorMessage = "Street name cannot be longer than 100 characters.")]
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

        [Required(ErrorMessage = "Email is required for username.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(255, ErrorMessage = "Password length must be within 255 characters.")]
        public string Password { get; set; }
    }
}
