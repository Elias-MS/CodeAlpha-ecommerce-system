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