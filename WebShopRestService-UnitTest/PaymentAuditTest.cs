using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebShopRestService.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

[TestClass]
public class PaymentAuditTests
{
    private PaymentAudit CreatePaymentAudit(int orderId, DateTime date, decimal amount, string actionType, DateTime actionDate)
    {
        return new PaymentAudit
        {
            OrderId = orderId,
            Date = date,
            Amount = amount,
            ActionType = actionType,
            ActionDate = actionDate
        };
    }

    private bool TryValidateModel(PaymentAudit paymentAudit, out ICollection<ValidationResult> results)
    {
        var context = new ValidationContext(paymentAudit, serviceProvider: null, items: null);
        results = new List<ValidationResult>();
        return Validator.TryValidateObject(paymentAudit, context, results, true);
    }

    // Test cases for OrderId
    [TestMethod]
    [DataRow(1)] // Minimum valid
    [DataRow(100)]
    [DataRow(int.MaxValue)] // Maximum valid integer
    public void PaymentAudit_WithValidOrderId_ShouldPassValidation(int orderId)
    {
        var paymentAudit = CreatePaymentAudit(orderId, DateTime.Now, 100.00m, "Payment", DateTime.Now);
        var result = TryValidateModel(paymentAudit, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    [DataRow(0)] // Invalid as per the model
    [DataRow(-1)] // Negative value
    public void PaymentAudit_WithInvalidOrderId_ShouldFailValidation(int orderId)
    {
        var paymentAudit = CreatePaymentAudit(orderId, DateTime.Now, 100.00m, "Payment", DateTime.Now);
        var result = TryValidateModel(paymentAudit, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

    // Test cases for Amount
    [TestMethod]
    [DataRow(0.01)] // Minimum valid
    [DataRow(1000000.00)] // Maximum valid
    public void PaymentAudit_WithValidAmount_ShouldPassValidation(double amount)
    {
        var paymentAudit = CreatePaymentAudit(1, DateTime.Now, (decimal)amount, "Payment", DateTime.Now);
        var result = TryValidateModel(paymentAudit, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    [DataRow(0.00)] // Invalid as per the model
    [DataRow(-1.00)] // Negative value
    public void PaymentAudit_WithInvalidAmount_ShouldFailValidation(double amount)
    {
        var paymentAudit = CreatePaymentAudit(1, DateTime.Now, (decimal)amount, "Payment", DateTime.Now);
        var result = TryValidateModel(paymentAudit, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }
    // Test cases for ActionType
    [TestMethod]
    [DataRow("Payment")]
    [DataRow("Refund")]
    [DataRow("Adjustment")]
    public void PaymentAudit_WithValidActionType_ShouldPassValidation(string actionType)
    {
        var paymentAudit = CreatePaymentAudit(1, DateTime.Now, 100.00m, actionType, DateTime.Now);
        var result = TryValidateModel(paymentAudit, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow("123Payment")]
    [DataRow("Payment!@#")]
    [DataRow(" ")]
    public void PaymentAudit_WithInvalidActionType_ShouldFailValidation(string actionType)
    {
        var paymentAudit = CreatePaymentAudit(1, DateTime.Now, 100.00m, actionType, DateTime.Now);
        var result = TryValidateModel(paymentAudit, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

    // Test cases for Date and ActionDate
    [TestMethod]
    [DataRow("2022-01-01")]
    [DataRow("2023-12-15")] // Assuming this date is not in the future relative to the test execution date
    public void PaymentAudit_WithValidDate_ShouldPassValidation(string dateString)
    {
        var date = DateTime.Parse(dateString);
        var paymentAudit = CreatePaymentAudit(1, date, 100.00m, "Payment", date);
        var result = TryValidateModel(paymentAudit, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    public void PaymentAudit_WithFutureDate_ShouldFailValidation()
    {
        var futureDate = DateTime.Now.AddDays(1); // Date in the future
        var paymentAudit = CreatePaymentAudit(1, futureDate, 100.00m, "Payment", futureDate);
        var result = TryValidateModel(paymentAudit, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

}
