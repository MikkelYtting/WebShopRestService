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
    //[DataRow("a" + new string('a', 101))] // 101 characters, over the limit
    public void Payment_WithInvalidPaymentMethod_ShouldFailValidation(string paymentMethod)
    {
        var payment = CreatePayment(paymentMethod, DateTime.Now, 100.00m, 1);
        var result = TryValidateModel(payment, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

    // Test cases for Amount
    [TestMethod]
    [DataRow(0.01)] // Minimum valid
    [DataRow(1000.00)]
    //[DataRow(double.MaxValue)] // Maximum valid
    //[DataRow(79228162514264337593543950335m)] // Large decimal value within range
    [DataRow(1e+18)] // High but convertible to decimal
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

    // Similar test cases can be created for OrderId
    // PaymentDate is a required field but without specific range constraints, 
    
}

