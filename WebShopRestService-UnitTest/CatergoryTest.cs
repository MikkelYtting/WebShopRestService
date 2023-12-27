using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebShopRestService.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

[TestClass]
public class CategoryTests
{
    private Category CreateCategory(string name, string description)
    {
        return new Category
        {
            Name = name,
            Description = description
        };
    }

    /// <summary>
    /// Tries to validate a Category model instance and collects validation results.
    /// This method is a crucial part of unit testing for Entity Framework models.
    /// It attempts to validate a model instance based on the specified validation rules in the model class.
    /// If the model passes all validation rules, the method returns true; otherwise, it returns false.
    /// The out ICollection<ValidationResult> parameter collects the details of validation failures,
    /// which can be used to assert the expected behavior in unit tests.
    /// </summary>
    /// <param name="category">The Category instance to validate.</param>
    /// <param name="results">Collection to store the validation results.</param>
    /// <returns>True if the model is valid, otherwise false.</returns>
    private bool TryValidateModel(Category category, out ICollection<ValidationResult> results)
    {
        var context = new ValidationContext(category, serviceProvider: null, items: null);
        results = new List<ValidationResult>();
        return Validator.TryValidateObject(category, context, results, true);
    }

    // Positive Test Cases for Name
    [TestMethod]
    [DataRow("Electronics")]
    [DataRow("Books")]
    [DataRow("Aa")] // Minimum Length (2 characters)
    [DataRow("Category_1-2")] // Name with valid characters (letters, numbers, spaces, hyphens, underscores)
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 100 'a's Maximum
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 99 'a's jsut below
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 50 'a's middle of range
    public void Category_WithValidName_ShouldPassValidation(string name)
    {
        var category = CreateCategory(name, "Valid Description");
        var result = TryValidateModel(category, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    // Negative Test Cases for Name
    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow(null)]
    [DataRow("A")] // Just Below Minimum (1 character)
    [DataRow("Invalid*Name")] // Invalid Characters
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 101 'a's Just Over Maximum (101 characters)
    [DataRow("Category@123")] // Special characters
    [DataRow("   ")] // Whitespaces
    public void Category_WithInvalidName_ShouldFailValidation(string name)
    {
        var category = CreateCategory(name, "Valid Description");
        var result = TryValidateModel(category, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }

    // Positive Test Cases for Description
    [TestMethod]
    [DataRow("This is a valid description for a category.")]
    [DataRow("")] // Empty is valid
    [DataRow("A")] // Just Above Minimum (1 character)
    // Maximum characters 500
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 500 'a's
    // Just below 499 characters
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 500 'a's
    public void Category_WithValidDescription_ShouldPassValidation(string description)
    {
        var category = CreateCategory("Valid Name", description);
        var result = TryValidateModel(category, out var validationResults);
        Assert.IsTrue(result);
        Assert.AreEqual(0, validationResults.Count);
    }

    // Negative Test Cases for Description
    [TestMethod]
    // 501 characters
    [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // 501 'a's
    public void Category_WithInvalidDescription_ShouldFailValidation(string description)
    {
        var category = CreateCategory("Valid Name", description);
        var result = TryValidateModel(category, out var validationResults);
        Assert.IsFalse(result);
        Assert.IsTrue(validationResults.Count > 0);
    }
}
