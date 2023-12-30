using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebShopRestService.Data;
using WebShopRestService.Interfaces;
using WebShopRestService.Managers;
using WebShopRestService.Models;
using WebShopRestService.Repositories;

[TestClass]
public class CustomersManagerTest
{
    private MyDbContext _context;
    private ICustomersRepository _repository;
    private CustomersManager _manager;
    private IDbContextTransaction _transaction;

    [TestInitialize]
    public async Task InitializeAsync()
    {
        var connectionString = Environment.GetEnvironmentVariable("TEST_CONNECTION_STRING");

        var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();

        if (!string.IsNullOrEmpty(connectionString))
        {
            // Use MySQL when TEST_CONNECTION_STRING is provided
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        else
        {
            // Fallback to local MSSQL connection string
            connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebshopDatabase-lokal;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            optionsBuilder.UseSqlServer(connectionString);
        }

        var options = optionsBuilder.Options;

        _context = new MyDbContext(options);
        _repository = new CustomersRepository(_context);
        _manager = new CustomersManager(_repository);
        _transaction = await _context.Database.BeginTransactionAsync();
    }



    [TestMethod]
    public async Task GetAll_ShouldReturnAllCustomers()
    {
        // Arrange

        // Act
        IEnumerable<Customer> customers = await _manager.GetAll();

        // Assert
        Assert.IsNotNull(customers);
        Assert.IsTrue(customers.Any(), "Der bør være mindst én kunde.");
    }

    [TestMethod]
    public async Task Get_ShouldReturnCustomer_WhenCustomerExists()
    {
        // Arrange
        int existingCustomerId = 1;

        // Act
        Customer customer = await _manager.Get(existingCustomerId);

        // Assert
        Assert.IsNotNull(customer, $"Kunde med ID {existingCustomerId} bør eksistere.");
    }

    [TestMethod]
    public async Task Create_ShouldAddNewCustomer()
    {
        // Arrange
        var newCustomer = await CreateTestCustomerAsync("Test", "User");

        // Act
        Customer createdCustomer = await _manager.Create(newCustomer);

        // Assert
        Assert.IsNotNull(createdCustomer, "Resultatet af oprettelsen bør ikke være null.");
        Assert.IsTrue(createdCustomer.CustomerId > 0, "Oprettet kunde bør have et ikke-nul ID.");

        // Cleanup
        _context.Customers.Remove(createdCustomer);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Tests the functionality of updating an existing customer in the database.
    /// The method follows these steps:
    /// 1. Create and add a new test customer to the database.
    /// 2. Retrieve (reload) the customer from the database to ensure it's being tracked by the DbContext.
    /// 3. Modify the customer's details.
    /// 4. Update the customer using the manager's update method.
    /// 5. Retrieve the customer again to verify the update.
    /// 6. Clean up by removing the test customer from the database.
    /// This test ensures that the update functionality works as expected.
    /// </summary>
    [TestMethod]
    public async Task Update_ShouldModifyExistingCustomer()
    {
        // Arrange
        // Create a new test customer and add it to the database.
        var customerToUpdate = await CreateTestCustomerAsync("Eksisterende", "Bruger");
        _context.Customers.Add(customerToUpdate);
        await _context.SaveChangesAsync();

        // Reload the customer from the database to ensure it's being tracked by the DbContext.
        // This step is crucial for avoiding the DbUpdateConcurrencyException.
        customerToUpdate = await _context.Customers.FindAsync(customerToUpdate.CustomerId);

        // Modify the customer's first name.
        customerToUpdate.FirstName = "Opdateret Navn";

        // Act
        // Update the customer in the database using the manager's method.
        await _manager.Update(customerToUpdate.CustomerId, customerToUpdate);

        // Assert
        // Retrieve the updated customer and assert that the changes have been saved.
        var updatedCustomer = await _context.Customers.FindAsync(customerToUpdate.CustomerId);
        Assert.IsNotNull(updatedCustomer);
        Assert.AreEqual("Opdateret Navn", updatedCustomer.FirstName, "Kundens navn bør være opdateret.");

        // Cleanup
        // Remove the test customer from the database.
        _context.Customers.Remove(updatedCustomer);
        await _context.SaveChangesAsync();
    }



    [TestMethod]
    public async Task Delete_ShouldRemoveCustomer()
    {
        // Arrange
        var customerToDelete = await CreateTestCustomerAsync("Slet", "Bruger");

        // Act
        await _manager.Delete(customerToDelete.CustomerId);

        // Assert
        var deletedCustomer = await _context.Customers.FindAsync(customerToDelete.CustomerId);
        Assert.IsNull(deletedCustomer, "Kunden bør være slettet fra databasen.");
    }

    private async Task<Customer> CreateTestCustomerAsync(string firstName, string lastName)
{
    var address = await EnsureAddressExists();
    var userCredential = await EnsureUserCredentialExists();

    // Create a new Customer instance without adding it to the context
    return new Customer
    {
        FirstName = firstName,
        LastName = lastName,
        Email = $"temp{Guid.NewGuid()}@example.com", // Ensure unique email
        Phone = "1234567890",
        AddressId = address.AddressId,
        UserId = userCredential.UserId
    };
}


    private async Task<Address> EnsureAddressExists()
    {
        var address = await _context.Addresses.FirstOrDefaultAsync();
        if (address == null)
        {
            address = new Address
            {
                Street = "123 Example St",
                City = "ExampleCity",
                PostalCode = "12345",
                Country = "ExampleCountry"
            };
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
        }
        return address;
    }

    private async Task<UserCredential> EnsureUserCredentialExists()
    {
        var userCredential = await _context.UserCredentials.FirstOrDefaultAsync();
        if (userCredential == null)
        {
            var role = await _context.Roles.FirstOrDefaultAsync();
            if (role == null)
            {
                role = new Role { Name = "Customer", AccessLevel = 1 };
                _context.Roles.Add(role);
                await _context.SaveChangesAsync();
            }

            userCredential = new UserCredential
            {
                Username = "testuser",
                HashedPassword = "hashedpassword",
                UserId = role.RoleId
            };
            _context.UserCredentials.Add(userCredential);
            await _context.SaveChangesAsync();
        }
        return userCredential;
    }

    [TestCleanup]
    public async Task CleanupAsync()
    {
        await _transaction?.RollbackAsync();
        _transaction?.Dispose();
        _context?.Dispose();
    }
}
