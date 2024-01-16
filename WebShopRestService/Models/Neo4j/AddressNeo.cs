

namespace WebShopRestService.Models.Neo4j
{
    public class AddressNeo
    {
        public int AddressID { get; set; }
        public string Street { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }
    }
}
