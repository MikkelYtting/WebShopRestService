CREATE DATABASE WebshopDatabase
USE WebshopDatabase
-- Categories Table
CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(255),
    Description VARCHAR(MAX)
);
GO

-- Addresses Table
CREATE TABLE Addresses (
    AddressID INT PRIMARY KEY IDENTITY(1,1),
    Street VARCHAR(255),
    City VARCHAR(255),
    PostalCode VARCHAR(20),
    Country VARCHAR(255)
);
GO

-- Roles Table
CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY (1,1),
    Name VARCHAR(255),
    AccessLevel INT
)
GO

-- UserCredentials Table for Authentication and Authorization
CREATE TABLE UserCredentials (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(255) UNIQUE NOT NULL,
    HashedPassword VARCHAR(255) NOT NULL,
    RoleID INT FOREIGN KEY REFERENCES Roles(RoleID)
);
GO

-- Customers Table
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(255),
    LastName VARCHAR(255),
    Email VARCHAR(255) UNIQUE NOT NULL,
    Phone VARCHAR(20),
    AddressID INT FOREIGN KEY REFERENCES Addresses(AddressID),
    UserID INT FOREIGN KEY REFERENCES UserCredentials(UserID)
);
GO

CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(255),
    Description VARCHAR(MAX),
    Price DECIMAL(10, 2),
    StockQuantity INT,
    CategoryID INT FOREIGN KEY REFERENCES Categories(CategoryID),
    Img VARCHAR(MAX) 
);
GO

-- OrderTables
CREATE TABLE OrderTables (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    OrderDate DATETIME,
    TotalAmount DECIMAL(10, 2),
    CustomerID INT FOREIGN KEY REFERENCES Customers(CustomerID),
    DeliveryAddressID INT FOREIGN KEY REFERENCES Addresses(AddressID)
);
GO

-- OrderItems Table
CREATE TABLE OrderItems (
    OrderItemID INT PRIMARY KEY IDENTITY(1,1),
    Quantity INT,
    Price DECIMAL(10, 2),
    OrderID INT FOREIGN KEY REFERENCES OrderTables(OrderID),
    ProductID INT FOREIGN KEY REFERENCES Products(ProductID)
);
GO

-- Payments Table
CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    PaymentMethod VARCHAR(50),
    PaymentDate DATETIME,
    Amount DECIMAL(10, 2),
    OrderID INT FOREIGN KEY REFERENCES OrderTables(OrderID)
);
GO

-- ProductAudits Table
CREATE TABLE ProductAudits (
    AuditID INT PRIMARY KEY IDENTITY(1,1),
    OldPrice DECIMAL(10, 2),
    NewPrice DECIMAL(10, 2),
    ChangeDate DATETIME,
    ProductID INT FOREIGN KEY REFERENCES Products(ProductID)
);

-- PaymentAudits Table
CREATE TABLE PaymentAudits (
    PaymentAuditID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT NOT NULL,
    Date DATE NOT NULL,
    Amount DECIMAL(9,2),
    ActionType VARCHAR(50),
    ActionDate DATETIME,
    FOREIGN KEY (OrderID) REFERENCES OrderTables(OrderID)
)
GO
