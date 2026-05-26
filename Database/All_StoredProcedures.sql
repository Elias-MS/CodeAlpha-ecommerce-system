-- ========================================================================================
-- E-COMMERCE SPEED: ALL STORED PROCEDURES
-- This file contains all stored procedures required by the application, 
-- including the ones already present and the missing Category procedures.
-- ========================================================================================

USE AdvancedECommerceDB;
GO

-- ========================================================================================
-- EXISTING STORED PROCEDURES
-- ========================================================================================

-- =============================================
-- 1. sp_GetFeaturedProducts
-- Purpose: Retrieves all active products that are marked as 'Featured'.
--          It joins the Products table with Categories to include the CategoryName.
--          Results are ordered so featured items and newer items appear first.
-- =============================================
CREATE OR ALTER PROCEDURE sp_GetFeaturedProducts AS
BEGIN 
    SELECT p.*, c.CategoryName 
    FROM Products p 
    LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
    WHERE p.IsActive = 1 
    ORDER BY p.IsFeatured DESC, p.CreatedDate DESC 
END
GO

-- =============================================
-- 2. GetDashboardStats
-- Purpose: Quickly aggregates essential metrics for the Admin Dashboard.
--          By running these 9 fast counts/sums directly on the database server,
--          we avoid downloading large datasets to the application.
-- =============================================
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
        (SELECT COUNT(*) FROM Reviews WHERE IsApproved = 0) AS PendingReviews,
        (SELECT COUNT(*) FROM Complaints WHERE Status = 'Pending') AS PendingComplaints
END
GO

-- =============================================
-- 3. UpdateProductRating
-- Purpose: Automatically recalculates a product's average 5-star rating and total review count.
--          It is typically triggered whenever a new review is approved by an administrator.
-- Parameters:
--   @ProductId - The ID of the specific product to update.
-- =============================================
CREATE OR ALTER PROCEDURE UpdateProductRating
    @ProductId INT 
