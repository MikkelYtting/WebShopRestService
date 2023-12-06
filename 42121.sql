-- Insert data into Roles
INSERT INTO Roles (Name, AccessLevel) VALUES
('Administrator', 10),
('Manager', 8),
('Customer Service', 6),
('Support', 5),
('Maintenance', 4),
('Human Resources', 3),
('Marketing', 3),
('Sales', 2),
('IT', 1),
('Janitor', 1);

-- Insert data into Categories
INSERT INTO Categories (Name, Description) VALUES
('Electronics', 'Devices and gadgets'),
('Books', 'Fiction and non-fiction literature'),
('Clothing', 'Men and Women apparel'),
('Home & Garden', 'Furniture and gardening'),
('Sports', 'Sporting goods and outdoor activities'),
('Toys & Games', 'Children''s toys and board games'),
('Health & Beauty', 'Personal care products'),
('Automotive', 'Car accessories and parts'),
('Groceries', 'Food and beverages'),
('Pet Supplies', 'Pet food and accessories');

-- Insert data into Addresses
INSERT INTO Addresses (Street, City, PostalCode, Country) VALUES
('123 Main St', 'Anytown', '12345', 'USA'),
('456 Elm St', 'Othertown', '23456', 'USA'),
('789 Oak St', 'Sometown', '34567', 'USA'),
('101 Maple St', 'Yourtown', '45678', 'USA'),
('202 Pine St', 'TheirTown', '56789', 'USA'),
('303 Birch St', 'Whotown', '67890', 'USA'),
('404 Cedar St', 'Howtown', '78901', 'USA'),
('505 Spruce St', 'Thistown', '89012', 'USA'),
('606 Ash St', 'Thatown', '90123', 'USA'),
('707 Dogwood St', 'Theirtown', '01234', 'USA');

-- Insert data into UserCredentials
INSERT INTO UserCredentials (Username, HashedPassword, RoleID) VALUES
('johnsmith', 'hashpass1', 1),
('emilyjones', 'hashpass2', 2),
('michaelbrown', 'hashpass3', 3),
('hannahwhite', 'hashpass4', 4),
('daviddavis', 'hashpass5', 5),
('sarahwilson', 'hashpass6', 6),
('brianmartinez', 'hashpass7', 7),
('laurathomas', 'hashpass8', 8),
('kevinjackson', 'hashpass9', 9),
('rachelmoore', 'hashpass10', 10);

-- Insert data into Customers
INSERT INTO Customers (FirstName, LastName, Email, Phone, AddressID, UserID) VALUES
('John', 'Smith', 'john.smith@email.com', '555-1111', 1, 1),
('Emily', 'Jones', 'emily.jones@email.com', '555-2222', 2, 2),
('Michael', 'Brown', 'michael.brown@email.com', '555-3333', 3, 3),
('Hannah', 'White', 'hannah.white@email.com', '555-4444', 4, 4),
('David', 'Davis', 'david.davis@email.com', '555-5555', 5, 5),
('Sarah', 'Wilson', 'sarah.wilson@email.com', '555-6666', 6, 6),
('Brian', 'Martinez', 'brian.martinez@email.com', '555-7777', 7, 7),
('Laura', 'Thomas', 'laura.thomas@email.com', '555-8888', 8, 8),
('Kevin', 'Jackson', 'kevin.jackson@email.com', '555-9999', 9, 9),
('Rachel', 'Moore', 'rachel.moore@email.com', '555-0000', 10, 10);

-- Insert data into Products
INSERT INTO Products (Name, Description, Price, StockQuantity, CategoryID) VALUES
('Laptop', 'High performance laptop', 999.99, 100, 1),
('Book', 'A novel by a famous author', 19.99, 200, 2),
('T-Shirt', 'Cotton unisex t-shirt', 9.99, 500, 3),
('Sofa', 'Comfortable leather sofa', 499.99, 10, 4),
('Basketball', 'Outdoor/indoor basketball', 29.99, 150, 5),
('Board Game', 'Strategy board game', 39.99, 100, 6),
('Shampoo', 'Organic hair shampoo', 12.99, 300, 7),
('Car Tires', 'All-weather car tires', 79.99, 80, 8),
('Organic Apples', 'Fresh organic apples', 2.99, 1000, 9),
('Cat Food', 'Nutritional food for cats', 19.99, 200, 10);

