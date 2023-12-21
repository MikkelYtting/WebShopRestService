using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopRestService.Interfaces;
using WebShopRestService.Managers;
using WebShopRestService.Models;

namespace WebShopRestService_UnitTest.MockTest
{
    [TestClass]
    public class AddressesManagerTest
    {
        private Mock<IAddressesRepository> _mockRepo;
        private AddressesManager _manager;
        private List<Address> _mockAddresses;

        [TestInitialize]
        public void TestSetup()
        {
            _mockRepo = new Mock<IAddressesRepository>();
            _manager = new AddressesManager(_mockRepo.Object);

            // Initialize common test data
            _mockAddresses = new List<Address>
            {
                new Address { AddressId = 1, Street = "123 Main St", City = "CityA", PostalCode = "12345", Country = "CountryA" },
                new Address { AddressId = 2, Street = "456 Second St", City = "CityB", PostalCode = "67890", Country = "CountryB" }
            };
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Any cleanup code if needed
        }

        [TestMethod]
        public async Task GetAddressesAsync_ReturnsAllAddresses()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllAddressesAsync()).ReturnsAsync(_mockAddresses);

            // Act
            var result = await _manager.GetAddressesAsync();

            // Assert
            CollectionAssert.AreEqual(_mockAddresses, new List<Address>(result));
        }

        [TestMethod]
        public async Task GetAddressByIdAsync_ReturnsAddress()
        {
            // Arrange
            var address = _mockAddresses[0];
            _mockRepo.Setup(repo => repo.GetAddressByIdAsync(1)).ReturnsAsync(address);

            // Act
            var result = await _manager.GetAddressByIdAsync(1);

            // Assert
            Assert.AreEqual(address, result);
        }

        [TestMethod]
        public async Task UpdateAddressAsync_ExistingAddress_ReturnsTrue()
        {
            // Arrange
            var addressToUpdate = _mockAddresses[0];
            _mockRepo.Setup(repo => repo.GetAddressByIdAsync(addressToUpdate.AddressId)).ReturnsAsync(addressToUpdate);
            _mockRepo.Setup(repo => repo.UpdateAddressAsync(It.IsAny<Address>())).Returns(Task.CompletedTask);

            // Act
            var result = await _manager.UpdateAddressAsync(addressToUpdate);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CreateAddressAsync_AddsNewAddress()
        {
            // Arrange
            var newAddress = new Address { AddressId = 3, Street = "789 Third St", City = "CityC", PostalCode = "12378", Country = "CountryC" };
            _mockRepo.Setup(repo => repo.AddAddressAsync(It.IsAny<Address>())).Returns(Task.CompletedTask);

            // Act
            var result = await _manager.CreateAddressAsync(newAddress);

            // Assert
            Assert.AreEqual(newAddress, result);
        }

        [TestMethod]
        public async Task DeleteAddressAsync_ExistingAddress_ReturnsTrue()
        {
            // Arrange
            var addressToDelete = _mockAddresses[0];
            _mockRepo.Setup(repo => repo.GetAddressByIdAsync(addressToDelete.AddressId)).ReturnsAsync(addressToDelete);
            _mockRepo.Setup(repo => repo.DeleteAddressAsync(It.IsAny<Address>())).Returns(Task.CompletedTask);

            // Act
            var result = await _manager.DeleteAddressAsync(addressToDelete.AddressId);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
