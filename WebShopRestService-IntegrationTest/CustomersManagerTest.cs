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
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebshopDatabase-lokal;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
            .Options;

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

    [TestMethod]
    public async Task Update_ShouldModifyExistingCustomer()
    {
        // Arrange
        var customerToUpdate = await CreateTestCustomerAsync("Eksisterende", "Bruger");
        customerToUpdate.FirstName = "Opdateret Navn";

        // Act
        await _manager.Update(customerToUpdate.CustomerId, customerToUpdate);

        // Assert
        var updatedCustomer = await _context.Customers.FindAsync(customerToUpdate.CustomerId);
        Assert.IsNotNull(updatedCustomer);
        Assert.AreEqual("Opdateret Navn", updatedCustomer.FirstName, "Kundens navn bør være opdateret.");

        // Cleanup
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
