using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebShopRestService.Data;
using WebShopRestService.Interfaces;
using WebShopRestService.Managers;
using WebShopRestService.Models;
using WebShopRestService.Repositories;

[TestClass]
public class OrderTablesManagerTests
{
    private MyDbContext _context;
    private OrderTablesManager _manager;
    private IOrderTablesRepository _orderTableRepository;
    private ICustomersRepository _customerRepository;
    private IAddressesRepository _addressRepository;
    private IProductsRepository _productRepository;

    [TestInitialize]
    public void Initialize()
    {
        // Use an environment variable to get the test connection string
        var connectionString = Environment.GetEnvironmentVariable("TEST_CONNECTION_STRING")
                               ?? "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebshopDatabase-lokal;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"; // Fallback to local connection string

        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;

        _context = new MyDbContext(options);
        _orderTableRepository = new OrderTablesRepository(_context);
        _customerRepository = new CustomersRepository(_context);
        _addressRepository = new AddressesRepository(_context);
        _productRepository = new ProductsRepository(_context);
        _manager = new OrderTablesManager(_customerRepository, _addressRepository, _orderTableRepository, _productRepository);
    }

    [TestMethod]
    public async Task AddOrder_ShouldAddSuccessfully()
    {
        // Arrange
        var newOrder = new OrderTable
        {
            CustomerId = 1, // Assumes a Customer with ID 1 exists in your database
            DeliveryAddressId = 1, // Assumes an Address with ID 1 exists in your database
            OrderDate = DateTime.UtcNow,
            TotalAmount = 100M,
            OrderItems = new System.Collections.Generic.List<OrderItem>
            {
                new OrderItem
                {
                    ProductId = 1, // Assumes a Product with ID 1 exists in your database
                    Quantity = 1,
                    Price = 100M
                }
            }
        };

        // Act
        await _manager.ValidateAndAddOrderAsync(newOrder);

        // Assert
        var addedOrder = await _orderTableRepository.GetOrderByIdAsync(newOrder.OrderId);
        Assert.IsNotNull(addedOrder, "The order should be added successfully.");

        // Cleanup
        if (addedOrder != null)
        {
            await _orderTableRepository.DeleteOrderAsync(addedOrder.OrderId);
        }
    }

    [TestMethod]
    public async Task GetOrderById_ShouldReturnOrder()
    {
        // Arrange
        int orderId = 1; // Replace with a valid order ID

        // Act
        var order = await _manager.GetOrderByIdAsync(orderId);

        // Assert
        Assert.IsNotNull(order, "Should retrieve the order with the specified ID.");
    }

    [TestMethod]
    public async Task UpdateOrder_ShouldModifyOrder()
    {
        // Arrange
        var orderToUpdate = await _context.OrderTables.FirstOrDefaultAsync();
        Assert.IsNotNull(orderToUpdate, "Test requires at least one order in the database.");
        decimal originalTotal = orderToUpdate.TotalAmount;
        orderToUpdate.TotalAmount = originalTotal + 1; // Modify the total amount

        // Act
        await _manager.UpdateOrderAsync(orderToUpdate);

        // Assert
        var updatedOrder = await _orderTableRepository.GetOrderByIdAsync(orderToUpdate.OrderId);
        Assert.AreEqual(originalTotal + 1, updatedOrder.TotalAmount, "The order total should be updated.");

        // Cleanup - reset the total amount
        updatedOrder.TotalAmount = originalTotal;
        await _manager.UpdateOrderAsync(updatedOrder);
    }

    [TestMethod]
    public async Task DeleteOrder_ShouldRemoveOrder()
    {
        // Arrange
        var newOrder = new OrderTable
        {
            CustomerId = 1,
            DeliveryAddressId = 1,
            OrderDate = DateTime.UtcNow,
            TotalAmount = 100M
        };
        await _context.OrderTables.AddAsync(newOrder);
        await _context.SaveChangesAsync();

        // Act
        await _manager.DeleteOrderAsync(newOrder.OrderId);

        // Assert
        var deletedOrder = await _orderTableRepository.GetOrderByIdAsync(newOrder.OrderId);
        Assert.IsNull(deletedOrder, "The order should be deleted.");
    }
    public async Task AddressShouldNotExist_WhenQueriedWithNonExistentId()
    {
        // Arrange - ID set to 1 for testing or any other non-existent ID
        int nonExistentAddressId = 123;

        // Act
        bool exists = await _addressRepository.AddressExistsAsync(nonExistentAddressId);

        // Assert - Check that exists is false, meaning the address does not exist
        Assert.IsFalse(exists, "Address with ID 1 should not exist.");
    }

    [TestMethod]
    public async Task AddressShouldExist_WhenQueriedWithExistingId()
    {
        // Arrange - ID set to a known existing ID in your test database
        int existingAddressId = 1; // Replace with an ID you know exists

        // Act
        bool exists = await _addressRepository.AddressExistsAsync(existingAddressId);

        // Assert - Check that exists is true, meaning the address does exist
        Assert.IsTrue(exists, $"Address with ID {existingAddressId} should exist.");
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Dispose();
    }

    // Helper method to get the connection string, replace with your actual method to retrieve the connection string
    private string GetConnectionString()
    {
        // Normally, you would retrieve the connection string from a configuration file or environment variable
        return "YourConnectionStringHere";
    }
}
