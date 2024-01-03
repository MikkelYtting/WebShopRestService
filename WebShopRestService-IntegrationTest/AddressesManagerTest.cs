using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebShopRestService.Data;
using WebShopRestService.Managers;
using WebShopRestService.Models;
using WebShopRestService.Repositories;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

[TestClass]
public class AddressesManagerTests
{
    private MyDbContext _context;
    private AddressesManager _manager;
    private AddressesRepository _repository; // Repository instance
    private IDbContextTransaction _transaction;

    /// <summbjary>
    /// Initialiseringsmetode der kaldes før hver testmetode.
    /// Denne metode opretter og konfigurerer de nødvendige afhængigheder for testklassen.
    /// </summary>
    [TestInitialize]
    public void Initialize()
    {
        // Hent forbindelsesstrengen fra miljøvariablerne. Dette tillader forskellige konfigurationer
        // for forskellige testmiljøer 
        var connectionString = Environment.GetEnvironmentVariable("TEST_CONNECTION_STRING");

        // Opbyg options for DbContext. Dette definerer, hvordan Entity Framework skal forbinde til databasen.
        var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();

        if (!string.IsNullOrEmpty(connectionString))
        {
            // Anvend MySQL databaseforbindelse hvis TEST_CONNECTION_STRING er angivet.
            // Dette er nyttigt til at skifte mellem forskellige databasetypen baseret på miljøet.
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        else
        {
            // Hvis ingen forbindelsesstreng er angivet, fald tilbage til en lokal MSSQL forbindelsesstreng.
            // Dette er nyttigt til lokal udvikling og testning.
            connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebshopNew;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            optionsBuilder.UseSqlServer(connectionString);
        }

        // Opret en instans af DbContext med de angivne options.
        var options = optionsBuilder.Options;
        _context = new MyDbContext(options);

        // Opret de nødvendige repository og manager instanser. Disse bruges til at interagere
        // med databasen i testene.
        _repository = new AddressesRepository(_context);
        _manager = new AddressesManager(_repository);

        // Start en database transaktion. Dette sikrer, at ændringer foretaget i en test
        // ikke påvirker andre tests, og gør det muligt at rulle ændringer tilbage efter hver test.
        _transaction = _context.Database.BeginTransaction();
    }

    /// <summary>
    /// Testmetode for at validere hentning af alle adresser.
    /// </summary>
    [TestMethod]
    public async Task GetAddressesAsync_ShouldReturnAllAddresses()
    {
        // Arrange - Sørg for, at der er data i testdatabasen for Adresser

        // Act
        IEnumerable<Address> addresses = await _manager.GetAddressesAsync();

        // Assert
        Assert.IsNotNull(addresses);
        Assert.IsTrue(addresses.Any(), "Der bør være mindst én adresse.");
    }

    /// <summary>
    /// Testmetode for at validere hentning af en enkelt adresse ved ID.
    /// </summary>
    [TestMethod]
    public async Task GetAddressByIdAsync_ShouldReturnAddress_WhenAddressExists()
    {
        // Arrange
        int existingAddressId = 1; // Erstat med et ID, du ved findes

        // Act
        Address address = await _manager.GetAddressByIdAsync(existingAddressId);

        // Assert
        Assert.IsNotNull(address, $"Adresse med ID {existingAddressId} bør eksistere.");
    }

    /// <summary>
    /// Testmetode for at validere oprettelse af en ny adresse.
    /// </summary>
    [TestMethod]
    public async Task CreateAddressAsync_ShouldAddNewAddress()
    {
        // Arrange
        var newAddress = new Address // Vi laver et nyt datasæt til oprettelse
        {
            Street = "125 New St",
            City = "NewCity",
            PostalCode = "54321",
            Country = "NewCountry"
        };

        // Act
        Address createdAddress = await _manager.CreateAddressAsync(newAddress);

        // Assert
        Assert.IsNotNull(createdAddress, "Resultatet af oprettelsen bør ikke være null.");
        Assert.IsTrue(createdAddress.AddressId > 0, "Oprettet adresse bør have et ikke-nul ID.");
    }

    /// <summary>
    /// Testmetode for at validere opdatering af en eksisterende adresse.
    /// </summary>
    [TestMethod]
    public async Task UpdateAddressAsync_ShouldModifyExistingAddress()
    {
        // Arrange
        var addressToUpdate = await _context.Addresses.AsNoTracking().FirstOrDefaultAsync(a => a.AddressId == 15);
        if (addressToUpdate == null)
        {
            // Hvis adressen ikke findes i databasen, opret en dummy adresse til testformål
            addressToUpdate = new Address
            {
                Street = "123 Main St",
                City = "YourCity",
                PostalCode = "12345",
                Country = "YourCountry"
            };
            _context.Addresses.Add(addressToUpdate);
            await _context.SaveChangesAsync();
        }

        // Ændre nogle data for opdateringen
        addressToUpdate.Street = "124 Main St";

        // Act
        bool updateResult = await _manager.UpdateAddressAsync(addressToUpdate);

        // Assert
        Assert.IsTrue(updateResult, "Opdatering bør være vellykket.");
        var updatedAddress = await _context.Addresses.FindAsync(addressToUpdate.AddressId);
        Assert.IsNotNull(updatedAddress);
        Assert.AreEqual("124 Main St", updatedAddress.Street, "Gadenavn bør være opdateret.");
    }

    /// <summary>
    /// Testmetode for at validere sletning af en adresse.
    /// </summary>
    [TestMethod]
    public async Task DeleteAddressAsync_ShouldRemoveAddress()
    {
        // Arrange - Opret og gem en ny adresse i databasen
        var addressToDelete = new Address
        {
            Street = "Delete St",
            City = "Delete City",
            PostalCode = "00000",
            Country = "Delete Country"
        };

        _context.Addresses.Add(addressToDelete);
        await _context.SaveChangesAsync();

        // Act - Forsøg at slette adressen
        bool deleteResult = await _manager.DeleteAddressAsync(addressToDelete.AddressId);

        // Assert - Bekræft, at adressen ikke længere er i databasen
        Assert.IsTrue(deleteResult, "Sletning bør være vellykket.");
        var deletedAddress = await _context.Addresses.FindAsync(addressToDelete.AddressId);
        Assert.IsNull(deletedAddress, "Adresse bør være slettet fra databasen.");
    }

    /// <summary>
    /// Oprydningsmetode der køres efter hver test.
    /// Ruller transaktionen tilbage for at annullere eventuelle ændringer foretaget under testen.
    /// </summary>
    [TestCleanup]
    public void Cleanup()
    {
        _transaction.Rollback(); // Dette vil fortryde eventuelle ændringer foretaget under testen
        _transaction.Dispose(); // Bortskaf transaktionen
        _context.Dispose();
    }

    private string GetTestConnectionString()
    {
        // Hent testdatabasens forbindelsesstreng fra et sikkert sted
        // For eksempel fra miljøvariabler eller en lokal konfigurationsfil
        return "YourTestDatabaseConnectionString";
    }
}
