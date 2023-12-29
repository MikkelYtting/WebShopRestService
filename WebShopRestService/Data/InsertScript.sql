USE WebshopDatabase;
GO

-- Insert data into Roles
INSERT INTO Roles (Name, AccessLevel) VALUES
('Administrator', 1),
('Customer', 2);
GO

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
GO

-- Insert data into Addresses
INSERT INTO Addresses (Street, City, PostalCode, Country) VALUES
('123 Main St', 'Anytown', '12345', 'USA'),
('456 Elm St', 'Othertown', '23456', 'USA'),
('789 Oak St', 'Sometown', '34567', 'USA'),
('101 Maple St', 'Yourtown', '45678', 'USA'),
('202 Pine St', 'TheirTown', '56789', 'USA'),
('303 Birch St', 'Newtown', '67890', 'USA'),
('404 Cedar St', 'Oldtown', '78901', 'USA'),
('505 Spruce St', 'Westtown', '89012', 'USA'),
('606 Ash St', 'Easttown', '90123', 'USA'),
('707 Willow St', 'Northtown', '01234', 'USA');
GO

-- Insert data into UserCredentials
INSERT INTO UserCredentials (Username, HashedPassword, RoleID) VALUES
('johnsmith', 'HASHED_PASSWORD_HERE', 1),
('emilyjones', 'HASHED_PASSWORD_HERE', 2),
('michaelbrown', 'HASHED_PASSWORD_HERE', 2),
('hannahwhite', 'HASHED_PASSWORD_HERE', 2),
('daviddavis', 'HASHED_PASSWORD_HERE', 2),
('user6', 'HASHED_PASSWORD_HERE', 2),
('user7', 'HASHED_PASSWORD_HERE', 2),
('user8', 'HASHED_PASSWORD_HERE', 2),
('user9', 'HASHED_PASSWORD_HERE', 2),
('user10', 'HASHED_PASSWORD_HERE', 2);
GO

-- Insert data into Customers
INSERT INTO Customers (FirstName, LastName, Email, Phone, AddressID, UserID) VALUES
('John', 'Smith', 'john.smith@email.com', '555-1111', 1, 1),
('Emily', 'Jones', 'emily.jones@email.com', '555-2222', 2, 2),
('Michael', 'Brown', 'michael.brown@email.com', '555-3333', 3, 3),
('Hannah', 'White', 'hannah.white@email.com', '555-4444', 4, 4),
('David', 'Davis', 'david.davis@email.com', '555-5555', 5, 5),
('Customer6', 'Lastname6', 'email6@email.com', '555-6666', 6, 6),
('Customer7', 'Lastname7', 'email7@email.com', '555-7777', 7, 7),
('Customer8', 'Lastname8', 'email8@email.com', '555-8888', 8, 8),
('Customer9', 'Lastname9', 'email9@email.com', '555-9999', 9, 9),
('Customer10', 'Lastname10', 'email10@email.com', '555-0000', 10, 10);
GO

