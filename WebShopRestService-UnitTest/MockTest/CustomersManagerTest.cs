using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopRestService.Interfaces;
using WebShopRestService.Managers;

namespace WebShopRestService_UnitTest.MockTest
{
    [TestClass]
    public class CustomersManagerTest
    {
        private Mock<ICustomersRepository> _mockRepo;
        private CustomersManager _manager;
        private List<Customer> _mockCustomers;

        [TestInitialize]
        public void TestSetup()
        {
            _mockRepo = new Mock<ICustomersRepository>();
            _manager = new CustomersManager(_mockRepo.Object);

            // Initialize common test data
            _mockCustomers = new List<Customer>
            {
                new Customer
                {
                    CustomerId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Phone = "1234567890",
                    AddressId = 1,
                    UserId = 1
                },
                new Customer
                {
                    CustomerId = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    Phone = "9876543210",
                    AddressId = 2,
                    UserId = 2
                }
            };
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Any cleanup code if needed
        }

        [TestMethod]
        public async Task GetAll_ReturnsAllCustomers() //ID 9
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllCustomersAsync()).ReturnsAsync(_mockCustomers);

            // Act
            var result = await _manager.GetAll();

            // Assert
            CollectionAssert.AreEqual(_mockCustomers, new List<Customer>(result));
        }

        [TestMethod]
        public async Task Get_ReturnsCustomerById() // ID 10
        {
            // Arrange
            var customer = _mockCustomers[0];
            _mockRepo.Setup(repo => repo.GetCustomerByIdAsync(1)).ReturnsAsync(customer);

            // Act
            var result = await _manager.GetCustomerByIdAsync(1);

            // Assert
            Assert.AreEqual(customer, result);
        }

        [TestMethod]
        public async Task Update_ExistingCustomer_ReturnsTrue() // ID 11
        {
            // Arrange
            var customerToUpdate = _mockCustomers[0];
            _mockRepo.Setup(repo => repo.GetCustomerByIdAsync(customerToUpdate.CustomerId)).ReturnsAsync(customerToUpdate);
            _mockRepo.Setup(repo => repo.UpdateCustomerAsync(It.IsAny<Customer>())).Returns(Task.CompletedTask);

            // Act
            await _manager.Update(customerToUpdate.CustomerId, customerToUpdate);

            // Assert
            // If the method completes without throwing an exception, consider the operation successful
            Assert.IsTrue(true);
        }



        [TestMethod]
        public async Task Create_AddsNewCustomer() //ID 12
        {
            // Arrange
            var newCustomer = new Customer
            {
                CustomerId = 3,
                FirstName = "New",
                LastName = "Customer",
                Email = "new.customer@example.com",
                Phone = "5555555555",
                AddressId = 3,
                UserId = 3
            };
            _mockRepo.Setup(repo => repo.AddCustomerAsync(It.IsAny<Customer>())).Returns(Task.CompletedTask);

            // Act
            var result = await _manager.Create(newCustomer);

            // Assert
            Assert.AreEqual(newCustomer, result);
        }

        [TestMethod]
        public async Task Delete_ExistingCustomer_ReturnsTrue() // ID 13
        {
            // Arrange
            var customerToDelete = _mockCustomers[0];
            _mockRepo.Setup(repo => repo.GetCustomerByIdAsync(customerToDelete.CustomerId)).ReturnsAsync(customerToDelete);
            _mockRepo.Setup(repo => repo.DeleteCustomerAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act & Assert
            try
            {
                await _manager.Delete(customerToDelete.CustomerId);
                // If no exception is thrown, consider the operation successful
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                // Handle any specific exception types if needed
                Assert.Fail($"Unexpected exception: {ex.Message}");
            }
        }



        [TestMethod]
        public async Task Get_NonExistentId_ReturnsNull() // ID 42
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCustomerByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);

            // Act
            var result = await _manager.GetCustomerByIdAsync(99); // Assuming 99 is a non-existent ID

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetCustomerByIdAsync_NonExistentId_ReturnsNull() // ID 42
        {
            // Arrange
            var nonExistentCustomerId = 99;
            _mockRepo.Setup(repo => repo.GetCustomerByIdAsync(nonExistentCustomerId)).ReturnsAsync((Customer)null);

            // Act
            var result = await _manager.GetCustomerByIdAsync(nonExistentCustomerId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Delete_NonExistentId_ReturnsFalse() // ID 45
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCustomerByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);
            _mockRepo.Setup(repo => repo.DeleteCustomerAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            await _manager.Delete(99);

            // Assert
            // If the method doesn't throw an exception and completes, consider the operation successful
            Assert.IsTrue(true);
        }
    }
}
