using static WebShopRestService.Models.Category;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebShopRestService.Models;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

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
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Invalid phone number format.")]
    [StringLength(20, ErrorMessage = "Phone number cannot be longer than 20 characters.")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Address ID is required.")]
    [ForeignKey("Address")]
    public int AddressId { get; set; }
    public Address Address { get; set; }

    [Required(ErrorMessage = "User ID is required.")]
    [ForeignKey("UserCredential")]
    public int UserId { get; set; }
    public UserCredential UserCredential { get; set; }

    public ICollection<OrderTable> Orders { get; set; }
}