using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebShopRestService.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

[TestClass]
public class OrderTableTests
{
    private OrderTable CreateOrderTable(DateTime orderDate, decimal totalAmount, int customerId, int deliveryAddressId)
    {
        return new OrderTable
        {
            OrderDate = orderDate,
            TotalAmount = totalAmount,
            CustomerId = customerId,
            DeliveryAddressId = deliveryAddressId
        };
    }

    private bool TryValidateModel(OrderTable orderTable, out ICollection<ValidationResult> results)
    {
        var context = new ValidationContext(orderTable, serviceProvider: null, items: null);
        results = new List<ValidationResult>();
        return Validator.TryValidateObject(orderTable, context, results, true);
    }

    // Test cases for TotalAmount
    //positiv
    [TestMethod]
    [DataRow(0.01)] // Minimum valid
    [DataRow(5000000.00)] // Middle range
    [DataRow(10000000.00)] // Maximum valid
    public void OrderTable_WithValidTotalAmount_ShouldPassValidation(double totalAmount)
    {
        var orderTable = CreateOrderTable(DateTime.Now, (decimal)totalAmount, 1, 1);
        var result = TryValidateModel(orderTable, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }
    
    // Negativ
    [TestMethod]
    [DataRow(0.00)] // Below minimum
    [DataRow(-1.00)] // Negative value
    [DataRow(10000000.01)] // Above maximum
    public void OrderTable_WithInvalidTotalAmount_ShouldFailValidation(double totalAmount)
    {
        var orderTable = CreateOrderTable(DateTime.Now, (decimal)totalAmount, 1, 1);
        var result = TryValidateModel(orderTable, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

    // Test cases for CustomerId
    [TestMethod]
    [DataRow(1)] // Minimum valid
    [DataRow(100)] // Middle range
    [DataRow(int.MaxValue)] // Maximum valid integer
    public void OrderTable_WithValidCustomerId_ShouldPassValidation(int customerId)
    {
        var orderTable = CreateOrderTable(DateTime.Now, 100.00m, customerId, 1);
        var result = TryValidateModel(orderTable, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    [DataRow(0)] // Zero, might be invalid
    [DataRow(-1)] // Negative value
    public void OrderTable_WithInvalidCustomerId_ShouldFailValidation(int customerId)
    {
        var orderTable = CreateOrderTable(DateTime.Now, 100.00m, customerId, 1);
        var result = TryValidateModel(orderTable, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

    // Test cases for DeliveryAddressId
    [TestMethod]
    [DataRow(1)] // Minimum valid
    [DataRow(100)] // Middle range
    [DataRow(int.MaxValue)] // Maximum valid integer
    public void OrderTable_WithValidDeliveryAddressId_ShouldPassValidation(int deliveryAddressId)
    {
        var orderTable = CreateOrderTable(DateTime.Now, 100.00m, 1, deliveryAddressId);
        var result = TryValidateModel(orderTable, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    [DataRow(0)] // Zero, might be invalid
    [DataRow(-1)] // Negative value
    public void OrderTable_WithInvalidDeliveryAddressId_ShouldFailValidation(int deliveryAddressId)
    {
        var orderTable = CreateOrderTable(DateTime.Now, 100.00m, 1, deliveryAddressId);
        var result = TryValidateModel(orderTable, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }
    [TestMethod]
    public void OrderTable_WithValidOrderDate_ShouldPassValidation()
    {
        var orderTable = CreateOrderTable(DateTime.Now, 100.00m, 1, 1);
        var result = TryValidateModel(orderTable, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    public void OrderTable_WithFutureOrderDate_ShouldFailOrPassValidation()
    {
        var orderTable = CreateOrderTable(DateTime.Now.AddDays(1), 100.00m, 1, 1); // Future date
        var result = TryValidateModel(orderTable, out var validationResults);
        
    }

    [TestMethod]
    public void OrderTable_WithPastOrderDate_ShouldPassValidation()
    {
        var orderTable = CreateOrderTable(DateTime.Now.AddDays(-1), 100.00m, 1, 1); // Past date
        var result = TryValidateModel(orderTable, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }
}
