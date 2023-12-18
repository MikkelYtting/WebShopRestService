using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebShopRestService.Data;
using WebShopRestService.Interfaces;
using WebShopRestService.Managers;
using WebShopRestService.Models;
using WebShopRestService.Repositories;

namespace WebShopRestService.IntegrationTest
{
    [TestClass]
    public class CustomersManagerTest
    {
        private MyDbContext _context;
        private ICustomersRepository _repository;
        private CustomersManager _manager;

        [TestInitialize]
        public void Initialize()
        {
            // Assuming we have a method to get the test connection string
            // var connectionString = GetTestConnectionString();

            // Configure the DbContext with the connection string for the database
            // Azure database
            //  var options = new DbContextOptionsBuilder<MyDbContext>()
            //   .UseSqlServer("Server=tcp:mikkelyttingserver.database.windows.net,1433;Initial Catalog=DatabaseForUdviklere-Webshop;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=Active Directory Default;")
            //  .Options;
            //Local database
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebshopDatabase-lokal;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
                .Options;

            _context = new MyDbContext(options);

            // Create the repository and pass it to the manager
            _repository = new CustomersRepository(_context);
            _manager = new CustomersManager(_repository);
        }
    [TestMethod]
    public async Task GetAll_ShouldReturnAllCustomers()
    {
        // Arrange

        // Act
        IEnumerable<Customer> customers = await _manager.GetAll();

        // Assert
        Assert.IsNotNull(customers);
        Assert.IsTrue(customers.Any(), "There should be at least one customer.");
    }

    [TestMethod]
    public async Task Get_ShouldReturnCustomer_WhenCustomerExists()
    {
        // Arrange
        int existingCustomerId = 1; // Replace with a known existing customer ID

        // Act
        Customer customer = await _manager.Get(existingCustomerId);

        // Assert
        Assert.IsNotNull(customer, $"Customer with ID {existingCustomerId} should exist.");
    }
    [TestMethod]
    public async Task Create_ShouldAddNewCustomer()
    {
        // Arrange
        var newCustomer = new Customer
        {
            FirstName = "Test",
            LastName = "User",
            Email = "test@example.com", // Make sure this is set to a non-null value
            Phone = "1234567890",
            AddressId = 1, // Assuming there is an Address with ID 1 in the database
            // ... set other required properties
        };

        // Act
        Customer createdCustomer = await _manager.Create(newCustomer);

        // Assert
        Assert.IsNotNull(createdCustomer, "The creation result should not be null.");
        Assert.IsTrue(createdCustomer.CustomerId > 0, "Created customer should have a non-zero ID.");

        // Clean up - remove the created customer
        _context.Customers.Remove(createdCustomer);
        await _context.SaveChangesAsync();
    }

    [TestMethod]
    public async Task Update_ShouldModifyExistingCustomer()
    {
        // Arrange
        var customerToUpdate = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == 1);
        if (customerToUpdate == null)
        {
            customerToUpdate = new Customer
            {
                // Populate with customer data
            };
            _context.Customers.Add(customerToUpdate);
            await _context.SaveChangesAsync();
        }

        // Make changes to the customer data
        customerToUpdate.FirstName = "Updated Name";

        // Act
        await _manager.Update(customerToUpdate.CustomerId, customerToUpdate);

        // Assert
        var updatedCustomer = await _context.Customers.FindAsync(customerToUpdate.CustomerId);
        Assert.IsNotNull(updatedCustomer);
        Assert.AreEqual("Updated Name", updatedCustomer.FirstName, "Customer name should be updated.");

        // Cleanup - remove the test data
        _context.Customers.Remove(updatedCustomer);
        await _context.SaveChangesAsync();
    }

    [TestMethod]
    public async Task Delete_ShouldRemoveCustomer()
    {
        // Arrange
        var customerToDelete = new Customer
        {
            // Populate with customer data
        };
        _context.Customers.Add(customerToDelete);
        await _context.SaveChangesAsync();

        // Act
        await _manager.Delete(customerToDelete.CustomerId);

        // Assert
        var deletedCustomer = await _context.Customers.FindAsync(customerToDelete.CustomerId);
        Assert.IsNull(deletedCustomer, "Customer should be deleted from the database.");
    }
   /* [TestCleanup]
    public void Cleanup()
    {
        _context.Dispose();
    }
        // No TestCleanup needed since we are using ClassCleanup for disposing the context
    */

    }
   
}