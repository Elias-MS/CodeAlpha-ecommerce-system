-- ===============================================
-- E-Commerce System Stored Procedures
-- ===============================================

-- 1. USER PROCEDURES
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_AuthenticateUser') DROP PROCEDURE sp_AuthenticateUser;
GO
CREATE PROCEDURE sp_AuthenticateUser @Username NVARCHAR(50) AS
BEGIN SELECT * FROM Users WHERE Username = @Username AND IsActive = 1 END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_RegisterUser') DROP PROCEDURE sp_RegisterUser;
GO
CREATE PROCEDURE sp_RegisterUser
    @Username NVARCHAR(50), @PasswordHash NVARCHAR(255), @Salt NVARCHAR(100), @Email NVARCHAR(100),
    @FirstName NVARCHAR(50), @LastName NVARCHAR(50), @Phone NVARCHAR(20), @Address NVARCHAR(500),
    @City NVARCHAR(50), @State NVARCHAR(50), @ZipCode NVARCHAR(10), @Country NVARCHAR(50),
    @DateOfBirth DATE, @Gender NVARCHAR(10)
AS
BEGIN
    INSERT INTO Users (Username, PasswordHash, Salt, Email, FirstName, LastName, Phone, Address, City, State, ZipCode, Country, DateOfBirth, Gender) 
    VALUES (@Username, @PasswordHash, @Salt, @Email, @FirstName, @LastName, @Phone, @Address, @City, @State, @ZipCode, @Country, @DateOfBirth, @Gender)
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetAllCategories') DROP PROCEDURE sp_GetAllCategories;
GO
CREATE PROCEDURE sp_GetAllCategories AS
BEGIN SELECT * FROM Categories WHERE IsActive = 1 ORDER BY SortOrder, CategoryName END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetMainCategories') DROP PROCEDURE sp_GetMainCategories;
GO
CREATE PROCEDURE sp_GetMainCategories AS
BEGIN SELECT * FROM Categories WHERE ParentCategoryId IS NULL AND IsActive = 1 ORDER BY SortOrder, CategoryName END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetSubCategories') DROP PROCEDURE sp_GetSubCategories;
GO
CREATE PROCEDURE sp_GetSubCategories @ParentCategoryId INT AS
BEGIN SELECT * FROM Categories WHERE ParentCategoryId = @ParentCategoryId AND IsActive = 1 ORDER BY SortOrder, CategoryName END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetCategoryById') DROP PROCEDURE sp_GetCategoryById;
GO
CREATE PROCEDURE sp_GetCategoryById @CategoryId INT AS
BEGIN SELECT * FROM Categories WHERE CategoryId = @CategoryId END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_AddCategory') DROP PROCEDURE sp_AddCategory;
GO
CREATE PROCEDURE sp_AddCategory @CategoryName NVARCHAR(100), @Description NVARCHAR(500), @ParentCategoryId INT, @ImagePath NVARCHAR(255), @SortOrder INT AS
BEGIN INSERT INTO Categories (CategoryName, Description, ParentCategoryId, ImagePath, SortOrder) VALUES (@CategoryName, @Description, @ParentCategoryId, @ImagePath, @SortOrder) END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_UpdateCategory') DROP PROCEDURE sp_UpdateCategory;
GO
CREATE PROCEDURE sp_UpdateCategory @CategoryId INT, @CategoryName NVARCHAR(100), @Description NVARCHAR(500), @ParentCategoryId INT, @ImagePath NVARCHAR(255), @SortOrder INT AS
BEGIN UPDATE Categories SET CategoryName = @CategoryName, Description = @Description, ParentCategoryId = @ParentCategoryId, ImagePath = @ImagePath, SortOrder = @SortOrder WHERE CategoryId = @CategoryId END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_DeleteCategory') DROP PROCEDURE sp_DeleteCategory;
GO
CREATE PROCEDURE sp_DeleteCategory @CategoryId INT AS
BEGIN UPDATE Categories SET IsActive = 0 WHERE CategoryId = @CategoryId END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetCategoriesWithProductCount') DROP PROCEDURE sp_GetCategoriesWithProductCount;
GO
CREATE PROCEDURE sp_GetCategoriesWithProductCount AS
BEGIN
    SELECT c.*, COUNT(p.ProductId) as ProductCount FROM Categories c
    LEFT JOIN Products p ON c.CategoryId = p.CategoryId AND p.IsActive = 1
    WHERE c.IsActive = 1
    GROUP BY c.CategoryId, c.CategoryName, c.Description, c.ParentCategoryId, c.ImagePath, c.IsActive, c.SortOrder, c.CreatedDate
    ORDER BY c.SortOrder, c.CategoryName
