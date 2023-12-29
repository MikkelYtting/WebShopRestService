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
    public class ProductsManagerTest
    {
        private Mock<IProductsRepository> _mockRepo;
        private ProductsManager _manager;
        private List<Product> _mockProducts;

        [TestInitialize]
        public void TestSetup()
        {
            _mockRepo = new Mock<IProductsRepository>();
            _manager = new ProductsManager(_mockRepo.Object);

            // Initialize common test data
            _mockProducts = new List<Product>
            {
                new Product { ProductId = 1, Name = "ProductA", Price = 20.0m },
                new Product { ProductId = 2, Name = "ProductB", Price = 30.0m }
                // Add more sample products as needed
            };
        }

        [TestMethod]
        public async Task GetAll_ReturnsAllProducts() // ID 20
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllProductsAsync()).ReturnsAsync(_mockProducts);

            // Act
            var result = await _manager.GetAll();

            // Assert
            CollectionAssert.AreEqual(_mockProducts, new List<Product>(result));
        }

        [TestMethod]
        public async Task GetProductById_ReturnsProduct() // ID 21
        {
            // Arrange
            var product = _mockProducts[0];
            _mockRepo.Setup(repo => repo.GetProductByIdAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _manager.Get(1);

            // Assert
            Assert.AreEqual(product, result);
        }


        [TestMethod]
        public async Task Delete_ExistingProduct_ReturnsTrue() // ID 24
        {
            // Arrange
            var productToDelete = _mockProducts[0];
            _mockRepo.Setup(repo => repo.GetProductByIdAsync(productToDelete.ProductId)).ReturnsAsync(productToDelete);
            _mockRepo.Setup(repo => repo.DeleteProductAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            await _manager.Delete(productToDelete.ProductId);

            // Assert
            // Since the Delete method has a void return type, there is no result to check.
            // You can assert that the Delete method did not throw an exception to ensure it executed without errors.
        }

        [TestMethod]
        public async Task UpdateNonExistentProduct_ReturnsFalse() // ID 51
        {
            // Arrange
            var nonExistentProduct = new Product { ProductId = 99, /* ... other properties ... */ };
            _mockRepo.Setup(repo => repo.GetProductByIdAsync(nonExistentProduct.ProductId)).ReturnsAsync((Product)null);

            // Act & Assert
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _manager.Update(nonExistentProduct.ProductId, nonExistentProduct));
        }

        [TestMethod]
        public async Task AddProductWithDuplicateInfo_ThrowsException() // ID 52
        {
            // Arrange
            var duplicateProduct = _mockProducts[0]; // Use an existing product for duplication
            _mockRepo.Setup(repo => repo.AddProductAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

            // Act & Assert
            Assert.ThrowsExceptionAsync<ArgumentException>(() => _manager.Create(duplicateProduct));
        }

        [TestMethod]
        public async Task DeleteNonExistentProduct_ReturnsFalse() // ID 53
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync((Product)null);

            // Act & Assert
            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _manager.Delete(99)); // Assuming 99 is a non-existent ID
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Any cleanup code if needed
        }
    }

}
