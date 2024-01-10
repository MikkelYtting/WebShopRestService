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
    /// <summary>
    /// Forsøger at validere et Address objekt og returnerer valideringsresultaterne.
    /// Denne metode anvendes til at sikre, at et Address objekt opfylder alle definerede valideringskrav
    /// før det behandles yderligere (f.eks. gemmes i databasen).
    /// </summary>
    /// 
    /// <returns>Returnerer true, hvis objektet opfylder alle valideringsregler, ellers false.</returns>
    /// <param name="address">Address objektet, der skal valideres.</param>
    private bool TryValidateModel(Address address, out ICollection<ValidationResult> results)
    {
        // Opretter et ValidationContext objekt, som indkapsler den kontekst, valideringen skal foregå i.
        var context = new ValidationContext(address, serviceProvider: null, items: null);
        // Initialiser en tom liste til at holde valideringsresultaterne
        results = new List<ValidationResult>();
        // Anvender Validator klassen til at prøve at validere objektet
        return Validator.TryValidateObject(address, context, results, true);
    }

    // Positive Test Cases for Street
    [TestMethod]
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 100 'a's Maximum Length
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 99 'a's Just Below Boundary
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 50 'a's Middle of Range
    [DataRow("aaa")] // 3 a´s Minimum Length
    [DataRow("Elm Street")] // 
    [DataRow("12345")] // Street name with only digits
    [DataRow("'-.,")] // Street name with only special characters
    [DataRow("1234, Oak Lane")] // Street name with valid characters and numbers
    //[DataRow("A".PadRight(99, 'A'))] // Just Below Boundary
    //[DataRow("A".PadRight(50, 'A'))] // Middle of Range// 
    public void Address_WithValidStreet_ShouldPassValidation(string street)
    {
        // Arrange: Opsætning af testdata.
        // Opretter en ny Address-instans med det givne gadenavn og foruddefinerede gyldige værdier for de andre felter.
        var address = CreateAddress(street, "ValidCity", "12345", "ValidCountry");

        // Act: Udførelse af den handling, der skal testes.
        // Validerer den oprettede Address-instans og opsamler eventuelle valideringsresultater.
        var result = TryValidateModel(address, out var validationResults);

        // Assert: Bekræftelse af, at handlingen har produceret det forventede resultat.
        // Assert.IsTrue(result) bekræfter, at valideringen er lykkedes, hvilket betyder, at objektet betragtes som gyldigt.
        // Assert.AreEqual(0, validationResults.Count) bekræfter, at der ikke er fundet nogen valideringsfejl.
        // Her betyder 'true' for 'result', at objektet er gyldigt, og '0' i 'validationResults.Count' betyder, at der er 0 valideringsfejl.
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    // Negative Test Cases for Street
    [TestMethod]
    [DataRow("")] // Empty
    [DataRow(" ")] //Whitespace
    [DataRow(null)] // Null
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 101 'a's.  Just Over Boundary
    [DataRow("A very long street name that exceeds the maximum length that is allowed for a street name in the Address model")]
    [DataRow("a")] // Just below boundary
    [DataRow("aa")] // Just below boundary
    [DataRow("A1!")] // Street name with invalid character
    [DataRow("123 Elm$ Street")] // Street name with a mix of valid and invalid characters
    public void Address_WithInvalidStreet_ShouldFailValidation(string street)
    {
        var address = CreateAddress(street, "ValidCity", "12345", "ValidCountry");
        var result = TryValidateModel(address, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }
    // Positive test cases for City
    [TestMethod]
    [DataRow("Anytown")]  // Normal case
    [DataRow("Springfield")]  // Normal case
    [DataRow("New York")]  // City name with space
    [DataRow("A")]  // Minimum Length
    [DataRow("San-Francisco")]  // City name with hyphen
    [DataRow("Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 50 'a's Maximum Length
    [DataRow("Bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb")] // 49 'b's Just Below Boundary
    [DataRow("Ccccccccccccccccccccccccccc")] // 25 'c's Middle of Range
    public void Address_WithValidCity_ShouldPassValidation(string city)
    {
        var address = CreateAddress("123 Main St", city, "12345", "ValidCountry");
        var result = TryValidateModel(address, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    // Negative Test Cases for City
    [TestMethod]
    [DataRow("")] // Empty
    [DataRow(" ")] // Whitespace
    [DataRow(null)] // Null
    [DataRow("Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 51 'a's. Just Over Boundary
    [DataRow("123City")] // City name with numbers
    [DataRow("City!")] // City name with special characters
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
    [DataRow("12345")] // Valid Format
    [DataRow("12345-6789")] // Valid Format with Extension
    [DataRow("12345-1234")] // Maximum Length (10 characters)
    [DataRow("1234-5678")] // Just Below Maximum (9 characters)
    [DataRow("1234")] // Minimum Length (4 characters)
    public void Address_WithValidPostalCode_ShouldPassValidation(string postalCode)
    {
        var address = CreateAddress("Valid Street", "ValidCity", postalCode, "ValidCountry");
        var result = TryValidateModel(address, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }


    // Negative Test Cases for PostalCode
    [TestMethod]
    [DataRow(" ")]
    [DataRow("123")] // Below Minimum Boundary
    [DataRow("12345-67890")] // Just Over Maximum Boundary (11 characters)
    [DataRow("ABC123")] // Invalid Format
    [DataRow(null)] // Null
    [DataRow("")] // Empty
    // Add more DataRow attributes for invalid postal codes
    public void Address_WithInvalidPostalCode_ShouldFailValidation(string postalCode)
    {
        var address = CreateAddress("123 Main St", "ValidCity", postalCode, "ValidCountry");
        var result = TryValidateModel(address, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

    // Positive Test Cases for Country
    [DataRow("A")] // Minimum Length
    [DataRow("United States")] // Normal Case
    [DataRow("United-Kingdom")] // Country name with hyphen
    [DataRow("New Zealand")] // Country name with space
    [DataRow("A country name that is exactly fifty-six characters long")] // Maximum Length (56 characters)
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
    [DataRow(" ")] // Whitespace
    [DataRow("")] // Empty
    [DataRow("A country name that is way too long to be considered valid")] // Just Over Maximum Boundary (57 characters)
    [DataRow("United States 123")] // Invalid Characters
    [DataRow(null)] // Null
    // Add more DataRow attributes for invalid country names
    public void Address_WithInvalidCountry_ShouldFailValidation(string country)
    {
        var address = CreateAddress("123 Main St", "ValidCity", "12345", country);
        var result = TryValidateModel(address, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

}