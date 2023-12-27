using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopRestService.Data;
using WebShopRestService.Interfaces;
using WebShopRestService.Managers;
using WebShopRestService.Models;

[TestClass]
public class OrderItemsManagerTest
{
    private static MyDbContext _context;
    private static OrderItemsManager _manager;
    private static IOrderItemsRepository _orderItemRepository;
    private static IProductsRepository _productRepository;

    [ClassInitialize]
    public static void ClassInitialize(TestContext testContext)
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
        _orderItemRepository = new OrderItemsRepository(_context); // Assuming you have implemented this
        _productRepository = new ProductsRepository(_context); // Assuming you have implemented this
        _manager = new OrderItemsManager(_productRepository, _orderItemRepository);

        // Optional: Seed the test database with data necessary for testing
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        // Clean up any data from the test database to reset the state
        _context.Dispose();
    }

    [TestMethod]
    public async Task AddOrderItem_ShouldAddItemSuccessfully()
    {
        // Arrange
        var newOrderItem = new OrderItem
        {
            // Populate order item details
            ProductId = 11, Quantity = 10, Price = 20.0M
        };

        // Act
        await _manager.AddOrderItemAsync(newOrderItem);

        // Assert
        var addedItem = await _orderItemRepository.GetOrderItemByIdAsync(newOrderItem.OrderItemId);
        Assert.IsNotNull(addedItem);

        // Cleanup
        await _orderItemRepository.DeleteOrderItemAsync(addedItem.OrderItemId);
    }

    [TestMethod]
    public async Task GetOrderItem_ShouldReturnItem()
    {
        // Arrange - Ensure there is an order item in the database

        // Act
        var orderItem = await _manager.GetOrderItemByIdAsync(1); // Use a known ID

        // Assert
        Assert.IsNotNull(orderItem);
    }

    [TestMethod]
    public async Task UpdateOrderItem_ShouldModifyItem()
    {
        // Arrange
        var orderItemToUpdate = new OrderItem
        {
            // Populate order item details
            ProductId = 1, Quantity = 10, Price = 20.0M
        };
        await _orderItemRepository.AddOrderItemAsync(orderItemToUpdate);

        // Modify some details
        orderItemToUpdate.Quantity = 15;

        // Act
        await _manager.UpdateOrderItemAsync(orderItemToUpdate);

        // Assert
        var updatedItem = await _orderItemRepository.GetOrderItemByIdAsync(orderItemToUpdate.OrderItemId);
        Assert.IsNotNull(updatedItem);
        Assert.AreEqual(15, updatedItem.Quantity);

        // Cleanup
        await _orderItemRepository.DeleteOrderItemAsync(updatedItem.OrderItemId);
    }

    [TestMethod]
    public async Task DeleteOrderItem_ShouldRemoveItem()
    {
        // Arrange
        var orderItemToDelete = new OrderItem
        {
            // Populate order item details
            ProductId = 1, Quantity = 10, Price = 20.0M
        };
        await _orderItemRepository.AddOrderItemAsync(orderItemToDelete);

        // Act
        await _manager.DeleteOrderItemAsync(orderItemToDelete.OrderItemId);

        // Assert
        var deletedItem = await _orderItemRepository.GetOrderItemByIdAsync(orderItemToDelete.OrderItemId);
        Assert.IsNull(deletedItem);
    }
}
