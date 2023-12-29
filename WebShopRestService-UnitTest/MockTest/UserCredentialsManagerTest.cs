using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopRestService.Configurations;
using WebShopRestService.Data;
using WebShopRestService.Models;

namespace WebShopRestService_UnitTest.MockTest
{
    [TestClass]
    public class UserCredentialsManagerTests
    {
        //private Mock<IOptions<JwtConfig>> _jwtConfigMock;
        //private Mock<MyDbContext> _dbContextMock;

        //[TestInitialize]
        //public void Setup()
        //{
        //    _jwtConfigMock = new Mock<IOptions<JwtConfig>>();
        //    _dbContextMock = new Mock<MyDbContext>();
        //}

        //[TestMethod]
        //public void HashPassword_ValidPassword_ReturnsHashedPassword()
        //{
        //    // Arrange
        //    var userManager = new UserCredentialsManager(_jwtConfigMock.Object, _dbContextMock.Object);
        //    var password = "TestPassword";

        //    // Act
        //    var hashedPassword = userManager.HashPassword(password);

        //    // Assert
        //    Assert.IsNotNull(hashedPassword);
        //    Assert.AreNotEqual(password, hashedPassword);
        //}

        //[TestMethod]
        //public void VerifyPassword_ValidPasswordAndHash_ReturnsTrue()
        //{
        //    // Arrange
        //    var userManager = new UserCredentialsManager(_jwtConfigMock.Object, _dbContextMock.Object);
        //    var password = "TestPassword";
        //    var hashedPassword = userManager.HashPassword(password);

        //    // Act
        //    var result = userManager.VerifyPassword(password, hashedPassword);

        //    // Assert
        //    Assert.IsTrue(result);
        //}

        //[TestMethod]
        //public void GenerateJwtToken_ValidUser_ReturnsToken()
        //{
        //    // Arrange
        //    var jwtConfig = new JwtConfig { Secret = "TestSecret" };
        //    _jwtConfigMock.Setup(j => j.Value).Returns(jwtConfig);

        //    var userManager = new UserCredentialsManager(_jwtConfigMock.Object, _dbContextMock.Object);
        //    var user = new UserCredential { UserId = 1, Username = "TestUser" };

        //    // Act
        //    var token = userManager.GenerateJwtToken(user);

        //    // Assert
        //    Assert.IsNotNull(token);
        //}

       
    }
}
