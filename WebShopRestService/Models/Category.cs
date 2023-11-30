using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopRestService.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters long.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [NotMapped] // instructing the framework to ignore the specified property when generating the database schema
        public ICollection<Product> Products { get; set; }
    }

    // In the code above, the Products collection is present in the Category class, but it will not be represented in the database.
    // This means that you can still use this property in your business logic to associate products with categories,
    // but you will need to handle the persistence of this relationship manually,

    // Fordele
    // Navigation Property: Products acts as a navigation property that allows you to navigate from a Category instance to its related Product instances.
    // For example, if you load a Category from the database, you can directly access all related Product entities through this property.

    // Modeling Relationships: It represents the one-to-many relationship between Category and Product at the model level.
    //This is important for ORM tools to understand how entities are related so they can correctly generate SQL statements for CRUD operations.

    // Ease of Access: When you're working with a Category object in your code, having direct access to its associated Products can make many operations more straightforward.
    // For example, you might want to display all products in a category, or calculate the total stock for a category.
}