END
GO

-- 3. PRODUCT PROCEDURES
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetAllProducts') DROP PROCEDURE sp_GetAllProducts;
GO
CREATE PROCEDURE sp_GetAllProducts AS
BEGIN SELECT * FROM Products WHERE IsActive = 1 ORDER BY Name END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetProductById') DROP PROCEDURE sp_GetProductById;
GO
CREATE PROCEDURE sp_GetProductById @ProductId INT AS
BEGIN SELECT * FROM Products WHERE ProductId = @ProductId END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_SearchProducts') DROP PROCEDURE sp_SearchProducts;
GO
CREATE PROCEDURE sp_SearchProducts @SearchTerm NVARCHAR(200) AS
BEGIN
    SELECT * FROM Products WHERE IsActive = 1 AND 
    (Name LIKE '%' + @SearchTerm + '%' OR ProductCode LIKE '%' + @SearchTerm + '%' OR Brand LIKE '%' + @SearchTerm + '%' OR Model LIKE '%' + @SearchTerm + '%')
    ORDER BY Name
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_UpdateStock') DROP PROCEDURE sp_UpdateStock;
GO
CREATE PROCEDURE sp_UpdateStock @ProductId INT, @Stock INT AS
BEGIN UPDATE Products SET Stock = @Stock WHERE ProductId = @ProductId END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetFeaturedProducts') DROP PROCEDURE sp_GetFeaturedProducts;
GO
CREATE PROCEDURE sp_GetFeaturedProducts AS
BEGIN 
    SELECT p.*, c.CategoryName 
    FROM Products p 
    LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
    WHERE p.IsActive = 1 
    ORDER BY p.IsFeatured DESC, p.CreatedDate DESC 
END
GO

-- 5. ORDER PROCEDURES
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CreateOrder') DROP PROCEDURE sp_CreateOrder;
GO
CREATE PROCEDURE sp_CreateOrder
    @OrderNumber NVARCHAR(50), @UserId INT, @SubTotal DECIMAL(10,2), @TaxAmount DECIMAL(10,2), @ShippingCost DECIMAL(10,2), @DiscountAmount DECIMAL(10,2), @TotalAmount DECIMAL(10,2), @PaymentMethod NVARCHAR(50), @PaymentRef NVARCHAR(100), @Phone NVARCHAR(20), @EstDelivery DATETIME, @ShippingAddress NVARCHAR(500), @BillingAddress NVARCHAR(500), @CurrencyCode NVARCHAR(10), @Proof NVARCHAR(500)
AS
BEGIN
    INSERT INTO Orders (OrderNumber, UserId, SubTotal, TaxAmount, ShippingCost, DiscountAmount, TotalAmount, PaymentMethod, PaymentReference, Phone, EstimatedDelivery, ShippingAddress, BillingAddress, CurrencyCode, PaymentProofImage) 
    VALUES (@OrderNumber, @UserId, @SubTotal, @TaxAmount, @ShippingCost, @DiscountAmount, @TotalAmount, @PaymentMethod, @PaymentRef, @Phone, @EstDelivery, @ShippingAddress, @BillingAddress, @CurrencyCode, @Proof);
    SELECT SCOPE_IDENTITY() AS OrderId;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetAllOrders') DROP PROCEDURE sp_GetAllOrders;
GO
CREATE PROCEDURE sp_GetAllOrders AS
BEGIN SELECT o.*, u.Username FROM Orders o INNER JOIN Users u ON o.UserId = u.UserId ORDER BY o.OrderDate DESC END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetDashboardStats') DROP PROCEDURE GetDashboardStats;
GO
CREATE PROCEDURE GetDashboardStats AS
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
