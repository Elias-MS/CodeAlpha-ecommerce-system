-- ===============================================
-- Advanced E-Commerce Database Creation Script
-- Professional E-Commerce System with Complete Features
-- ===============================================

-- Create Database
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'AdvancedECommerceDB')
BEGIN
    CREATE DATABASE AdvancedECommerceDB;
END
GO

USE AdvancedECommerceDB;
GO

-- ===============================================
-- 1. USERS TABLE (Enhanced with security features)
-- ===============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' AND xtype='U')
BEGIN
    CREATE TABLE Users (
        UserId INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(50) UNIQUE NOT NULL,
        PasswordHash NVARCHAR(255) NOT NULL,
        Salt NVARCHAR(100) NOT NULL,
        Email NVARCHAR(100) UNIQUE NOT NULL,
        FirstName NVARCHAR(50) NOT NULL,
        LastName NVARCHAR(50) NOT NULL,
        Phone NVARCHAR(20),
        Address NVARCHAR(500),
        City NVARCHAR(50),
        State NVARCHAR(50),
        ZipCode NVARCHAR(10),
        Country NVARCHAR(50) DEFAULT 'Ethiopia',
        DateOfBirth DATE,
        Gender NVARCHAR(10),
        IsActive BIT DEFAULT 1,
        IsEmailVerified BIT DEFAULT 0,
        LastLoginDate DATETIME,
        PasswordResetToken NVARCHAR(255),
        PasswordResetExpiry DATETIME,
        CreatedDate DATETIME DEFAULT GETDATE(),
        UpdatedDate DATETIME DEFAULT GETDATE()
    );
END
GO

-- ===============================================
-- 2. ADMINS TABLE (Separate admin management)
-- ===============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Admins' AND xtype='U')
BEGIN
    CREATE TABLE Admins (
        AdminId INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(50) UNIQUE NOT NULL,
        PasswordHash NVARCHAR(255) NOT NULL,
        Salt NVARCHAR(100) NOT NULL,
        Email NVARCHAR(100) UNIQUE NOT NULL,
        FullName NVARCHAR(100) NOT NULL,
        Role NVARCHAR(50) DEFAULT 'Admin',
        Permissions NVARCHAR(500),
        IsActive BIT DEFAULT 1,
        LastLoginDate DATETIME,
        CreatedDate DATETIME DEFAULT GETDATE(),
        CreatedBy INT
    );
END
GO

-- ===============================================
-- 3. CATEGORIES TABLE (Product categorization)
-- ===============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Categories' AND xtype='U')
BEGIN
    CREATE TABLE Categories (
        CategoryId INT IDENTITY(1,1) PRIMARY KEY,
        CategoryName NVARCHAR(100) UNIQUE NOT NULL,
        Description NVARCHAR(500),
        ParentCategoryId INT NULL,
        ImagePath NVARCHAR(255),
        IsActive BIT DEFAULT 1,
        SortOrder INT DEFAULT 0,
        CreatedDate DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (ParentCategoryId) REFERENCES Categories(CategoryId)
    );
END
GO

-- ===============================================
-- 4. PRODUCTS TABLE (Enhanced with more details)
-- ===============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Products' AND xtype='U')
BEGIN
    CREATE TABLE Products (
        ProductId INT IDENTITY(1,1) PRIMARY KEY,
        ProductCode NVARCHAR(50) UNIQUE NOT NULL,
        Name NVARCHAR(200) NOT NULL,
        Description NVARCHAR(1000),
        ShortDescription NVARCHAR(255),
        CategoryId INT NOT NULL,
        Brand NVARCHAR(100),
        Model NVARCHAR(100),
        Price DECIMAL(10,2) NOT NULL,
        DiscountPrice DECIMAL(10,2),
        CostPrice DECIMAL(10,2),
        Stock INT NOT NULL DEFAULT 0,
        MinStockLevel INT DEFAULT 5,
        Weight DECIMAL(8,2),
        Dimensions NVARCHAR(100),
        Color NVARCHAR(50),
        Size NVARCHAR(50),
        Material NVARCHAR(100),
        Warranty NVARCHAR(100),
        MainImagePath NVARCHAR(255),
        ImageGallery NVARCHAR(1000),
        Tags NVARCHAR(500),
        MetaTitle NVARCHAR(200),
        MetaDescription NVARCHAR(500),
        IsFeatured BIT DEFAULT 0,
        IsActive BIT DEFAULT 1,
        ViewCount INT DEFAULT 0,
        SalesCount INT DEFAULT 0,
        AverageRating DECIMAL(3,2) DEFAULT 0,
        ReviewCount INT DEFAULT 0,
        CreatedDate DATETIME DEFAULT GETDATE(),
        UpdatedDate DATETIME DEFAULT GETDATE(),
        CreatedBy INT,
        FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
    );
