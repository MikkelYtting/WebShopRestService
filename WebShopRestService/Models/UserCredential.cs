using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic; // Required for ICollection

namespace WebShopRestService.Models
{
    public class UserCredential
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Email is required for username.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(255, ErrorMessage = "Password length must be within 255 characters.")]
        public string HashedPassword { get; set; }

        [Required(ErrorMessage = "Role ID is required.")]
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}