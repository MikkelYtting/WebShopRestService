using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebShopRestService.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

[TestClass]
public class UserCredentialTests
{
    private UserCredential CreateUserCredential(string username, string hashedPassword, int roleId)
    {
        return new UserCredential
        {
            Username = username,
            HashedPassword = hashedPassword,
            RoleId = roleId
        };
    }

    private bool TryValidateModel(UserCredential userCredential, out ICollection<ValidationResult> results)
    {
        var context = new ValidationContext(userCredential, serviceProvider: null, items: null);
        results = new List<ValidationResult>();
        return Validator.TryValidateObject(userCredential, context, results, true);
    }

    // Test cases for Username
    [TestMethod]
    [DataRow("example@example.com")]
    [DataRow("user@example.net")]
    //[DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 100 'a's
    public void UserCredential_WithValidUsername_ShouldPassValidation(string username)
    {
        var userCredential = CreateUserCredential(username, "hashedPassword123", 1);
        var result = TryValidateModel(userCredential, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow("notanemail")]
    //[DataRow(new string('a', 101) + "@example.com")] // 101 characters, over the limit
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 101 'a's
    public void UserCredential_WithInvalidUsername_ShouldFailValidation(string username)
    {
        var userCredential = CreateUserCredential(username, "hashedPassword123", 1);
        var result = TryValidateModel(userCredential, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

    // Test cases for HashedPassword
    [TestMethod]
    [DataRow("hashedPassword123")]
    [DataRow("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb")] // 255 'a's
    //[DataRow(new string('a', 255))] // Maximum valid length
    public void UserCredential_WithValidHashedPassword_ShouldPassValidation(string hashedPassword)
    {
        var userCredential = CreateUserCredential("example@example.com", hashedPassword, 1);
        var result = TryValidateModel(userCredential, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow("")]
    [DataRow("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb")] // 256 'b's
    //[DataRow(new string('a', 256))] // 256 characters, over the limit
    public void UserCredential_WithInvalidHashedPassword_ShouldFailValidation(string hashedPassword)
    {
        var userCredential = CreateUserCredential("example@example.com", hashedPassword, 1);
        var result = TryValidateModel(userCredential, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

    // Test cases for RoleId
    [TestMethod]
    [DataRow(1)] // Assuming 1 is a valid RoleId
    [DataRow(100)]
    [DataRow(int.MaxValue)]
    public void UserCredential_WithValidRoleId_ShouldPassValidation(int roleId)
    {
        var userCredential = CreateUserCredential("example@example.com", "hashedPassword123", roleId);
        var result = TryValidateModel(userCredential, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    // Assuming zero or negative numbers are invalid for RoleId
    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void UserCredential_WithInvalidRoleId_ShouldFailValidation(int roleId)
    {
        var userCredential = CreateUserCredential("example@example.com", "hashedPassword123", roleId);
        var result = TryValidateModel(userCredential, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }
}