AS
BEGIN
    UPDATE Products 
    SET 
        AverageRating = (
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

-- ========================================================================================
-- CATEGORY MODULE STORED PROCEDURES (Reconstructed for CategoryService.cs)
-- ========================================================================================

-- =============================================
-- 4. sp_GetAllCategories
-- Purpose: Retrieves a list of all categories in the database.
--          Results are ordered by their designated SortOrder, then alphabetically.
-- =============================================
CREATE OR ALTER PROCEDURE sp_GetAllCategories
AS
BEGIN
    SELECT * FROM Categories ORDER BY SortOrder, CategoryName;
END
GO

-- =============================================
-- 5. sp_GetMainCategories
-- Purpose: Retrieves only the "top-level" or "parent" categories.
--          These are categories that do not have a ParentCategoryId assigned.
-- =============================================
CREATE OR ALTER PROCEDURE sp_GetMainCategories
AS
BEGIN
    SELECT * FROM Categories WHERE ParentCategoryId IS NULL ORDER BY SortOrder, CategoryName;
END
GO

-- =============================================
-- 6. sp_GetSubCategories
-- Purpose: Retrieves all child categories belonging to a specific parent category.
-- Parameters:
--   @parentCategoryId - The ID of the parent category.
-- =============================================
CREATE OR ALTER PROCEDURE sp_GetSubCategories
    @parentCategoryId INT
AS
BEGIN
    SELECT * FROM Categories WHERE ParentCategoryId = @parentCategoryId ORDER BY SortOrder, CategoryName;
END
GO

-- =============================================
-- 7. sp_GetCategoryById
-- Purpose: Retrieves all details for a single specific category based on its ID.
-- Parameters:
--   @categoryId - The unique identifier of the category to fetch.
-- =============================================
CREATE OR ALTER PROCEDURE sp_GetCategoryById
    @categoryId INT
AS
BEGIN
    SELECT * FROM Categories WHERE CategoryId = @categoryId;
END
GO

-- =============================================
-- 8. sp_AddCategory
-- Purpose: Inserts a newly created category into the database and sets it as active.
-- Parameters:
--   @categoryName     - Name of the new category.
--   @description      - Text description of the category.
--   @parentCategoryId - (Optional) ID of the parent if this is a subcategory.
--   @imagePath        - (Optional) File path or URL to the category image.
--   @sortOrder        - Number dictating display order.
-- =============================================
CREATE OR ALTER PROCEDURE sp_AddCategory
    @categoryName NVARCHAR(100),
    @description NVARCHAR(500),
    @parentCategoryId INT = NULL,
    @imagePath NVARCHAR(255),
    @sortOrder INT
AS
BEGIN
    INSERT INTO Categories (CategoryName, Description, ParentCategoryId, ImagePath, SortOrder, IsActive, CreatedDate)
    VALUES (@categoryName, @description, @parentCategoryId, @imagePath, @sortOrder, 1, GETDATE());
END
GO

-- =============================================
-- 9. sp_UpdateCategory
-- Purpose: Modifies the details of an existing category in the database.
-- Parameters: Same as AddCategory, plus the @categoryId to identify which one to update.
-- =============================================
CREATE OR ALTER PROCEDURE sp_UpdateCategory
    @categoryId INT,
    @categoryName NVARCHAR(100),
    @description NVARCHAR(500),
    @parentCategoryId INT = NULL,
    @imagePath NVARCHAR(255),
    @sortOrder INT
AS
BEGIN
    UPDATE Categories
    SET CategoryName = @categoryName,
        Description = @description,
        ParentCategoryId = @parentCategoryId,
        ImagePath = @imagePath,
        SortOrder = @sortOrder
    WHERE CategoryId = @categoryId;
END
GO

-- =============================================
-- 10. sp_DeleteCategory
-- Purpose: Completely removes a category from the database. 
--          (Ensure products are reassigned before running this to avoid orphaned data).
-- Parameters:
--   @categoryId - The ID of the category to delete.
-- =============================================
CREATE OR ALTER PROCEDURE sp_DeleteCategory
    @categoryId INT
AS
BEGIN
    DELETE FROM Categories WHERE CategoryId = @categoryId;
END
GO

-- =============================================
-- 11. sp_IsCategoryNameExists
-- Purpose: Checks if a category name is already taken. Used for validation before saving.
--          If updating an existing category, the @excludeCategoryId ignores itself.
-- Parameters:
--   @categoryName      - The name to check.
--   @excludeCategoryId - (Optional) Skips checking against this specific category ID.
-- =============================================
CREATE OR ALTER PROCEDURE sp_IsCategoryNameExists
    @categoryName NVARCHAR(100),
    @excludeCategoryId INT = NULL
AS
BEGIN
    IF @excludeCategoryId IS NOT NULL
        SELECT COUNT(*) FROM Categories WHERE CategoryName = @categoryName AND CategoryId != @excludeCategoryId;
    ELSE
        SELECT COUNT(*) FROM Categories WHERE CategoryName = @categoryName;
END
GO

-- =============================================
-- 12. sp_GetCategoriesWithProductCount
-- Purpose: Retrieves all categories alongside the total number of active products 
--          currently assigned to each category.
-- =============================================
CREATE OR ALTER PROCEDURE sp_GetCategoriesWithProductCount
AS
BEGIN
    SELECT c.*, ISNULL(COUNT(p.ProductId), 0) AS ProductCount
    FROM Categories c
    LEFT JOIN Products p ON c.CategoryId = p.CategoryId AND p.IsActive = 1
    GROUP BY c.CategoryId, c.CategoryName, c.Description, c.ParentCategoryId, c.ImagePath, c.IsActive, c.SortOrder, c.CreatedDate
    ORDER BY c.SortOrder, c.CategoryName;
END
GO

-- =============================================
-- 13. sp_UpdateCategorySortOrder
-- Purpose: Quickly updates just the display 'SortOrder' property of a category.
--          Used when dragging-and-dropping or rearranging category lists.
-- Parameters:
--   @categoryId - The ID of the category.
--   @sortOrder  - The new numeric position for display.
-- =============================================
CREATE OR ALTER PROCEDURE sp_UpdateCategorySortOrder
    @categoryId INT,
    @sortOrder INT
AS
BEGIN
    UPDATE Categories SET SortOrder = @sortOrder WHERE CategoryId = @categoryId;
END
GO

-- =============================================
-- 14. sp_GetCategoryHierarchy
-- Purpose: Retrieves all categories sorted in a way that makes it easy to build 
--          nested menu structures or tree-views in the application UI.
-- =============================================
CREATE OR ALTER PROCEDURE sp_GetCategoryHierarchy
AS
BEGIN
    SELECT * FROM Categories ORDER BY ParentCategoryId, SortOrder, CategoryName;
END
GO
