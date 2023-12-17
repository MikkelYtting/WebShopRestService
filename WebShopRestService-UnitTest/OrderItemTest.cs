using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebShopRestService.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

[TestClass]
public class OrderItemTests
{
    private OrderItem CreateOrderItem(int quantity, decimal price, int orderId, int productId)
    {
        return new OrderItem
        {
            Quantity = quantity,
            Price = price,
            OrderId = orderId, // Assuming a valid OrderId
            ProductId = productId // Assuming a valid ProductId
        };
    }

    private bool TryValidateModel(OrderItem orderItem, out ICollection<ValidationResult> results)
    {
        var context = new ValidationContext(orderItem, serviceProvider: null, items: null);
        results = new List<ValidationResult>();
        return Validator.TryValidateObject(orderItem, context, results, true);
    }

    // positive test cases for quantity
    [TestMethod]
    [DataRow(1)] // Minimum valid
    [DataRow(10)]
    [DataRow(int.MaxValue)] // Maximum valid
    public void OrderItem_WithValidQuantity_ShouldPassValidation(int quantity)
    {
        var orderItem = CreateOrderItem(quantity, 50.00m, 1, 1);
        var result = TryValidateModel(orderItem, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    // negative test cases for quantity
    [TestMethod]
    [DataRow(0)] // Below minimum
    [DataRow(-1)] // Negative value
    public void OrderItem_WithInvalidQuantity_ShouldFailValidation(int quantity)
    {
        var orderItem = CreateOrderItem(quantity, 50.00m, 1, 1);
        var result = TryValidateModel(orderItem, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

    // positive test cases for price
    [TestMethod]
    [DataRow(0.01)] // Using double, will convert to decimal in method
    [DataRow(1000.00)]
    [DataRow(1000000.00)] // Maximum valid
    public void OrderItem_WithValidPrice_ShouldPassValidation(double price)
    {
        var orderItem = CreateOrderItem(1, (decimal)price, 1, 1);
        var result = TryValidateModel(orderItem, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    // negative test cases for price
    [TestMethod]
    [DataRow(0.00)] // Using double, will convert to decimal in method
    [DataRow(-1.00)] // Negative value
    [DataRow(1000000.01)] // Above maximum
    public void OrderItem_WithInvalidPrice_ShouldFailValidation(double price)
    {
        var orderItem = CreateOrderItem(1, (decimal)price, 1, 1);
        var result = TryValidateModel(orderItem, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }


    [TestMethod]
    [DataRow(1)] // Minimum valid (assuming IDs start from 1)
    [DataRow(100)]
    [DataRow(int.MaxValue)] // Maximum valid integer
    public void OrderItem_WithValidOrderId_ShouldPassValidation(int orderId)
    {
        var orderItem = CreateOrderItem(1, 50.00m, orderId, 1);
        var result = TryValidateModel(orderItem, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    // negative test cases for OrderId
    [TestMethod]
    [DataRow(0)] // Zero, which might be invalid in your system
    [DataRow(-1)] // Negative value
    public void OrderItem_WithInvalidOrderId_ShouldFailValidation(int orderId)
    {
        var orderItem = CreateOrderItem(1, 50.00m, orderId, 1);
        var result = TryValidateModel(orderItem, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

    // positive test cases for ProductId
    [TestMethod]
    [DataRow(1)] // Minimum valid (assuming IDs start from 1)
    [DataRow(100)]
    [DataRow(int.MaxValue)] // Maximum valid integer
    public void OrderItem_WithValidProductId_ShouldPassValidation(int productId)
    {
        var orderItem = CreateOrderItem(1, 50.00m, 1, productId);
        var result = TryValidateModel(orderItem, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    // negative test cases for ProductId
    [TestMethod]
    [DataRow(0)] // Zero, which might be invalid in your system
    [DataRow(-1)] // Negative value
    public void OrderItem_WithInvalidProductId_ShouldFailValidation(int productId)
    {
        var orderItem = CreateOrderItem(1, 50.00m, 1, productId);
        var result = TryValidateModel(orderItem, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }




    // Similar test cases can be created for OrderId and ProductId
    // Ensure to test for both positive and negative scenarios
    // Note: Since OrderId and ProductId are foreign keys, you might need to set up your testing environment 
    // to handle database-related validations if any.
}
