using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopRestService.Interfaces;
using WebShopRestService.Managers;
using WebShopRestService.Models;

namespace WebShopRestService_UnitTest.MockTest
{
        [TestClass]
        public class OrderitemsManagerTest
        {
            private Mock<IProductsRepository> _mockProductRepo;
            private Mock<IOrderItemsRepository> _mockOrderItemRepo;
            private OrderItemsManager _manager;
            private List<OrderItem> _mockOrderItems;
            private List<Product> _mockProducts;

            [TestInitialize]
            public void TestSetup()
            {
                _mockProductRepo = new Mock<IProductsRepository>();
                _mockOrderItemRepo = new Mock<IOrderItemsRepository>();
                _manager = new OrderItemsManager(_mockProductRepo.Object, _mockOrderItemRepo.Object);

                // Initialize common test data
                _mockProducts = new List<Product>{
                        new Product { ProductId = 1, Name = "ProductA", Price = 10.0m },
                        new Product { ProductId = 2, Name = "ProductB", Price = 20.0m }
                    };

                _mockOrderItems = new List<OrderItem>
                {
                    new OrderItem { OrderItemId = 1, ProductId = 1, Quantity = 2, Price = 20.0m },
                    new OrderItem { OrderItemId = 2, ProductId = 2, Quantity = 1, Price = 20.0m }
                };


            }

            [TestCleanup]
            public void TestCleanup()
            {
                // Any cleanup code if needed
            }

            [TestMethod]
            public async Task ValidateAndAddOrderItem_ValidOrderItem_AddsOrderItem() // ID 15
            {
                // Arrange
                var validOrderItem = new OrderItem { ProductId = 1, Quantity = 2, Price = 20.0m };

                // Set up the mock product repository to return the correct product with the matching price
                _mockProductRepo.Setup(repo => repo.GetProductByIdAsync(validOrderItem.ProductId)).ReturnsAsync(new Product { Price = validOrderItem.Price });

                _mockOrderItemRepo.Setup(repo => repo.AddOrderItemAsync(It.IsAny<OrderItem>())).Returns(Task.CompletedTask);

                // Act & Assert
                await _manager.ValidateAndAddOrderItem(validOrderItem);

                // Verify that AddOrderItemAsync was called with the correct OrderItem
                _mockOrderItemRepo.Verify(repo => repo.AddOrderItemAsync(validOrderItem), Times.Once);
            }

            [TestMethod]
            public async Task ValidateAndAddOrderItem_InvalidProductId_ThrowsException() //ID 15
            {
                // Arrange
                var invalidOrderItem = new OrderItem { ProductId = 99, Quantity = 2, Price = 20.0m };
                _mockProductRepo.Setup(repo => repo.GetProductByIdAsync(invalidOrderItem.ProductId)).ReturnsAsync((Product)null);

                // Act & Assert
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _manager.ValidateAndAddOrderItem(invalidOrderItem));
            }

            [TestMethod]
            public async Task ValidateAndAddOrderItem_PriceMismatch_ThrowsException() //ID 15
            {
                // Arrange
                var orderItemWithMismatchedPrice = new OrderItem { ProductId = 1, Quantity = 2, Price = 30.0m };
                _mockProductRepo.Setup(repo => repo.GetProductByIdAsync(orderItemWithMismatchedPrice.ProductId)).ReturnsAsync(_mockProducts[0]);

                // Act & Assert
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _manager.ValidateAndAddOrderItem(orderItemWithMismatchedPrice));
            }

            [TestMethod]
            public async Task AddOrderItemAsync_AddsNewOrderItem() // ID 14
            {
                // Arrange
                var newOrderItem = new OrderItem { OrderItemId = 3, ProductId = 2, Quantity = 3, Price = 60.0m };
                _mockOrderItemRepo.Setup(repo => repo.AddOrderItemAsync(It.IsAny<OrderItem>())).Returns(Task.CompletedTask);

                // Act
                await _manager.AddOrderItemAsync(newOrderItem);

                // Assert
                _mockOrderItemRepo.Verify(repo => repo.AddOrderItemAsync(newOrderItem), Times.Once);
            }

            [TestMethod]
            public async Task GetOrderItemByIdAsync_ReturnsOrderItem() // ID 16
            {
                // Arrange
                var orderItemToGet = _mockOrderItems[0];
                _mockOrderItemRepo.Setup(repo => repo.GetOrderItemByIdAsync(orderItemToGet.OrderItemId)).ReturnsAsync(orderItemToGet);

                // Act
                var result = await _manager.GetOrderItemByIdAsync(orderItemToGet.OrderItemId);

                // Assert
                Assert.AreEqual(orderItemToGet, result);
            }


            [TestMethod]
            public async Task UpdateOrderItemAsync_ExistingOrderItem_CompletesSuccessfully() // ID 17
            {
                // Arrange
                var orderItemToUpdate = _mockOrderItems[0];
                _mockOrderItemRepo.Setup(repo => repo.UpdateOrderItemAsync(It.IsAny<OrderItem>())).Returns(Task.CompletedTask);

                // Act & Assert
                await _manager.UpdateOrderItemAsync(orderItemToUpdate);
                // If it completes without exceptions, the test is considered successful
            }


        [TestMethod]
            public async Task DeleteOrderItemAsync_ExistingOrderItem_CompletesSuccessfully() // ID 18
            {
                // Arrange
                var orderItemToDelete = _mockOrderItems[0];
                _mockOrderItemRepo.Setup(repo => repo.DeleteOrderItemAsync(orderItemToDelete.OrderItemId)).Returns(Task.CompletedTask);

                // Act & Assert
                await _manager.DeleteOrderItemAsync(orderItemToDelete.OrderItemId);
                // If it completes without exceptions, the test is considered successful
            }

            [TestMethod]
            public async Task GetAllOrderItemsAsync_ReturnsAllOrderItems() // ID 19
            {
                // Arrange
                _mockOrderItemRepo.Setup(repo => repo.GetAllOrderItemsAsync()).ReturnsAsync(_mockOrderItems);

                // Act
                var result = await _manager.GetAllOrderItemsAsync();

                // Assert
                CollectionAssert.AreEqual(_mockOrderItems, new List<OrderItem>(result));
            }

          
    }
}
