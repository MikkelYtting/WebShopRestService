using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebShopRestService.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

[TestClass]
public class ProductTests
{
    private Product CreateProduct(string name, string description, string img, decimal price, int stockQuantity, int categoryId)
    {
        return new Product
        {
            Name = name,
            Description = description,
            Img = img,
            Price = price,
            StockQuantity = stockQuantity,
            CategoryId = categoryId
        };
    }

    private bool TryValidateModel(Product product, out ICollection<ValidationResult> results)
    {
        var context = new ValidationContext(product, serviceProvider: null, items: null);
        results = new List<ValidationResult>();
        return Validator.TryValidateObject(product, context, results, true);
    }

    // Test cases for Name
    [TestMethod]
    [DataRow("Laptop")]
    [DataRow("Smartphone")]
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 100 'a's
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 101 'a's

    public void Product_WithName_ShouldPassValidation(string name)
    {
        var product = CreateProduct(name, "Some Description", "img.jpg", 100.00m, 10, 1);
        var result = TryValidateModel(product, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 101 'a's
    public void Product_WithName_ShouldFailValidation(string name)
    {
        var product = CreateProduct(name, "Some Description", "img.jpg", 100.00m, 10, 1);
        var result = TryValidateModel(product, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

    // Test cases for Price
    [TestMethod]
    [DataRow(0.01)] // Minimum valid
    [DataRow(500000.00)] // middle range
    [DataRow(1000000.00)] // Maximum valid
    public void Product_WithValidPrice_ShouldPassValidation(double price)
    {
        var product = CreateProduct("Product", "Some Description", "img.jpg", (decimal)price, 10, 1);
        var result = TryValidateModel(product, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    [DataRow(0.00)] // Below minimum
    [DataRow(-1.00)] // Negative value
    [DataRow(1000000.01)] // Above maximum
    public void Product_WithInvalidPrice_ShouldFailValidation(double price)
    {
        var product = CreateProduct("Product", "Some Description", "img.jpg", (decimal)price, 10, 1);
        var result = TryValidateModel(product, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }
    // Test cases for StockQuantity
    [TestMethod]
    [DataRow(0)] // Minimum valid
    [DataRow(50)]
    [DataRow(int.MaxValue)] // Maximum valid integer
    public void Product_WithValidStockQuantity_ShouldPassValidation(int stockQuantity)
    {
        var product = CreateProduct("Product", "Some Description", "img.jpg", 100.00m, stockQuantity, 1);
        var result = TryValidateModel(product, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    [DataRow(-1)] // Negative value, invalid
    public void Product_WithInvalidStockQuantity_ShouldFailValidation(int stockQuantity)
    {
        var product = CreateProduct("Product", "Some Description", "img.jpg", 100.00m, stockQuantity, 1);
        var result = TryValidateModel(product, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }
    // Test cases for CategoryId
    [TestMethod]
    [DataRow(1)] // Minimum valid
    [DataRow(100)]
    [DataRow(int.MaxValue)] // Maximum valid integer
    public void Product_WithValidCategoryId_ShouldPassValidation(int categoryId)
    {
        var product = CreateProduct("Product", "Some Description", "img.jpg", 100.00m, 10, categoryId);
        var result = TryValidateModel(product, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    [DataRow(0)] // Zero, might be invalid
    [DataRow(-1)] // Negative value, invalid
    public void Product_WithInvalidCategoryId_ShouldFailValidation(int categoryId)
    {
        var product = CreateProduct("Product", "Some Description", "img.jpg", 100.00m, 10, categoryId);
        var result = TryValidateModel(product, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }
    // Test cases for Description
    [TestMethod]
    [DataRow("Short description")] // Valid short description
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
             "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
             "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
             "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
             "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 500 'a's
    public void Product_WithValidDescription_ShouldPassValidation(string description)
    {
        var product = CreateProduct("Product", description, "img.jpg", 100.00m, 10, 1);
        var result = TryValidateModel(product, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
             "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
             "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
             "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
             "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 501 'a's
    public void Product_WithInvalidDescription_ShouldFailValidation(string description)
    {
        var product = CreateProduct("Product", description, "img.jpg", 100.00m, 10, 1);
        var result = TryValidateModel(product, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }



}

