using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebShopRestService.Data;
using WebShopRestService.Managers;
using WebShopRestService.Models;
using WebShopRestService.Repositories; // Ensure to include the repository namespace

[TestClass]
public class AddressesManagerTests
{
    private MyDbContext _context;
    private AddressesManager _manager;
    private AddressesRepository _repository; // Repository instance
    private IDbContextTransaction _transaction;

    [TestInitialize]
    public void Initialize()
    {
        // Assuming we have a method to get the test connection string
        // var connectionString = GetTestConnectionString();

        //Local database
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseSqlServer("Server=tcp:webrestdb.database.windows.net,1433;Initial Catalog=WebRestDB;Persist Security Info=False;User ID=tub2508;Password=Bubber240811;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
            .Options;

        _context = new MyDbContext(options);

        // Create the repository and pass it to the manager
        _repository = new AddressesRepository(_context);
        _manager = new AddressesManager(_repository);

        // Begin a transaction for each test for easy rollback
        _transaction = _context.Database.BeginTransaction();
    }

    [TestMethod]
    public async Task GetAddressesAsync_ShouldReturnAllAddresses()
    {
        // Arrange - Ensure there is data in the test database for Addresses

        // Act
        IEnumerable<Address> addresses = await _manager.GetAddressesAsync();

        // Assert
        Assert.IsNotNull(addresses);
        Assert.IsTrue(addresses.Any(), "There should be at least one address.");
    }

    [TestMethod]
    public async Task GetAddressByIdAsync_ShouldReturnAddress_WhenAddressExists()
    {
        // Arrange
        int existingAddressId = 1; // Replace with an ID you know exists

        // Act
        Address address = await _manager.GetAddressByIdAsync(existingAddressId);

        // Assert
        Assert.IsNotNull(address, $"Address with ID {existingAddressId} should exist.");
    }

    [TestMethod]
    public async Task CreateAddressAsync_ShouldAddNewAddress()
    {
        // Arrange
        var newAddress = new Address // We make a new set of data to create
        {
            Street = "125 New St",
            City = "NewCity",
            PostalCode = "54321",
            Country = "NewCountry"
        };

        // Act
        Address createdAddress = await _manager.CreateAddressAsync(newAddress);

        // Assert
        Assert.IsNotNull(createdAddress, "The creation result should not be null.");
        Assert.IsTrue(createdAddress.AddressId > 0, "Created address should have a non-zero ID.");
    }

    [TestMethod]
    public async Task UpdateAddressAsync_ShouldModifyExistingAddress()
    {
        // Arrange
        var addressToUpdate = await _context.Addresses.AsNoTracking().FirstOrDefaultAsync(a => a.AddressId == 1);
        if (addressToUpdate == null)
        {
            // If the address does not exist in the database, create a dummy address for testing purposes
            addressToUpdate = new Address
            {
                Street = "123 Main St",
                City = "YourCity",
                PostalCode = "12345",
                Country = "YourCountry"
            };
            _context.Addresses.Add(addressToUpdate);
            await _context.SaveChangesAsync();
        }

        // Change some data for the update
        addressToUpdate.Street = "124 Main St";

        // Act
        bool updateResult = await _manager.UpdateAddressAsync(addressToUpdate);

        // Assert
        Assert.IsTrue(updateResult, "Update should be successful.");
        var updatedAddress = await _context.Addresses.FindAsync(addressToUpdate.AddressId);
        Assert.IsNotNull(updatedAddress);
        Assert.AreEqual("124 Main St", updatedAddress.Street, "Street name should be updated.");
    }

    [TestMethod]
    public async Task DeleteAddressAsync_ShouldRemoveAddress()
    {
        // Arrange - Create and save a new address to the database
        var addressToDelete = new Address
        {
            Street = "Delete St",
            City = "Delete City",
            PostalCode = "00000",
            Country = "Delete Country"
        };

        _context.Addresses.Add(addressToDelete);
        await _context.SaveChangesAsync();

        // Act - Attempt to delete the address
        bool deleteResult = await _manager.DeleteAddressAsync(addressToDelete.AddressId);

        // Assert - Verify that the address is no longer in the database
        Assert.IsTrue(deleteResult, "Delete should be successful.");
        var deletedAddress = await _context.Addresses.FindAsync(addressToDelete.AddressId);
        Assert.IsNull(deletedAddress, "Address should be deleted from the database.");
    }

    [TestCleanup]
    public void Cleanup()
    {
        _transaction.Rollback(); // This will undo any changes made during the test
        _transaction.Dispose(); // Dispose the transaction
        _context.Dispose();
    }

    private string GetTestConnectionString()
    {
        // Retrieve the test database connection string from a secure place
        // For example, from environment variables or a local configuration file
        return "YourTestDatabaseConnectionString";
    }
}
