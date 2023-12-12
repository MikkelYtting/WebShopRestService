using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebShopRestService.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

[TestClass]
public class AddressTests
{
    private Address CreateAddress(string street, string city, string postalCode, string country)
    {
        return new Address
        {
            Street = street,
            City = city,
            PostalCode = postalCode,
            Country = country,
            Customers = new List<Customer>() // Assuming valid customers for simplicity
        };
    }

    private bool TryValidateModel(Address address, out ICollection<ValidationResult> results)
    {
        var context = new ValidationContext(address, serviceProvider: null, items: null);
        results = new List<ValidationResult>();
        return Validator.TryValidateObject(address, context, results, true);
    }

    // Positive Test Cases for Street
    [TestMethod]
    [DataRow("123 Main St")]
    [DataRow("Elm Street")]
    [DataRow("1234, Oak Lane")]
    // Add more DataRow attributes for valid street names
    public void Address_WithValidStreet_ShouldPassValidation(string street)
    {
        var address = CreateAddress(street, "ValidCity", "12345", "ValidCountry");
        var result = TryValidateModel(address, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    // Negative Test Cases for Street
    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow(null)]
    [DataRow("A very long street name that exceeds the maximum length that is allowed for a street name in the Address model")]
    // Add more DataRow attributes for invalid street names
    public void Address_WithInvalidStreet_ShouldFailValidation(string street)
    {
        var address = CreateAddress(street, "ValidCity", "12345", "ValidCountry");
        var result = TryValidateModel(address, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

    // Positive Test Cases for City
    [TestMethod]
    [DataRow("Anytown")]
    [DataRow("Springfield")]
    [DataRow("New York")]
    // Add more DataRow attributes for valid city names
    public void Address_WithValidCity_ShouldPassValidation(string city)
    {
        var address = CreateAddress("123 Main St", city, "12345", "ValidCountry");
        var result = TryValidateModel(address, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    // Negative Test Cases for City
    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow(null)]
    [DataRow("A city name that is way too long to be considered valid based on the Address model's validation rules")]
    // Add more DataRow attributes for invalid city names
    public void Address_WithInvalidCity_ShouldFailValidation(string city)
    {
        var address = CreateAddress("Valid Street", city, "12345", "ValidCountry");
        var result = TryValidateModel(address, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

    // Positive Test Cases for PostalCode
    [TestMethod]
    [DataRow("12345")]
    [DataRow("12345-6789")]
    [DataRow("54321")]
    // Add more DataRow attributes for valid postal codes
    public void Address_WithValidPostalCode_ShouldPassValidation(string postalCode)
    {
        var address = CreateAddress("123 Main St", "ValidCity", postalCode, "ValidCountry");
        var result = TryValidateModel(address, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    // Negative Test Cases for PostalCode
    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow(null)]
    [DataRow("123")]
    [DataRow("ABCDE")]
    [DataRow("123456")]
    // Add more DataRow attributes for invalid postal codes
    public void Address_WithInvalidPostalCode_ShouldFailValidation(string postalCode)
    {
        var address = CreateAddress("123 Main St", "ValidCity", postalCode, "ValidCountry");
        var result = TryValidateModel(address, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

    // Positive Test Cases for Country
    [TestMethod]
    [DataRow("Wonderland")]
    [DataRow("Oz")]
    [DataRow("Narnia")]
    [DataRow("Neverland")]
    // Add more DataRow attributes for valid country names
    public void Address_WithValidCountry_ShouldPassValidation(string country)
    {
        var address = CreateAddress("123 Main St", "ValidCity", "12345", country);
        var result = TryValidateModel(address, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    // Negative Test Cases for Country
    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow(null)]
    [DataRow("A very long country name that exceeds the maximum length that is allowed in the Address model")]
    // Add more DataRow attributes for invalid country names
    public void Address_WithInvalidCountry_ShouldFailValidation(string country)
    {
        var address = CreateAddress("123 Main St", "ValidCity", "12345", country);
        var result = TryValidateModel(address, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

}