END
GO

-- ===============================================
-- 5. CART TABLE (Persistent shopping cart)
-- ===============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Cart' AND xtype='U')
BEGIN
    CREATE TABLE Cart (
        CartId INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        ProductId INT NOT NULL,
        Quantity INT NOT NULL DEFAULT 1,
        Price DECIMAL(10,2) NOT NULL,
        AddedDate DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (UserId) REFERENCES Users(UserId),
        FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
        UNIQUE(UserId, ProductId)
    );
END
GO

-- ===============================================
-- 6. WISHLIST TABLE (Favorite products)
-- ===============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Wishlist' AND xtype='U')
BEGIN
    CREATE TABLE Wishlist (
        WishlistId INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        ProductId INT NOT NULL,
        AddedDate DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (UserId) REFERENCES Users(UserId),
        FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
        UNIQUE(UserId, ProductId)
    );
END
GO

-- ===============================================
-- 7. ORDERS TABLE (Enhanced order management)
-- ===============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Orders' AND xtype='U')
BEGIN
    CREATE TABLE Orders (
        OrderId INT IDENTITY(1,1) PRIMARY KEY,
        OrderNumber NVARCHAR(50) UNIQUE NOT NULL,
        UserId INT NOT NULL,
        OrderDate DATETIME DEFAULT GETDATE(),
        Status NVARCHAR(50) DEFAULT 'Pending',
        SubTotal DECIMAL(10,2) NOT NULL,
        TaxAmount DECIMAL(10,2) DEFAULT 0,
        ShippingCost DECIMAL(10,2) DEFAULT 0,
        DiscountAmount DECIMAL(10,2) DEFAULT 0,
        TotalAmount DECIMAL(10,2) NOT NULL,
        PaymentMethod NVARCHAR(50),
        PaymentStatus NVARCHAR(50) DEFAULT 'Pending',
        ShippingAddress NVARCHAR(500),
        BillingAddress NVARCHAR(500),
        TrackingNumber NVARCHAR(100),
        EstimatedDelivery DATETIME,
        DeliveredDate DATETIME,
        Notes NVARCHAR(1000),
        CreatedDate DATETIME DEFAULT GETDATE(),
        UpdatedDate DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (UserId) REFERENCES Users(UserId)
    );
END
GO

-- ===============================================
-- 8. ORDER DETAILS TABLE (Order line items)
-- ===============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='OrderDetails' AND xtype='U')
BEGIN
    CREATE TABLE OrderDetails (
        OrderDetailId INT IDENTITY(1,1) PRIMARY KEY,
        OrderId INT NOT NULL,
        ProductId INT NOT NULL,
        ProductName NVARCHAR(200) NOT NULL,
        ProductCode NVARCHAR(50),
        Quantity INT NOT NULL,
        UnitPrice DECIMAL(10,2) NOT NULL,
        DiscountAmount DECIMAL(10,2) DEFAULT 0,
        TotalPrice DECIMAL(10,2) NOT NULL,
        FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
        FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
    );
END
GO

-- ===============================================
-- 9. PAYMENTS TABLE (Payment tracking)
-- ===============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Payments' AND xtype='U')
BEGIN
    CREATE TABLE Payments (
        PaymentId INT IDENTITY(1,1) PRIMARY KEY,
        OrderId INT NOT NULL,
        PaymentMethod NVARCHAR(50) NOT NULL,
        PaymentAmount DECIMAL(10,2) NOT NULL,
        PaymentStatus NVARCHAR(50) DEFAULT 'Pending',
        TransactionId NVARCHAR(100),
        PaymentDate DATETIME DEFAULT GETDATE(),
        PaymentGateway NVARCHAR(50),
        GatewayResponse NVARCHAR(1000),
        Notes NVARCHAR(500),
        FOREIGN KEY (OrderId) REFERENCES Orders(OrderId)
    );
END
GO

