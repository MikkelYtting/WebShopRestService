using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebShopRestService.Models.Neo4j
{
    public class UserCredentialNeo
    {
        public int UserID { get; set; }

        public string Username { get; set; }

        public string HashedPassword { get; set; }

        public int RoleID { get; set; }
    }
}
