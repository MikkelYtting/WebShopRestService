using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebShopRestService.Interfaces;
using WebShopRestService.Managers;
using WebShopRestService.Models;

namespace WebShopRestService_UnitTest.MockTest
{
    [TestClass]
    public class OrderTableManagerTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task ValidateAndAddOrderAsync_ThrowsException_IfCustomerDoesNotExist()
        {
            // Arrange
            var mockCustomerRepo = new Mock<ICustomersRepository>();
            mockCustomerRepo.Setup(repo => repo.Exists(It.IsAny<int>())).ReturnsAsync(false);
            var mockAddressRepo = new Mock<IAddressesRepository>();
            var mockOrderTableRepo = new Mock<IOrderTablesRepository>();
            var mockProductRepo = new Mock<IProductsRepository>();

            var manager = new OrderTablesManager(
                mockCustomerRepo.Object,
                mockAddressRepo.Object,
                mockOrderTableRepo.Object,
                mockProductRepo.Object
            );

            var order = new OrderTable { /* ... */ };

            // Act
            await manager.ValidateAndAddOrderAsync(order);

            // The ExpectedException attribute indicates that an exception is expected
        }
    }
}