-- ===============================================
-- 10. REVIEWS TABLE (Product reviews and ratings)
-- ===============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Reviews' AND xtype='U')
BEGIN
    CREATE TABLE Reviews (
        ReviewId INT IDENTITY(1,1) PRIMARY KEY,
        ProductId INT NOT NULL,
        UserId INT NOT NULL,
        OrderId INT,
        Rating INT NOT NULL CHECK (Rating >= 1 AND Rating <= 5),
        Title NVARCHAR(200),
        Comment NVARCHAR(1000),
        IsVerifiedPurchase BIT DEFAULT 0,
        IsApproved BIT DEFAULT 0,
        HelpfulCount INT DEFAULT 0,
        CreatedDate DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
        FOREIGN KEY (UserId) REFERENCES Users(UserId),
        FOREIGN KEY (OrderId) REFERENCES Orders(OrderId)
    );
END
GO

-- ===============================================
-- 11. INVENTORY TABLE (Stock management)
-- ===============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Inventory' AND xtype='U')
BEGIN
    CREATE TABLE Inventory (
        InventoryId INT IDENTITY(1,1) PRIMARY KEY,
        ProductId INT NOT NULL,
        TransactionType NVARCHAR(50) NOT NULL, -- 'IN', 'OUT', 'ADJUSTMENT'
        Quantity INT NOT NULL,
        RemainingStock INT NOT NULL,
        Reason NVARCHAR(200),
        ReferenceId INT, -- OrderId for sales, PurchaseId for restocking
        CreatedDate DATETIME DEFAULT GETDATE(),
        CreatedBy INT,
        FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
    );
END
GO

-- ===============================================
-- 12. REPORTS TABLE (System reports)
-- ===============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Reports' AND xtype='U')
BEGIN
    CREATE TABLE Reports (
        ReportId INT IDENTITY(1,1) PRIMARY KEY,
        ReportType NVARCHAR(100) NOT NULL,
        ReportName NVARCHAR(200) NOT NULL,
        Parameters NVARCHAR(1000),
        GeneratedBy INT,
        GeneratedDate DATETIME DEFAULT GETDATE(),
        FilePath NVARCHAR(500),
        FileSize BIGINT,
        Status NVARCHAR(50) DEFAULT 'Generated'
    );
END
GO

-- ===============================================
-- 13. SYSTEM LOGS TABLE (Activity logging)
-- ===============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='SystemLogs' AND xtype='U')
BEGIN
    CREATE TABLE SystemLogs (
        LogId INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT,
        AdminId INT,
        Action NVARCHAR(100) NOT NULL,
        TableName NVARCHAR(50),
        RecordId INT,
        OldValues NVARCHAR(2000),
        NewValues NVARCHAR(2000),
        IPAddress NVARCHAR(50),
        UserAgent NVARCHAR(500),
        CreatedDate DATETIME DEFAULT GETDATE()
    );
END
GO

-- ===============================================
-- 14. COUPONS TABLE (Discount management)
-- ===============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Coupons' AND xtype='U')
BEGIN
    CREATE TABLE Coupons (
        CouponId INT IDENTITY(1,1) PRIMARY KEY,
        CouponCode NVARCHAR(50) UNIQUE NOT NULL,
        Description NVARCHAR(200),
        DiscountType NVARCHAR(20) NOT NULL, -- 'PERCENTAGE', 'FIXED'
        DiscountValue DECIMAL(10,2) NOT NULL,
        MinOrderAmount DECIMAL(10,2) DEFAULT 0,
        MaxDiscountAmount DECIMAL(10,2),
        UsageLimit INT DEFAULT 1,
        UsedCount INT DEFAULT 0,
        ValidFrom DATETIME NOT NULL,
        ValidTo DATETIME NOT NULL,
        IsActive BIT DEFAULT 1,
        CreatedDate DATETIME DEFAULT GETDATE(),
        CreatedBy INT
    );
END
GO


-- ===============================================
-- 16. ANNOUNCEMENTS TABLE (Global Messages)
-- ===============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Announcements' AND xtype='U')
BEGIN
    CREATE TABLE Announcements (
        AnnouncementId INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(200) NOT NULL,
        Content NVARCHAR(MAX) NOT NULL,
        TargetRole NVARCHAR(50) DEFAULT 'All',
        IsActive BIT DEFAULT 1,
        CreatedBy INT,
        CreatedDate DATETIME DEFAULT GETDATE(),
        ExpiryDate DATETIME
    );
END
GO

