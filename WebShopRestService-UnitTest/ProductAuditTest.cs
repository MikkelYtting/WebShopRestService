using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebShopRestService.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

[TestClass]
public class ProductAuditTests
{
    private ProductAudit CreateProductAudit(decimal oldPrice, decimal newPrice, DateTime changeDate, int productId)
    {
        return new ProductAudit
        {
            OldPrice = oldPrice,
            NewPrice = newPrice,
            ChangeDate = changeDate,
            ProductId = productId
        };
    }

    private bool TryValidateModel(ProductAudit productAudit, out ICollection<ValidationResult> results)
    {
        var context = new ValidationContext(productAudit, serviceProvider: null, items: null);
        results = new List<ValidationResult>();
        return Validator.TryValidateObject(productAudit, context, results, true);
    }

    // Test cases for OldPrice and NewPrice
    [TestMethod]
    [DataRow(0.00)] // Minimum valid
    [DataRow(500000.00)] // Mid-range
    [DataRow(1000000.00)] // Maximum valid
    public void ProductAudit_WithValidPrices_ShouldPassValidation(double price)
    {
        var productAudit = CreateProductAudit((decimal)price, (decimal)price, DateTime.Now, 1);
        var result = TryValidateModel(productAudit, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    [DataRow(-1.00)] // Negative value, invalid
    [DataRow(1000000.01)] // Above maximum, invalid
    public void ProductAudit_WithInvalidPrices_ShouldFailValidation(double price)
    {
        var productAudit = CreateProductAudit((decimal)price, (decimal)price, DateTime.Now, 1);
        var result = TryValidateModel(productAudit, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

    // Test cases for ChangeDate
    [TestMethod]
    public void ProductAudit_WithValidChangeDate_ShouldPassValidation()
    {
        var productAudit = CreateProductAudit(100.00m, 200.00m, DateTime.Now, 1);
        var result = TryValidateModel(productAudit, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    public void ProductAudit_WithFutureChangeDate_ShouldFailValidation()
    {
        var futureDate = DateTime.Now.AddDays(1);
        var productAudit = CreateProductAudit(100.00m, 200.00m, futureDate, 1);
        var result = TryValidateModel(productAudit, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

    // Test cases for ProductId
    [TestMethod]
    [DataRow(1)] // Minimum valid
    [DataRow(100)] // Mid-range
    [DataRow(int.MaxValue)] // Maximum valid
    public void ProductAudit_WithValidProductId_ShouldPassValidation(int productId)
    {
        var productAudit = CreateProductAudit(100.00m, 200.00m, DateTime.Now, productId);
        var result = TryValidateModel(productAudit, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    [DataRow(0)] // Zero, invalid
    [DataRow(-1)] // Negative value, invalid
    public void ProductAudit_WithInvalidProductId_ShouldFailValidation(int productId)
    {
        var productAudit = CreateProductAudit(100.00m, 200.00m, DateTime.Now, productId);
        var result = TryValidateModel(productAudit, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }
}
