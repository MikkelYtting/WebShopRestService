
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
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(255),
    AccessLevel INT
);
GO

-- UserCredentials Table for Authentication and Authorization
CREATE TABLE UserCredentials (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(255) UNIQUE NOT NULL,
    HashedPassword VARCHAR(255) NOT NULL,
    RoleID INT,
    CONSTRAINT FK_UserCredentials_Roles FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);
GO

-- Customers Table
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(255),
    LastName VARCHAR(255),
    Email VARCHAR(255) UNIQUE NOT NULL,
    Phone VARCHAR(20),
    AddressID INT,
    UserID INT,
    CONSTRAINT FK_Customers_Addresses FOREIGN KEY (AddressID) REFERENCES Addresses(AddressID),
    CONSTRAINT FK_Customers_UserCredentials FOREIGN KEY (UserID) REFERENCES UserCredentials(UserID)
);
GO

-- Products Table
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(255),
    Description VARCHAR(MAX),
    Price DECIMAL(10, 2),
    StockQuantity INT,
    CategoryID INT,
    Img VARCHAR(MAX), -- Changed from TEXT to VARCHAR(MAX) if large text is needed.
    CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);
GO

-- OrderTables
CREATE TABLE OrderTables (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    OrderDate DATETIME,
    TotalAmount DECIMAL(10, 2),
    CustomerID INT,
    DeliveryAddressID INT,
    CONSTRAINT FK_OrderTables_Customers FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    CONSTRAINT FK_OrderTables_Addresses FOREIGN KEY (DeliveryAddressID) REFERENCES Addresses(AddressID)
);
GO

-- OrderItems Table
CREATE TABLE OrderItems (
    OrderItemID INT PRIMARY KEY IDENTITY(1,1),
    Quantity INT,
    Price DECIMAL(10, 2),
    OrderID INT,
    ProductID INT,
    CONSTRAINT FK_OrderItems_OrderTables FOREIGN KEY (OrderID) REFERENCES OrderTables(OrderID),
    CONSTRAINT FK_OrderItems_Products FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);
GO

-- Payments Table
CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    PaymentMethod VARCHAR(50),
    PaymentDate DATETIME,
    Amount DECIMAL(10, 2),
    OrderID INT,
    CONSTRAINT FK_Payments_OrderTables FOREIGN KEY (OrderID) REFERENCES OrderTables(OrderID)
);
GO

-- ProductAudits Table
CREATE TABLE ProductAudits (
    AuditID INT PRIMARY KEY IDENTITY(1,1),
    OldPrice DECIMAL(10, 2),
    NewPrice DECIMAL(10, 2),
    ChangeDate DATETIME,
    ProductID INT,
    CONSTRAINT FK_ProductAudits_Products FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);
GO

-- PaymentAudits Table
CREATE TABLE PaymentAudits (
    PaymentAuditID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT NOT NULL,
    Date DATE NOT NULL,
    Amount DECIMAL(9,2),
    ActionType VARCHAR(50),
    ActionDate DATETIME,
    CONSTRAINT FK_PaymentAudits_OrderTables FOREIGN KEY (OrderID) REFERENCES OrderTables(OrderID)
);
GO
