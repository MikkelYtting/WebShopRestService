using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebShopRestService.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

[TestClass]
public class PaymentTests
{
    private Payment CreatePayment(string paymentMethod, DateTime paymentDate, decimal amount, int orderId)
    {
        return new Payment
        {
            PaymentMethod = paymentMethod,
            PaymentDate = paymentDate,
            Amount = amount,
            OrderId = orderId
        };
    }

    private bool TryValidateModel(Payment payment, out ICollection<ValidationResult> results)
    {
        var context = new ValidationContext(payment, serviceProvider: null, items: null);
        results = new List<ValidationResult>();
        return Validator.TryValidateObject(payment, context, results, true);
    }

    // Test cases for PaymentMethod
    [TestMethod]
    [DataRow("Credit Card")]
    [DataRow("PayPal")]
    [DataRow("Cash")]
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 100 'a's
    public void Payment_WithValidPaymentMethod_ShouldPassValidation(string paymentMethod)
    {
        var payment = CreatePayment(paymentMethod, DateTime.Now, 100.00m, 1);
        var result = TryValidateModel(payment, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 101 'a's
    public void Payment_WithInvalidPaymentMethod_ShouldFailValidation(string paymentMethod)
    {
        var payment = CreatePayment(paymentMethod, DateTime.Now, 100.00m, 1);
        var result = TryValidateModel(payment, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

    // Test cases for PaymentDate
    [TestMethod]
    [DataRow("2022-01-01")]
    [DataRow("2023-12-12")] // Assuming this date is not in the future relative to the test execution date
    public void Payment_WithValidPaymentDate_ShouldPassValidation(string dateString)
    {
        var paymentDate = DateTime.Parse(dateString);
        var payment = CreatePayment("Credit Card", paymentDate, 100.00m, 1);
        var result = TryValidateModel(payment, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    public void Payment_WithFuturePaymentDate_ShouldFailOrPassValidation()
    {
        var futureDate = DateTime.Now.AddDays(1); // Date in the future
        var payment = CreatePayment("Credit Card", futureDate, 100.00m, 1);
        var result = TryValidateModel(payment, out var validationResults);
        // Assert based on your business rule: Assert.IsTrue(result) or Assert.IsFalse(result)
    }

    // Test cases for Amount
    [TestMethod]
    [DataRow(0.01)] // Minimum valid
    [DataRow(1000.00)] // Middle range
    [DataRow(1e+18)] // High but valid
    public void Payment_WithValidAmount_ShouldPassValidation(double amount)
    {
        var payment = CreatePayment("Credit Card", DateTime.Now, (decimal)amount, 1);
        var result = TryValidateModel(payment, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    [DataRow(0.00)] // Below minimum
    [DataRow(-1.00)] // Negative value
    public void Payment_WithInvalidAmount_ShouldFailValidation(double amount)
    {
        var payment = CreatePayment("Credit Card", DateTime.Now, (decimal)amount, 1);
        var result = TryValidateModel(payment, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

    // Test cases for OrderId
    [TestMethod]
    [DataRow(1)] // Minimum valid
    [DataRow(100)] // Middle range
    [DataRow(int.MaxValue)] // Maximum valid
    public void Payment_WithValidOrderId_ShouldPassValidation(int orderId)
    {
        var payment = CreatePayment("Credit Card", DateTime.Now, 100.00m, orderId);
        var result = TryValidateModel(payment, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    [DataRow(0)] // Zero, invalid
    [DataRow(-1)] // Negative value
    public void Payment_WithInvalidOrderId_ShouldFailValidation(int orderId)
    {
        var payment = CreatePayment("Credit Card", DateTime.Now, 100.00m, orderId);
        var result = TryValidateModel(payment, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }
}
