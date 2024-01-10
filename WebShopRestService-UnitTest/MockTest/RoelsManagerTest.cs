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
    public class RolesManagerTest
    {
        private RolesManager _manager;
        private Mock<IRolesRepository> _mockRepo;
        private List<Role> _mockRoles;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepo = new Mock<IRolesRepository>();
            _manager = new RolesManager(_mockRepo.Object);

            // Initialize mock roles
            _mockRoles = new List<Role>
        {
            new Role { RoleId = 1, Name = "Admin" },
            new Role { RoleId = 2, Name = "User" }
            // Add more mock roles as needed
        };
        }

        [TestMethod]
        public async Task GetAll_ReturnsListOfRoles() // ID 30
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllRolesAsync()).ReturnsAsync(_mockRoles);

            // Act
            var result = await _manager.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Role>));
        }

        [TestMethod]
        public async Task Get_ExistingRoleId_ReturnsRole() // 31
        {
            // Arrange
            var roleIdToRetrieve = _mockRoles[0].RoleId;
            _mockRepo.Setup(repo => repo.GetRoleByIdAsync(roleIdToRetrieve)).ReturnsAsync(_mockRoles[0]);

            // Act
            var result = await _manager.Get(roleIdToRetrieve);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(roleIdToRetrieve, result.RoleId);
        }

        [TestMethod]
        public async Task Get_NonExistentRoleId_ReturnsNull()  // ID 50
        {
            // Arrange
            var nonExistentRoleId = 999; // Assuming 999 doesn't exist
            _mockRepo.Setup(repo => repo.GetRoleByIdAsync(nonExistentRoleId)).ReturnsAsync((Role)null);

            // Act
            var result = await _manager.Get(nonExistentRoleId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Update_ExistingRole_ReturnsTrue() // 32
        {
            // Arrange
            var roleToUpdate = _mockRoles[0];
            _mockRepo.Setup(repo => repo.GetRoleByIdAsync(roleToUpdate.RoleId)).ReturnsAsync(roleToUpdate);
            _mockRepo.Setup(repo => repo.UpdateRoleAsync(It.IsAny<Role>()))
                     .Callback<Role>(updatedRole =>
                     {
                         // Simulate the behavior of UpdateRoleAsync
                         var existingRole = _mockRoles.SingleOrDefault(r => r.RoleId == updatedRole.RoleId);
                         if (existingRole != null)
                         {
                             existingRole.Name = updatedRole.Name;
                             existingRole.AccessLevel = updatedRole.AccessLevel;
                         }
                     });

            // Act
            await _manager.Update(roleToUpdate.RoleId, roleToUpdate);

            // Assert
            var updatedRole = _mockRoles.SingleOrDefault(r => r.RoleId == roleToUpdate.RoleId);
            Assert.IsNotNull(updatedRole);
            Assert.AreEqual(roleToUpdate.Name, updatedRole.Name);
            Assert.AreEqual(roleToUpdate.AccessLevel, updatedRole.AccessLevel);
        }

       


        [TestMethod]
        public async Task Create_NewRole_AddsRole() // 33
        {
            // Arrange
            var newRole = new Role { Name = "NewRole" };
            _mockRepo.Setup(repo => repo.AddRoleAsync(It.IsAny<Role>())).Returns(Task.CompletedTask);

            // Act
            await _manager.Create(newRole);

            // Assert
            _mockRepo.Verify(repo => repo.AddRoleAsync(newRole), Times.Once);
        }

        [TestMethod]
        public async Task Delete_ExistingRole_ReturnsTrue() // 34
        {
            // Arrange
            var roleToDelete = _mockRoles[0];
            _mockRepo.Setup(repo => repo.GetRoleByIdAsync(roleToDelete.RoleId)).ReturnsAsync(roleToDelete);
            _mockRepo.Setup(repo => repo.DeleteRoleAsync(It.IsAny<int>()))
                     .Callback<int>(roleId =>
                     {
                         // Simulate the behavior of DeleteRoleAsync
                         var role = _mockRoles.SingleOrDefault(r => r.RoleId == roleId);
                         if (role != null)
                         {
                             _mockRoles.Remove(role);
                         }
                     });

            // Act
            await _manager.Delete(roleToDelete.RoleId);

            // Assert
            var roleExists = _mockRoles.Any(r => r.RoleId == roleToDelete.RoleId);
            Assert.IsFalse(roleExists);
        }

        [TestMethod]
        public async Task Delete_NonExistentRole_DoesNotThrowException() // ID 58
        {
            // Arrange
            var nonExistentRoleId = 999; // Assuming 999 doesn't exist
            _mockRepo.Setup(repo => repo.GetRoleByIdAsync(nonExistentRoleId)).ReturnsAsync((Role)null);

            // Act & Assert
            await _manager.Delete(nonExistentRoleId);

            // No exception expected, consider the operation successful
            Assert.IsTrue(true);
        }
    }

}
