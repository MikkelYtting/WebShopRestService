using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebShopRestService.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

[TestClass]
public class CustomerTests
{
    private Customer CreateCustomer(string firstName, string lastName, string email, string phone)
    {
        return new Customer
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone,
            AddressId = 1, // Assuming a valid AddressId
            UserId = 1 // Assuming a valid UserId
        };
    }

    private bool TryValidateModel(Customer customer, out ICollection<ValidationResult> results)
    {
        var context = new ValidationContext(customer, serviceProvider: null, items: null);
        results = new List<ValidationResult>();
        return Validator.TryValidateObject(customer, context, results, true);
    }

    // positive test cases for first name
    [TestMethod]
    [DataRow("Alice")]
    [DataRow("Bob")]
    [DataRow("A")] // Minimum valid length
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 50 'a's, maximum valid length
    [DataRow("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb")] // 49 'b's Just Below Maximum
    [DataRow("bbbbbbbbbbbbbbbbbbbbbbb")] // 25 'b's middle of range
    public void Customer_WithValidFirstName_ShouldPassValidation(string firstName)
    {
        var customer = CreateCustomer(firstName, "ValidLastName", "email@example.com", "1234567890");
        var result = TryValidateModel(customer, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }


    // negative test cases for firstname
    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow(null)]
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 51 'a's, just over the limit
    [DataRow("123")] // Numeric
    [DataRow("Name@123")] // Special characters
    public void Customer_WithInvalidFirstName_ShouldFailValidation(string firstName)
    {
        var customer = CreateCustomer(firstName, "ValidLastName", "email@example.com", "1234567890");
        var result = TryValidateModel(customer, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

    // positive test cases for lastname
    [TestMethod]
    [DataRow("Smith")]
    [DataRow("Johnson")]
    [DataRow("S")] // Minimum valid length
    [DataRow("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb")] // 50 'b's, maximum valid length
    public void Customer_WithValidLastName_ShouldPassValidation(string lastName)
    {
        var customer = CreateCustomer("ValidFirstName", lastName, "email@example.com", "1234567890");
        var result = TryValidateModel(customer, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    // negative test cases for lastname
    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow(null)]
    [DataRow("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb")] // 53 'b's, just over the limit
    [DataRow("456")] // Numeric
    [DataRow("Last@Name")] // Special characters
    public void Customer_WithInvalidLastName_ShouldFailValidation(string lastName)
    {
        var customer = CreateCustomer("ValidFirstName", lastName, "email@example.com", "1234567890");
        var result = TryValidateModel(customer, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }
    // positive test cases for email
    [TestMethod]
    [DataRow("email@example.com")]
    [DataRow("firstname.lastname@example.co.uk")]
    [DataRow("user@sub.example.com")] // Contains Dot in Domain
    [DataRow("a@b.c")] // Short but valid email

    public void Customer_WithValidEmail_ShouldPassValidation(string email)
    {
        var customer = CreateCustomer("ValidFirstName", "ValidLastName", email, "1234567890");
        var result = TryValidateModel(customer, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }
    // negative test cases for email
    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow(null)]
    [DataRow("invalid-email")] // Missing @ and domain
    [DataRow("email@domain..com")] // Double dot in domain
    [DataRow("@domain.com")] // Missing username
    [DataRow("name@domain")] // Missing top-level domain
    //[DataRow("<script>alert('XSS')</script>@domain.com")] // Encoded HTML
    [DataRow("name@@domain.com")] // Two @ signs
    //[DataRow(".email@domain.com")] // Leading dot
    //[DataRow("名@domain.com")] // Unicode characters
    public void Customer_WithInvalidEmail_ShouldFailValidation(string email)
    {
        var customer = CreateCustomer("ValidFirstName", "ValidLastName", email, "1234567890");
        var result = TryValidateModel(customer, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }
    // positive test cases for phone numbers
    [TestMethod]
    [DataRow("123-456-7890")] // Valid Phone Number
    [DataRow("1234567890")] // Just Numbers
    [DataRow("+1-234-567-8900")] // Contains Country Code
    [DataRow("12345678901234567890")] // 20 characters Maximum Length
    public void Customer_WithValidPhone_ShouldPassValidation(string phone)
    {
        var customer = CreateCustomer("ValidFirstName", "ValidLastName", "email@example.com", phone);
        var result = TryValidateModel(customer, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }
    // negative test cases for phone numbre
    [TestMethod]
    [DataRow("")] // Empty String
    [DataRow(" ")]
    [DataRow("123-ABCD-EFGH")] // Invalid Format
    [DataRow("123456789012345678901")] // 21 characters Just Over Maximum
    //[DataRow("123")]
    [DataRow(null)]
    public void Customer_WithInvalidPhone_ShouldFailValidation(string phone)
    {
        var customer = CreateCustomer("ValidFirstName", "ValidLastName", "email@example.com", phone);
        var result = TryValidateModel(customer, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }
}