-- ===============================================
-- CREATE INDEXES FOR PERFORMANCE
-- ===============================================

-- Users indexes
CREATE INDEX IX_Users_Email ON Users(Email);
CREATE INDEX IX_Users_Username ON Users(Username);
CREATE INDEX IX_Users_IsActive ON Users(IsActive);

-- Products indexes
CREATE INDEX IX_Products_CategoryId ON Products(CategoryId);
CREATE INDEX IX_Products_Name ON Products(Name);
CREATE INDEX IX_Products_Price ON Products(Price);
CREATE INDEX IX_Products_IsActive ON Products(IsActive);
CREATE INDEX IX_Products_IsFeatured ON Products(IsFeatured);
CREATE INDEX IX_Products_ProductCode ON Products(ProductCode);

-- Orders indexes
CREATE INDEX IX_Orders_UserId ON Orders(UserId);
CREATE INDEX IX_Orders_OrderDate ON Orders(OrderDate);
CREATE INDEX IX_Orders_Status ON Orders(Status);
CREATE INDEX IX_Orders_OrderNumber ON Orders(OrderNumber);

-- OrderDetails indexes
CREATE INDEX IX_OrderDetails_OrderId ON OrderDetails(OrderId);
CREATE INDEX IX_OrderDetails_ProductId ON OrderDetails(ProductId);

-- Cart indexes
CREATE INDEX IX_Cart_UserId ON Cart(UserId);
CREATE INDEX IX_Cart_ProductId ON Cart(ProductId);

-- Reviews indexes
CREATE INDEX IX_Reviews_ProductId ON Reviews(ProductId);
CREATE INDEX IX_Reviews_UserId ON Reviews(UserId);
CREATE INDEX IX_Reviews_Rating ON Reviews(Rating);

-- ===============================================
-- INSERT SAMPLE DATA
-- ===============================================

-- Insert Default Admin
IF NOT EXISTS (SELECT * FROM Admins WHERE Username = 'admin')
BEGIN
    INSERT INTO Admins (Username, PasswordHash, Salt, Email, FullName, Role, Permissions) 
    VALUES ('admin', 'hashed_password_here', 'salt_here', 'admin@ecommerce.com', 'System Administrator', 'SuperAdmin', 'ALL');
END
GO

-- Insert Categories
IF NOT EXISTS (SELECT * FROM Categories)
BEGIN
    INSERT INTO Categories (CategoryName, Description) VALUES 
    ('Electronics', 'Electronic devices and gadgets'),
    ('Computers', 'Laptops, desktops, and computer accessories'),
    ('Mobile Phones', 'Smartphones and mobile accessories'),
    ('Clothing', 'Fashion and apparel'),
    ('Men''s Clothing', 'Clothing for men'),
    ('Women''s Clothing', 'Clothing for women'),
    ('Books', 'Books and educational materials'),
    ('Home & Garden', 'Home improvement and garden supplies'),
    ('Sports', 'Sports equipment and accessories'),
    ('Beauty', 'Beauty and personal care products');
    
    -- Update parent categories
    UPDATE Categories SET ParentCategoryId = 1 WHERE CategoryName IN ('Computers', 'Mobile Phones');
    UPDATE Categories SET ParentCategoryId = 4 WHERE CategoryName IN ('Men''s Clothing', 'Women''s Clothing');
END
GO

-- Insert Sample Products
IF NOT EXISTS (SELECT * FROM Products)
BEGIN
    INSERT INTO Products (ProductCode, Name, Description, CategoryId, Brand, Price, Stock, MainImagePath) VALUES 
    ('LAPTOP001', 'Gaming Laptop Pro', 'High-performance gaming laptop with RTX graphics', 2, 'TechBrand', 1299.99, 15, '/images/laptop1.jpg'),
    ('PHONE001', 'Smartphone X1', 'Latest flagship smartphone with AI camera', 3, 'PhoneBrand', 899.99, 25, '/images/phone1.jpg'),
    ('SHIRT001', 'Cotton T-Shirt', 'Premium cotton t-shirt for everyday wear', 5, 'FashionBrand', 29.99, 100, '/images/shirt1.jpg'),
    ('BOOK001', 'Programming Guide', 'Complete guide to modern programming', 7, 'TechBooks', 49.99, 50, '/images/book1.jpg'),
    ('MOUSE001', 'Wireless Gaming Mouse', 'Precision gaming mouse with RGB lighting', 2, 'GamerTech', 79.99, 30, '/images/mouse1.jpg'),
    ('KEYBOARD001', 'Mechanical Keyboard', 'Professional mechanical keyboard', 2, 'GamerTech', 129.99, 20, '/images/keyboard1.jpg'),
    ('MONITOR001', '4K Gaming Monitor', '27-inch 4K gaming monitor with HDR', 2, 'DisplayTech', 399.99, 12, '/images/monitor1.jpg'),
    ('HEADPHONE001', 'Noise-Canceling Headphones', 'Premium wireless headphones', 1, 'AudioTech', 249.99, 18, '/images/headphones1.jpg'),
    ('JEANS001', 'Classic Blue Jeans', 'Comfortable fit blue jeans', 5, 'DenimBrand', 69.99, 40, '/images/jeans1.jpg'),
    ('SNEAKERS001', 'Running Sneakers', 'Lightweight running shoes', 9, 'SportsBrand', 119.99, 35, '/images/sneakers1.jpg');
