using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WebShopRestService.Data;
using WebShopRestService.Interfaces;
using WebShopRestService.Managers;
using WebShopRestService.Models;
using WebShopRestService.Repositories;

[TestClass]
public class OrderItemsManagerTest
{
    private MyDbContext _context;
    private OrderItemsManager _manager;
    private IOrderItemsRepository _orderItemRepository;
    private IProductsRepository _productRepository;
    private IDbContextTransaction _transaction;

    [TestInitialize]
    public async Task InitializeAsync()
    {
        var connectionString = Environment.GetEnvironmentVariable("TEST_CONNECTION_STRING")
                        ?? "Server=localhost;Database=Webshop;Uid=root;Pwd=1234;";

        var options = new DbContextOptionsBuilder<MyDbContext>()
                   .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                   .Options;

        _context = new MyDbContext(options);
        _orderItemRepository = new OrderItemsRepository(_context);
        _productRepository = new ProductsRepository(_context);
        _manager = new OrderItemsManager(_productRepository, _orderItemRepository);
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    [TestMethod]
    public async Task AddOrderItem_ShouldAddItemSuccessfully()
    {
        var existingOrderId = 1; // Assuming this exists in your OrderTables
        var existingProductId = 1; // Assuming this exists in your Products

        var newOrderItem = new OrderItem
        {
            OrderId = existingOrderId,
            ProductId = existingProductId,
            Quantity = 10,
            Price = 20.0M
        };

        await _manager.AddOrderItemAsync(newOrderItem);
        await _context.SaveChangesAsync();

        var addedItem = await _orderItemRepository.GetOrderItemByIdAsync(newOrderItem.OrderItemId);
        Assert.IsNotNull(addedItem);
    }

    [TestMethod]
    public async Task GetOrderItem_ShouldReturnItem()
    {
        var orderItem = await _manager.GetOrderItemByIdAsync(1); // Use a known ID
        Assert.IsNotNull(orderItem);
    }
    /*
    [TestMethod]
    public async Task UpdateOrderItem_ShouldModifyItem()
    {
        var orderItemToUpdate = new OrderItem { ProductId = 2, Quantity = 10, Price = 20.0M };
        await _orderItemRepository.AddOrderItemAsync(orderItemToUpdate);

        orderItemToUpdate.Quantity = 15;
        await _manager.UpdateOrderItemAsync(orderItemToUpdate);

        var updatedItem = await _orderItemRepository.GetOrderItemByIdAsync(orderItemToUpdate.OrderItemId);
        Assert.IsNotNull(updatedItem);
        Assert.AreEqual(15, updatedItem.Quantity);
    }
    */
    /*
    [TestMethod]
    public async Task DeleteOrderItem_ShouldRemoveItem()
    {
        // Arrange - Create and add a new Order and OrderItem
        var newOrder = new OrderTable
        {
            // Initialize necessary properties, including a valid DateTime
            OrderDate = DateTime.UtcNow // or any valid date within the SQL Server range
            // Other necessary initializations...
        };
        _context.OrderTables.Add(newOrder);
        await _context.SaveChangesAsync();

        var orderItemToDelete = new OrderItem
        {
            OrderId = newOrder.OrderId,
            ProductId = 1, // Ensure this ProductId exists
            Quantity = 10,
            Price = 20.0M
        };
        _context.OrderItems.Add(orderItemToDelete);
        await _context.SaveChangesAsync();

        // Act
        await _manager.DeleteOrderItemAsync(orderItemToDelete.OrderItemId);

        // Assert
        var deletedItem = await _context.OrderItems.FindAsync(orderItemToDelete.OrderItemId);
        Assert.IsNull(deletedItem);
    }
    */

    [TestCleanup]
    public async Task CleanupAsync()
    {
        await _transaction.RollbackAsync();
        _transaction.Dispose();
        _context.Dispose();
    }
}
