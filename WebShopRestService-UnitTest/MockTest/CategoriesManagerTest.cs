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
    public class CategoriesManagerTest
    {
        private Mock<ICategoriesRepository> _mockRepo;
        private CategoriesManager _manager;
        private List<Category> _mockCategories;

        [TestInitialize]
        public void TestSetup()
        {
            _mockRepo = new Mock<ICategoriesRepository>();
            _manager = new CategoriesManager(_mockRepo.Object);

            // Initialize common test data
            _mockCategories = new List<Category>
            {
                new Category { CategoryId = 1, Name = "CategoryA", Description = "DescriptionA" },
                new Category { CategoryId = 2, Name = "CategoryB", Description = "DescriptionB" }
            };
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Any cleanup code if needed
        }

        [TestMethod]
        public async Task GetAllCategoriesAsync_ReturnsAllCategories() //ID 6 
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllCategoriesAsync()).ReturnsAsync(_mockCategories);

            // Act
            var result = await _manager.GetAllCategoriesAsync();

            // Assert
            CollectionAssert.AreEqual(_mockCategories, new List<Category>(result));
        }

        [TestMethod]
        public async Task GetCategoryByIdAsync_ReturnsCategory()  //ID 6 
        {
            // Arrange
            var category = _mockCategories[0];
            _mockRepo.Setup(repo => repo.GetCategoryByIdAsync(1)).ReturnsAsync(category);

            // Act
            var result = await _manager.GetCategoryByIdAsync(1);

            // Assert
            Assert.AreEqual(category, result);
        }

        [TestMethod]
        public async Task AddCategoryAsync_AddsNewCategory() // ID 5
        {
            // Arrange
            var newCategory = new Category { CategoryId = 3, Name = "CategoryC", Description = "DescriptionC" };
            _mockRepo.Setup(repo => repo.AddCategoryAsync(It.IsAny<Category>())).Returns(Task.CompletedTask);

            // Act
            await _manager.AddCategoryAsync(newCategory);

            // Assert
            _mockRepo.Verify(repo => repo.AddCategoryAsync(newCategory), Times.Once);
        }

        [TestMethod]
        public async Task UpdateCategoryAsync_ExistingCategory_ReturnsTrue() // ID 7
        {
            // Arrange
            var categoryToUpdate = _mockCategories[0];
            _mockRepo.Setup(repo => repo.UpdateCategoryAsync(It.IsAny<Category>())).Returns(Task.CompletedTask);

            // Act
            await _manager.UpdateCategoryAsync(categoryToUpdate);

            // Assert
            _mockRepo.Verify(repo => repo.UpdateCategoryAsync(categoryToUpdate), Times.Once);
        }

        [TestMethod]
        public async Task DeleteCategoryAsync_ExistingCategory_ReturnsTrue() //ID 8
        {
            // Arrange
            var categoryToDelete = _mockCategories[0];
            _mockRepo.Setup(repo => repo.DeleteCategoryAsync(1)).Returns(Task.CompletedTask);

            // Act
            await _manager.DeleteCategoryAsync(1);

            // Assert
            _mockRepo.Verify(repo => repo.DeleteCategoryAsync(1), Times.Once);
        }

        [TestMethod]
        public async Task GetCategoryByIdAsync_NonExistentId_ReturnsNull() //ID 39
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCategoryByIdAsync(It.IsAny<int>())).ReturnsAsync((Category)null);

            // Act
            var result = await _manager.GetCategoryByIdAsync(99); // Assuming 99 is a non-existent ID

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task UpdateCategoryAsync_NonExistentCategory_ThrowsException() //ID 40
        {
            // Arrange
            var nonExistentCategory = new Category { CategoryId = 99, /* ... other properties ... */ };
            _mockRepo.Setup(repo => repo.UpdateCategoryAsync(nonExistentCategory)).ThrowsAsync(new InvalidOperationException());

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _manager.UpdateCategoryAsync(nonExistentCategory));
        }

        [TestMethod]
        public async Task DeleteCategoryAsync_NonExistentId_ThrowsException()  // ID 41
        {
            // Arrange
            _mockRepo.Setup(repo => repo.DeleteCategoryAsync(99)).ThrowsAsync(new InvalidOperationException());

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _manager.DeleteCategoryAsync(99));
        }
    }
}