-- Insert data into OrderTables
-- Assuming the `OrderDate` is the current date for simplicity
INSERT INTO OrderTables (OrderDate, TotalAmount, CustomerID, DeliveryAddressID) VALUES
(GETDATE(), 1000.00, 1, 1),
(GETDATE(), 50.00, 2, 2),
(GETDATE(), 23.99, 3, 3),
(GETDATE(), 450.00, 4, 4),
(GETDATE(), 29.99, 5, 5),
(GETDATE(), 89.99, 6, 6),
(GETDATE(), 15.99, 7, 7),
(GETDATE(), 99.99, 8, 8),
(GETDATE(), 3.99, 9, 9),
(GETDATE(), 500.00, 10, 10);

-- Insert data into OrderItems
-- Assuming one product per order item for simplicity
INSERT INTO OrderItems (Quantity, Price, OrderID, ProductID) VALUES
(1, 999.99, 1, 1),
(3, 19.99, 2, 2),
(5, 9.99, 3, 3),
(1, 499.99, 4, 4),
(2, 29.99, 5, 5),
(2, 39.99, 6, 6),
(10, 12.99, 7, 7),
(4, 79.99, 8, 8),
(100, 2.99, 9, 9),
(10, 19.99, 10, 10);

-- Insert data into Payments
-- Assuming the `PaymentDate` is the current date for simplicity
INSERT INTO Payments (PaymentMethod, PaymentDate, Amount, OrderID) VALUES
('Credit Card', GETDATE(), 1000.00, 1),
('PayPal', GETDATE(), 50.00, 2),
('Credit Card', GETDATE(), 23.99, 3),
('Debit Card', GETDATE(), 450.00, 4),
('Cash', GETDATE(), 29.99, 5),
('Credit Card', GETDATE(), 89.99, 6),
('PayPal', GETDATE(), 15.99, 7),
('Debit Card', GETDATE(), 99.99, 8),
('Cash', GETDATE(), 3.99, 9),
('Credit Card', GETDATE(), 500.00, 10);

-- Insert data into ProductAudits
-- Assuming the `ChangeDate` is the current date for simplicity
INSERT INTO ProductAudits (OldPrice, NewPrice, ChangeDate, ProductID) VALUES
(899.99, 999.99, GETDATE(), 1),
(15.99, 19.99, GETDATE(), 2),
(7.99, 9.99, GETDATE(), 3),
(399.99, 499.99, GETDATE(), 4),
(24.99, 29.99, GETDATE(), 5),
(34.99, 39.99, GETDATE(), 6),
(9.99, 12.99, GETDATE(), 7),
(69.99, 79.99, GETDATE(), 8),
(1.99, 2.99, GETDATE(), 9),
(17.99, 19.99, GETDATE(), 10);

-- Insert data into PaymentAudits
-- Assuming the `ActionDate` is the current date for simplicity
INSERT INTO PaymentAudits (OrderID, Date, Amount, ActionType, ActionDate) VALUES
(1, GETDATE(), 1000.00, 'Charge', GETDATE()),
(2, GETDATE(), 50.00, 'Charge', GETDATE()),
(3, GETDATE(), 23.99, 'Charge', GETDATE()),
(4, GETDATE(), 450.00, 'Charge', GETDATE()),
(5, GETDATE(), 29.99, 'Charge', GETDATE()),
(6, GETDATE(), 89.99, 'Charge', GETDATE()),
(7, GETDATE(), 15.99, 'Charge', GETDATE()),
(8, GETDATE(), 99.99, 'Charge', GETDATE()),
(9, GETDATE(), 3.99, 'Charge', GETDATE()),
(10, GETDATE(), 500.00, 'Charge', GETDATE());