-- Insert data into Products
-- Please make sure that the CategoryID and the image URLs match your actual data
INSERT INTO Products (Name, Description, Price, StockQuantity, CategoryID, Img) VALUES
('Laptop', 'High performance laptop', 4999.99, 100, 1, 'https://media.wired.com/photos/64daad6b4a854832b16fd3bc/master/pass/How-to-Choose-a-Laptop-August-2023-Gear.jpg'),
('IPhone', 'The newest product from Apple', 2999.99, 100, 1, 'https://images.fyndiq.se/images/f_auto/t_600x600/prod/0c572c0b0b2a43b6/ebe836a6e68e/iphone-12-pro-max-cover-bla'),
('PC', 'High performance PC, ready for gaming', 6999.99, 100, 1, 'https://media.wired.com/photos/624df21cb340f55b37084fdc/16:9/w_1400,h_950,c_limit/How-to-Build-a-PC-Gear.jpg'),
('Microphone', 'For the best quality sound', 999.99, 100, 1, 'https://www.zdnet.com/a/img/resize/173e864da543f2b0fccd6466ef310975bf7521f5/2023/11/26/55155b37-dbd9-4991-b36d-14ebfcb82f9d/stellar-x2-microphone.jpg?auto=webp&fit=crop&height=1200&width=1200'),
('The Great Gatsby', 'The novel chronicles an era that Fitzgerald himself dubbed the "Jazz Age". Following the shock and chaos of World War I, American society enjoyed unprecedented levels of prosperity during the "roaring" 1920s as the economy soared.', 319.99, 200, 2, 'https://i.ebayimg.com/images/g/nncAAOSwHBZiACoE/s-l1200.webp'),
('Moby Dick', 'First published in 1851, Melville''s masterpiece is, in Elizabeth Hardwick''s words, "the greatest novel in American literature." The saga of Captain Ahab and his monomaniacal pursuit of the white whale remains a peerless adventure story but one full of mythic grandeur, poetic majesty, and symbolic power.', 419.99, 200, 2, 'https://covers.storytel.com/jpg-640/9783736800748.5677c285-990e-4d28-8ad4-edaab19c59a9?quality=70'),
('Hamlet', 'The Tragedy of Hamlet, Prince of Denmark, or more simply Hamlet, is a tragedy by William Shakespeare, believed to have been written between 1599 and 1601.', 249.99, 200, 2, 'https://g.christianbook.com/g/slideshow/7/72789/main/72789_1_ftc_dp.jpg'),
('The Odyssey', 'The Odyssey is one of two major ancient Greek epic poems attributed to Homer. It is, in part, a sequel to the Iliad, the other work traditionally ascribed to Homer.', 599.99, 200, 2, 'https://cdn.kobo.com/book-images/1c003baf-c48d-45be-9fd3-bc9c2bc6a685/1200/1200/False/the-odyssey-172.jpg'),
('Black T-Shirt', 'Cotton unisex t-shirt', 99.99, 500, 3, 'https://bandmerch.dk/wp-content/uploads/2022/09/tshirt-basic-black-front.jpg'),
('Jeans', 'Baggy Jeans from Levis', 299.99, 500, 3, 'https://lsco.scene7.com/is/image/lsco/A47500006-alt1-pdp-lse?fmt=jpeg&qlt=70&resMode=bisharp&fit=crop,0&op_usm=1.25,0.6,8&wid=1200&hei=1000'),
('Jacket', 'Unisex Puffer Jacket to keep you warm', 399.99, 500, 3, 'https://www.tretorn.dk/pub_images/original/800162050_1.jpg'),
('Socks', 'White socks for the cold winter days', 14.99, 500, 3, 'https://repbasics.dk/wp-content/uploads/socks2-scaled.jpg'),
('Sofa', 'Comfortable leather sofa', 2499.99, 10, 4, 'https://damcache.harald-nyborg.dk/v-637999615712223789/fe/7f/cd92-5791-4f8a-8b94-3c7691460d2b/27937_01.jpg'),
('Chair', 'Comfy wool chair', 999.99, 10, 4, 'https://sw28470.sfstatic.io/upload_dir/shop/_0003_NORR11Little_Big_ChairSheepskin_Moonlight_Front.jpg'),
('Dining Table', 'Wooden table perfect for the living room', 1299.99, 10, 4, 'https://images.eq3.com/product-definitions/cjuve2av001gf0114n14n1g7l/instance/cjv5f9yza056u0186ov5ryrah/THUMBNAIL/91f03b87-0ded-4b6d-b5a6-cac0774b0636.jpg'),
('Cabinet', 'Used to store all your items inside', 1099.99, 10, 4, 'https://www.ikea.com/us/en/images/products/havsta-cabinet-with-base-gray__0720107_pe732421_s5.jpg?f=s'),
('Basketball', 'Outdoor/indoor basketball', 199.99, 150, 5, 'https://www.nordicbasketball.com/wp-content/uploads/2022/10/WTB7500ID_0_7_NBA_OFFICIAL_GAME_BALL_BR.png.cq5dam.web_.1200.1200-PhotoRoom.png'),
('Football', 'Outdoor/indoor football', 249.99, 150, 5, 'https://www.sportsdirect.com/images/products/82819632_h.jpg'),
('Tennis Racket', 'High quality tennis racket for when you play tennis', 499.99, 150, 5, 'https://nwscdn.com/media/catalog/product/v/e/vermont-colt-is-a-tennis-racket-for-all-ages-to-enjoy_1.jpg'),
('Ludo', 'Board game fun for the entire family', 139.99, 100, 6, 'https://ukbuyzone.co.uk/cdn/shop/files/3feb555d-0f54-4896-a5cc-dc7d1baa3231.jpg?v=1696370492'),
('Monopoly', 'Strategy board game', 199.99, 100, 6, 'https://image.smythstoys.com/zoom/159143.jpg'),
('Shampoo', 'Organic hair shampoo', 22.99, 300, 7, 'https://cdn.nicehair.dk/products/96911/loreal-paris-elvital-hyaluron-plump-shampoo-400-ml-1641887281.jpg'),
('Hair Wax', 'To keep your look fresh', 12.99, 300, 7, 'https://www.darimooch.com/cdn/shop/products/Hair-Wax-2_bb7f2525-2b55-4063-9b52-d01d9869b301.jpg?v=1676895072'),
('Car Tires', 'All-weather car tires', 279.99, 80, 8 ,'https://csttires.eu/storage/2021/01/medallion_md_a1.png'),
('Car Battery', 'All powerful new car battery', 979.99, 80, 8 ,'https://i5.walmartimages.com/seo/EverStart-Value-Lead-Acid-Automotive-Battery-Group-Size-26-12-Volt-525-CCA_bac4b4f9-5d19-4e2c-b5f8-7533d420d63a.aaeab574a492d71d7f29f6621334eace.jpeg'),
('Organic Apples', 'Fresh organic apples', 2.99, 1000, 9, 'https://i5.walmartimages.com/asr/35257a70-6d96-40fc-94e4-5e27b2dd4195.ea3985d9f7a6579b2e01329dff80e27f.jpeg?odnHeight=768&odnWidth=768&odnBg=FFFFFF'),
('Sliced Bread', 'Fresh bread right from the bakery', 13.99, 1000, 9, 'https://www.tornado-studios.com/sites/default/files/styles/slider_full/public/products/2028/gallery/sliced_bread_in_bag_thumbnail_square_0000.jpg?itok=v_xPdFUU'),
('Cat Food', 'Nutritional food for cats', 49.99, 200, 10, 'https://headsupfortails.com/cdn/shop/files/WhiskasOceanFishAdultDryCatFood_f5bbf1f9-31dd-433e-99bd-00582d979f60.jpg?v=1683109071'),
('Dog Food', 'Nutritional food for dogs', 49.99, 200, 10, 'https://pedigreeclub.in/cdn/shop/products/B00LHS884Y.MAIN.jpg?v=1673452671')
GO

