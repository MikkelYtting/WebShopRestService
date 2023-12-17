using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopRestService.Data;
using WebShopRestService.Interfaces;
using WebShopRestService.Managers;
using WebShopRestService.Models;
using WebShopRestService.Repositories; // Assuming your repository classes are in this namespace

[TestClass]
public class CategoriesManagerTest
{
    private static MyDbContext _context;
    private static CategoriesManager _manager;

    [ClassInitialize]
    public static void ClassInitialize(TestContext testContext)
    {
        // Assuming we have a method to get the test connection string
        // var connectionString = GetTestConnectionString();

        // Configure the DbContext with the connection string for the database
        // Azure database
         var options = new DbContextOptionsBuilder<MyDbContext>()
          .UseSqlServer("Server=tcp:mikkelyttingserver.database.windows.net,1433;Initial Catalog=DatabaseForUdviklere-Webshop;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=Active Directory Default;")
         .Options;
        // Local database
       // var options = new DbContextOptionsBuilder<MyDbContext>()
         //   .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebshopDatabase-lokal;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
           // .Options;

        _context = new MyDbContext(options);
        var categoryRepository = new CategoriesRepository(_context);
        _manager = new CategoriesManager(categoryRepository);

        // Seed the database with a known state if necessary, for instance:
        // _context.Categories.Add(new Category { Name = "TestCategory", Description = "TestDescription" });
        // await _context.SaveChangesAsync();
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        // Clean up any data from the test database to reset the state
        _context.Dispose();
    }

    [TestMethod]
    public async Task GetAllCategories_ShouldReturnAllCategories()
    {
        // Arrange

        // Act
        IEnumerable<Category> categories = await _manager.GetAllCategoriesAsync();

        // Assert
        Assert.IsNotNull(categories);
        Assert.IsTrue(categories.Any(), "There should be at least one category.");
    }

    [TestMethod]
    public async Task GetCategoryById_ShouldReturnCategory_WhenCategoryExists()
    {
        // Arrange
        int existingCategoryId = 1; // Replace with a known existing category ID

        // Act
        Category category = await _manager.GetCategoryByIdAsync(existingCategoryId);

        // Assert
        Assert.IsNotNull(category, $"Category with ID {existingCategoryId} should exist.");
    }

    [TestMethod]
    public async Task AddCategory_ShouldAddNewCategory()
    {
        // Arrange
        var newCategory = new Category
        {
            Name = "New Category",
            Description = "New Category Description"
        };

        // Act
        await _manager.AddCategoryAsync(newCategory);

        // Assert
        var createdCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Name == newCategory.Name);
        Assert.IsNotNull(createdCategory, "The category should be found in the database.");
        Assert.AreEqual(newCategory.Name, createdCategory.Name, "The category name should match the created category.");

        // Clean up - remove the created category
        _context.Categories.Remove(createdCategory);
        await _context.SaveChangesAsync();
    }

    [TestMethod]
    public async Task UpdateCategory_ShouldModifyExistingCategory()
    {
        // Arrange
        var categoryToUpdate = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == 1);
        if (categoryToUpdate == null)
        {
            categoryToUpdate = new Category
            {
                Name = "Existing Category",
                Description = "Existing Category Description"
            };
            _context.Categories.Add(categoryToUpdate);
            await _context.SaveChangesAsync();
        }

        // Change some data for the update
        var originalName = categoryToUpdate.Name;
        categoryToUpdate.Name = "Updated Category";

        // Act
        _context.Entry(categoryToUpdate).State = EntityState.Modified;
        await _context.SaveChangesAsync(); // Persist the changes

        // Assert
        var updatedCategory = await _context.Categories.FindAsync(categoryToUpdate.CategoryId);
        Assert.IsNotNull(updatedCategory);
        Assert.AreEqual("Updated Category", updatedCategory.Name, "Category name should be updated.");

        // Cleanup - Optionally reset the name back to the original if needed
        updatedCategory.Name = originalName;
        _context.Entry(updatedCategory).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }



    [TestMethod]
    public async Task DeleteCategory_ShouldRemoveCategory()
    {
        // Arrange
        var categoryToDelete = new Category
        {
            Name = "Delete Category",
            Description = "Delete Category Description"
        };
        _context.Categories.Add(categoryToDelete);
        await _context.SaveChangesAsync();

        // Act
        await _manager.DeleteCategoryAsync(categoryToDelete.CategoryId);

        // Assert
        var deletedCategory = await _context.Categories.FindAsync(categoryToDelete.CategoryId);
        Assert.IsNull(deletedCategory, "Category should be deleted from the database.");
    }

    // No TestCleanup needed since we are using ClassCleanup for disposing the context
}
