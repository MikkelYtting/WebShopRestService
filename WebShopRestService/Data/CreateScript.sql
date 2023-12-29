-- Categories Table
CREATE TABLE Categories (
                            CategoryID INT AUTO_INCREMENT PRIMARY KEY,
                            Name VARCHAR(255),
                            Description TEXT
);

-- Addresses Table
CREATE TABLE Addresses (
                           AddressID INT AUTO_INCREMENT PRIMARY KEY,
                           Street VARCHAR(255),
                           City VARCHAR(255),
                           PostalCode VARCHAR(20),
                           Country VARCHAR(255)
);

-- Roles Table
CREATE TABLE Roles (
                       RoleID INT AUTO_INCREMENT PRIMARY KEY,
                       Name VARCHAR(255),
                       AccessLevel INT
);

-- UserCredentials Table for Authentication and Authorization
CREATE TABLE UserCredentials (
                                 UserID INT AUTO_INCREMENT PRIMARY KEY,
                                 Username VARCHAR(255) UNIQUE NOT NULL,
                                 HashedPassword VARCHAR(255) NOT NULL,
                                 RoleID INT,
                                 FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);

-- Customers Table
CREATE TABLE Customers (
                           CustomerID INT AUTO_INCREMENT PRIMARY KEY,
                           FirstName VARCHAR(255),
                           LastName VARCHAR(255),
                           Email VARCHAR(255) UNIQUE NOT NULL,
                           Phone VARCHAR(20),
                           AddressID INT,
                           UserID INT,
                           FOREIGN KEY (AddressID) REFERENCES Addresses(AddressID),
                           FOREIGN KEY (UserID) REFERENCES UserCredentials(UserID)
);

-- Products Table
CREATE TABLE Products (
                          ProductID INT AUTO_INCREMENT PRIMARY KEY,
                          Name VARCHAR(255),
                          Description TEXT,
                          Price DECIMAL(10, 2),
                          StockQuantity INT,
                          CategoryID INT,
                          Img TEXT,
                          FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);

-- OrderTables
CREATE TABLE OrderTables (
                             OrderID INT AUTO_INCREMENT PRIMARY KEY,
                             OrderDate DATETIME,
                             TotalAmount DECIMAL(10, 2),
                             CustomerID INT,
                             DeliveryAddressID INT,
                             FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
                             FOREIGN KEY (DeliveryAddressID) REFERENCES Addresses(AddressID)
);

-- OrderItems Table
CREATE TABLE OrderItems (
                            OrderItemID INT AUTO_INCREMENT PRIMARY KEY,
                            Quantity INT,
                            Price DECIMAL(10, 2),
                            OrderID INT,
                            ProductID INT,
                            FOREIGN KEY (OrderID) REFERENCES OrderTables(OrderID),
                            FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- Payments Table
CREATE TABLE Payments (
                          PaymentID INT AUTO_INCREMENT PRIMARY KEY,
                          PaymentMethod VARCHAR(50),
                          PaymentDate DATETIME,
                          Amount DECIMAL(10, 2),
                          OrderID INT,
                          FOREIGN KEY (OrderID) REFERENCES OrderTables(OrderID)
);

-- ProductAudits Table
CREATE TABLE ProductAudits (
                               AuditID INT AUTO_INCREMENT PRIMARY KEY,
                               OldPrice DECIMAL(10, 2),
                               NewPrice DECIMAL(10, 2),
                               ChangeDate DATETIME,
                               ProductID INT,
                               FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- PaymentAudits Table
CREATE TABLE PaymentAudits (
                               PaymentAuditID INT AUTO_INCREMENT PRIMARY KEY,
                               OrderID INT NOT NULL,
                               Date DATE NOT NULL,
                               Amount DECIMAL(9,2),
                               ActionType VARCHAR(50),
                               ActionDate DATETIME,
                               FOREIGN KEY (OrderID) REFERENCES OrderTables(OrderID)
);
