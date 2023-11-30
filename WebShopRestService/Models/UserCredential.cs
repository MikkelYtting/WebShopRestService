using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace WebShopRestService.Models
{
    public class UserCredential
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        // Brugeroplysninger er knyttet til kunder.
        public ICollection<Customer> Customers { get; set; }
    }
}