END
GO

-- ===============================================
-- CREATE STORED PROCEDURES
-- ===============================================

-- Procedure to get dashboard statistics
CREATE OR ALTER PROCEDURE GetDashboardStats
AS
BEGIN
    SELECT 
        (SELECT COUNT(*) FROM Users WHERE IsActive = 1) AS TotalCustomers,
        (SELECT COUNT(*) FROM Products WHERE IsActive = 1) AS TotalProducts,
        (SELECT COUNT(*) FROM Orders) AS TotalOrders,
        (SELECT ISNULL(SUM(TotalAmount), 0) FROM Orders WHERE Status = 'Delivered' OR Status = 'Completed') AS TotalRevenue,
        (SELECT COUNT(*) FROM Orders WHERE Status = 'Delivered' OR Status = 'Completed') AS DeliveredOrders,
        (SELECT COUNT(*) FROM Orders WHERE Status = 'Cancelled') AS CancelledOrders,
        (SELECT COUNT(*) FROM Orders WHERE Status = 'Pending') AS PendingOrders,
        (SELECT COUNT(*) FROM Reviews WHERE IsApproved = 0) AS PendingReviews
END
GO

-- Procedure to update product rating
CREATE OR ALTER PROCEDURE UpdateProductRating
    @ProductId INT
AS
BEGIN
    UPDATE Products 
    SET AverageRating = (
        SELECT ISNULL(AVG(CAST(Rating AS DECIMAL(3,2))), 0) 
        FROM Reviews 
        WHERE ProductId = @ProductId AND IsApproved = 1
    ),
    ReviewCount = (
        SELECT COUNT(*) 
        FROM Reviews 
        WHERE ProductId = @ProductId AND IsApproved = 1
    )
    WHERE ProductId = @ProductId
END
GO

-- ===============================================
-- CREATE TRIGGERS
-- ===============================================

-- Trigger to update inventory when order is placed
CREATE OR ALTER TRIGGER TR_OrderDetails_UpdateInventory
ON OrderDetails
AFTER INSERT
AS
BEGIN
    INSERT INTO Inventory (ProductId, TransactionType, Quantity, RemainingStock, Reason, ReferenceId)
    SELECT 
        i.ProductId,
        'OUT',
        -i.Quantity,
        p.Stock - i.Quantity,
        'Sale',
        i.OrderId
    FROM inserted i
    INNER JOIN Products p ON i.ProductId = p.ProductId
    
    UPDATE Products 
    SET Stock = Stock - i.Quantity,
        SalesCount = SalesCount + i.Quantity
    FROM Products p
    INNER JOIN inserted i ON p.ProductId = i.ProductId
END
GO

-- ===============================================
-- COMPLETION MESSAGE
-- ===============================================
PRINT '===============================================';
PRINT 'Advanced E-Commerce Database Created Successfully!';
PRINT '===============================================';
PRINT 'Database: AdvancedECommerceDB';
PRINT 'Tables Created: 14 tables with relationships';
PRINT 'Indexes: Performance optimized';
PRINT 'Stored Procedures: Dashboard and utility procedures';
PRINT 'Triggers: Inventory management automation';
PRINT '';
PRINT 'Default Admin Credentials:';
PRINT 'Username: admin';
PRINT 'Password: admin123 (Remember to hash in application)';
PRINT '';
PRINT 'Sample Data: Categories and Products added';
PRINT '===============================================';
GO