-- Insert data into OrderTables
INSERT INTO OrderTables (OrderDate, TotalAmount, CustomerID, DeliveryAddressID) VALUES
(GETDATE(), 1000.00, 1, 1),
(GETDATE(), 50.00, 2, 2),
(GETDATE(), 23.99, 3, 3),
(GETDATE(), 450.00, 4, 4),
(GETDATE(), 29.99, 5, 5),
(GETDATE(), 75.50, 6, 6),
(GETDATE(), 150.00, 7, 7),
(GETDATE(), 200.00, 8, 8),
(GETDATE(), 300.00, 9, 9),
(GETDATE(), 400.00, 10, 10);
GO

-- Insert data into OrderItems
INSERT INTO OrderItems (Quantity, Price, OrderID, ProductID) VALUES
(1, 999.99, 1, 1),
(3, 19.99, 2, 2),
(5, 9.99, 3, 3),
(1, 499.99, 4, 4),
(2, 29.99, 5, 5),
(2, 15.99, 6, 6),
(1, 45.99, 7, 7),
(3, 12.99, 8, 8),
(4, 22.99, 9, 9),
(1, 18.99, 10, 10);
GO

-- Insert data into Payments
INSERT INTO Payments (PaymentMethod, PaymentDate, Amount, OrderID) VALUES
('Credit Card', GETDATE(), 1000.00, 1),
('PayPal', GETDATE(), 50.00, 2),
('Credit Card', GETDATE(), 23.99, 3),
('Debit Card', GETDATE(), 450.00, 4),
('Cash', GETDATE(), 29.99, 5),
('Credit Card', GETDATE(), 75.50, 6),
('PayPal', GETDATE(), 150.00, 7),
('Debit Card', GETDATE(), 200.00, 8),
('Cash', GETDATE(), 300.00, 9),
('Credit Card', GETDATE(), 400.00, 10);
GO

-- Insert data into ProductAudits
INSERT INTO ProductAudits (OldPrice, NewPrice, ChangeDate, ProductID) VALUES
(899.99, 999.99, GETDATE(), 1),
(15.99, 19.99, GETDATE(), 2),
(7.99, 9.99, GETDATE(), 3),
(399.99, 499.99, GETDATE(), 4),
(24.99, 29.99, GETDATE(), 5),
(30.99, 34.99, GETDATE(), 6),
(20.99, 24.99, GETDATE(), 7),
(10.99, 12.99, GETDATE(), 8),
(35.99, 39.99, GETDATE(), 9),
(16.99, 19.99, GETDATE(), 10);
GO

-- Insert data into PaymentAudits
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
GO
