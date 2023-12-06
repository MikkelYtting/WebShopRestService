using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebShopRestService.Data;
using WebShopRestService.Managers;
using WebShopRestService.Models;


[TestClass]
public class AddressesManagerTests
{
    private MyDbContext _context;
    private AddressesManager _manager;
    private IDbContextTransaction _transaction;

    [TestInitialize]
    public void Initialize()
    {
        // Assuming we have a method to get the test connection string
        // var connectionString = GetTestConnectionString();

        // Configure the DbContext with the connection string for the database
        // Azure database
       // var options = new DbContextOptionsBuilder<MyDbContext>()
          //  .UseSqlServer("Server=tcp:mikkelyttingserver.database.windows.net,1433;Initial Catalog=DatabaseForUdviklere-Webshop;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=Active Directory Default;")
           // .Options;
        // Local database
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebshopDatabase-lokal;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
            .Options;



        
        _context = new MyDbContext(options);
        _manager = new AddressesManager(_context);
        _transaction = _context.Database.BeginTransaction();
    }

    [TestMethod]
    public async Task GetAll_ShouldReturnAllAddresses()
    {
        // Arrange - Ensure there is data in the test database for Addresses

        // Act
        IEnumerable<Address> addresses = await _manager.GetAll();

        // Assert
        Assert.IsNotNull(addresses);
        Assert.IsTrue(addresses.Any(), "There should be at least one address.");
    }

    [TestMethod]
    public async Task Get_ShouldReturnAddress_WhenAddressExists()
    {
        // Arrange
        int existingAddressId = 1; // Replace with an ID you know exists

        // Act
        Address address = await _manager.Get(existingAddressId);

        // Assert
        Assert.IsNotNull(address, $"Address with ID {existingAddressId} should exist.");
    }

    [TestMethod]
    public async Task Create_ShouldAddNewAddress()
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
        Address createdAddress = await _manager.Create(newAddress);

        // Assert
        Assert.IsNotNull(createdAddress, "The creation result should not be null.");
        Assert.IsTrue(createdAddress.AddressId > 0, "Created address should have a non-zero ID.");

        // Additional Assert
        var foundAddress = await _context.Addresses.FirstOrDefaultAsync(a => a.AddressId == createdAddress.AddressId); //When this line is executed, Entity Framework Core will translate the LINQ query into a SQL statement that retrieves the appropriate record from the database. If it finds a record with the matching AddressId, it will be returned and assigned to foundAddress. If no record is found, foundAddress will be null.
        Assert.IsNotNull(foundAddress, "The address should be found in the database.");
        Assert.AreEqual(newAddress.Street, foundAddress.Street, "The street name should match the created address.");

        // Clean up - remove the created address
        _context.Addresses.Remove(foundAddress);
        await _context.SaveChangesAsync();
    }


    [TestMethod]
    public async Task Update_ShouldModifyExistingAddress()
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
        await _manager.Update(addressToUpdate.AddressId, addressToUpdate);

        // Assert
        var updatedAddress = await _context.Addresses.FindAsync(addressToUpdate.AddressId);
        Assert.IsNotNull(updatedAddress);
        Assert.AreEqual("124 Main St", updatedAddress.Street, "Street name should be updated.");
    }

    
    [TestMethod]
    public async Task Delete_ShouldRemoveAddress()
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
        await _manager.Delete(addressToDelete.AddressId);

        // Assert - Verify that the address is no longer in the database
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
