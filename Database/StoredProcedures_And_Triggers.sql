-- ========================================================================================
-- E-COMMERCE SPEED: STORED PROCEDURES & TRIGGERS
-- These scripts run directly on the SQL Database to improve performance and automate tasks.
-- ========================================================================================

USE AdvancedECommerceDB;
GO

-- ========================================================================================
-- PROCEDURE: GetDashboardStats
-- Purpose: Quickly calculates all statistics needed for the Admin Dashboard overview.
--          It runs 9 fast counts/sums directly on the server instead of downloading all data.
-- ========================================================================================
CREATE OR ALTER PROCEDURE GetDashboardStats
AS
BEGIN
    SELECT 
        -- Counts how many active users exist in the system
        (SELECT COUNT(*) FROM Users WHERE IsActive = 1) AS TotalCustomers,
        
        -- Counts how many active products exist in the inventory
        (SELECT COUNT(*) FROM Products WHERE IsActive = 1) AS TotalProducts,
        
        -- Counts the absolute total number of orders placed (historical and active)
        (SELECT COUNT(*) FROM Orders) AS TotalOrders,
        
        -- Calculates the total revenue (money made) only from successful, completed orders
        (SELECT ISNULL(SUM(TotalAmount), 0) FROM Orders WHERE Status = 'Delivered' OR Status = 'Completed') AS TotalRevenue,
        
        -- Counts how many orders have been successfully delivered to the customer
        (SELECT COUNT(*) FROM Orders WHERE Status = 'Delivered' OR Status = 'Completed') AS DeliveredOrders,
        
        -- Counts how many orders were cancelled
        (SELECT COUNT(*) FROM Orders WHERE Status = 'Cancelled') AS CancelledOrders,
        
        -- Counts how many orders are waiting to be processed or shipped
        (SELECT COUNT(*) FROM Orders WHERE Status = 'Pending') AS PendingOrders,
        
        -- Counts how many customer reviews are waiting for admin approval before going public
        (SELECT COUNT(*) FROM Reviews WHERE IsApproved = 0) AS PendingReviews,
        
        -- Counts how many customer support complaints are unresolved
        (SELECT COUNT(*) FROM Complaints WHERE Status = 'Pending') AS PendingComplaints
END
GO

-- ========================================================================================
-- PROCEDURE: UpdateProductRating
-- Purpose: Automatically recalculates a product's average 5-star rating whenever a new
--          review is approved. This saves the application from doing the math later.
-- ========================================================================================
CREATE OR ALTER PROCEDURE UpdateProductRating
    @ProductId INT -- The ID of the specific product being updated
AS
BEGIN
    UPDATE Products 
    SET 
        -- Averages all approved ratings for this specific product (e.g. 4.5)
        AverageRating = (
            SELECT ISNULL(AVG(CAST(Rating AS DECIMAL(3,2))), 0) 
            FROM Reviews 
            WHERE ProductId = @ProductId AND IsApproved = 1
        ),
        
        -- Counts the total number of approved reviews for this product
        ReviewCount = (
            SELECT COUNT(*) 
            FROM Reviews 
            WHERE ProductId = @ProductId AND IsApproved = 1
        )
    WHERE ProductId = @ProductId
END
GO

-- ========================================================================================
-- TRIGGER: TR_OrderDetails_UpdateInventory
-- Purpose: This fires AUTOMATICALLY whenever a customer successfully checks out.
--          When an item is added to an Order (OrderDetails), this trigger instantly deducts
--          the inventory so no one else can buy stock that is already sold.
-- ========================================================================================
CREATE TRIGGER TR_OrderDetails_UpdateInventory
ON OrderDetails
AFTER INSERT -- Triggers exactly after a new order detail row is inserted
AS
BEGIN
    -- 1. Create a historical record in the Inventory table that stock left the warehouse
    INSERT INTO Inventory (ProductId, TransactionType, Quantity, RemainingStock, Reason, ReferenceId)
    SELECT 
        i.ProductId,
        'OUT',                      -- Marks this as an outgoing transaction
        -i.Quantity,                -- Make the quantity negative since stock is dropping
        p.Stock - i.Quantity,       -- Calculate the new remaining stock
        'Sale',                     -- Reason for inventory change
        i.OrderId                   -- Links this log to the exact customer Order ID
    FROM inserted i
    INNER JOIN Products p ON i.ProductId = p.ProductId
    
    -- 2. Actually deduct the stock from the main Products table
    UPDATE Products 
    SET Stock = Stock - i.Quantity,                   -- Subtract purchased amount from available stock
        SalesCount = SalesCount + i.Quantity          -- Increment the total 'Sales Count' tracker for popular items
    FROM Products p
    INNER JOIN inserted i ON p.ProductId = i.ProductId
END
GO
