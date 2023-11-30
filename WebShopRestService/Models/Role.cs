using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopRestService.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string Name { get; set; }
        public int AccessLevel { get; set; }

        // Roller er knyttet til flere bruger. Roler kan eksistere uden at være linked til en bruger
        public ICollection<UserCredential> UserCredentials { get; set; }
    }
}
