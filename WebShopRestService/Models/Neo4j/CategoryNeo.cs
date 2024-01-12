using System.ComponentModel.DataAnnotations;

namespace WebShopRestService.Models.Neo4j
{
    public class CategoryNeo
    {
        public int CategoryID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
