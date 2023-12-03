using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WebShopRestService.Data;
using WebShopRestService.Repositories;

[TestClass]
public class AddressRepositoryIntegrationTests
{
    private MyDbContext _context;
    private AddressRepository _addressRepository;

    // This method will run before each test method is executed.
    [TestInitialize]
    public void TestSetup()
    {
        // Configure the DbContext with the connection string for the test database
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseSqlServer("Server=tcp:mikkelyttingserver.database.windows.net,1433;Initial Catalog=DatabaseForUdviklere-Webshop;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=Active Directory Default;")
            .Options;

        _context = new MyDbContext(options);

        // Initialize the repository with the context
        _addressRepository = new AddressRepository(_context);
    }

    [TestMethod]
    public async Task AddressShouldNotExist_WhenQueriedWithNonExistentId()
    {
        // Arrange - ID set to 1 for testing or any other non-existent ID
        int nonExistentAddressId = 1;

        // Act
        bool exists = await _addressRepository.AddressExistsAsync(nonExistentAddressId);

        // Assert - Check that exists is false, meaning the address does not exist
        Assert.IsFalse(exists, "Address with ID 1 should not exist.");
    }

    // This method will run after each test method is executed.
    [TestCleanup]
    public void TestCleanup()
    {
        _context.Dispose();
    }
}