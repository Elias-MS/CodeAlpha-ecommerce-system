using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using E_commerance_System.Utils;

namespace E_commerance_System.Data
{
    public class DatabaseHelper
    {
        public static string ConnectionString => connectionString;
        private static string connectionString = ConfigurationManager.ConnectionStrings["ECommerceDB"]?.ConnectionString
            ?? @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\OneDrive\Documents\ECommerceDB.mdf;Integrated Security=True;Connect Timeout=30";

        public static void InitializeDatabase()
        {
            try
            {
                CreateDatabaseFile();
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    RunMainScript(connection);
                    CreateTables(connection); // Keep as fallback/supplement
                    MigrateSchema(connection);
                    SeedData(connection);
                    RegisterStoredProcedures(connection);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database initialization failed: {ex.Message}", "Critical Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void CreateDatabaseFile()
        {
            string mdfPath = "";
            string[] parts = connectionString.Split(';');
            foreach (string part in parts)
            {
                if (part.Trim().StartsWith("AttachDbFilename", StringComparison.OrdinalIgnoreCase))
                {
                    mdfPath = part.Split('=')[1].Trim();
                    break;
                }
            }

            if (string.IsNullOrEmpty(mdfPath) || File.Exists(mdfPath))
                return;

            string directory = Path.GetDirectoryName(mdfPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            string dbName = Path.GetFileNameWithoutExtension(mdfPath);
            string masterConn = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30";

            using (var connection = new SqlConnection(masterConn))
            {
                connection.Open();
                string createSql = $"CREATE DATABASE [{dbName}] ON PRIMARY (NAME = N'{dbName}', FILENAME = N'{mdfPath}')";
                using (var cmd = new SqlCommand(createSql, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            System.Threading.Thread.Sleep(500);
        }

        private static void RunMainScript(SqlConnection connection)
        {
            try
            {
                string[] possiblePaths = {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Database", "AdvancedECommerceDB.sql"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "AdvancedECommerceDB.sql"),
                    Path.Combine(Directory.GetCurrentDirectory(), "Database", "AdvancedECommerceDB.sql")
                };

                string scriptPath = "";
                foreach (var path in possiblePaths)
                {
                    if (File.Exists(path)) { scriptPath = path; break; }
                }

                if (string.IsNullOrEmpty(scriptPath)) return;

                string script = File.ReadAllText(scriptPath);
                
                // Remove CREATE DATABASE and USE statements as we are already connected to a specific MDF
                script = System.Text.RegularExpressions.Regex.Replace(script, @"^\s*(CREATE DATABASE|USE)\s+.*$", "", System.Text.RegularExpressions.RegexOptions.Multiline | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                string[] commands = System.Text.RegularExpressions.Regex.Split(script, @"^\s*GO\s*$", System.Text.RegularExpressions.RegexOptions.Multiline | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                foreach (string cmdText in commands)
                {
                    if (string.IsNullOrWhiteSpace(cmdText)) continue;
                    try
                    {
                        using (var cmd = new SqlCommand(cmdText, connection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Some commands might fail if objects already exist, which is expected
                        System.Diagnostics.Debug.WriteLine($"Script command info: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error running main script: {ex.Message}");
            }
        }

        private static void CreateTables(SqlConnection connection)
        {
            ExecuteNonQuery(connection, @"
                IF OBJECT_ID('Users', 'U') IS NULL
                CREATE TABLE Users (
                    UserId INT IDENTITY(1,1) PRIMARY KEY,
                    Username NVARCHAR(50) UNIQUE NOT NULL,
                    PasswordHash NVARCHAR(255) NOT NULL,
                    Salt NVARCHAR(100) NOT NULL,
                    Email NVARCHAR(100) NOT NULL,
                    FirstName NVARCHAR(50) NOT NULL,
                    LastName NVARCHAR(50) NOT NULL,
                    Phone NVARCHAR(20) DEFAULT '',
                    Address NVARCHAR(500) DEFAULT '',
                    City NVARCHAR(50) DEFAULT '',
                    State NVARCHAR(50) DEFAULT '',
                    ZipCode NVARCHAR(10) DEFAULT '',
                    Country NVARCHAR(50) DEFAULT 'Ethiopia',
                    DateOfBirth DATE NULL,
                    Gender NVARCHAR(10) DEFAULT '',
                    IsActive BIT DEFAULT 1,
                    LastLoginDate DATETIME NULL,
                    CreatedDate DATETIME DEFAULT GETDATE(),
                    UpdatedDate DATETIME DEFAULT GETDATE(),
                    PreferredCurrency NVARCHAR(10) DEFAULT 'ETB'
                )");

            ExecuteNonQuery(connection, @"
                IF OBJECT_ID('Admins', 'U') IS NULL
                CREATE TABLE Admins (
                    AdminId INT IDENTITY(1,1) PRIMARY KEY,
                    Username NVARCHAR(50) UNIQUE NOT NULL,
                    PasswordHash NVARCHAR(255) NOT NULL,
                    Salt NVARCHAR(100) NOT NULL,
                    Email NVARCHAR(100) NOT NULL,
                    FullName NVARCHAR(100) NOT NULL,
                    Role NVARCHAR(50) DEFAULT 'Admin',
                    Permissions NVARCHAR(500) DEFAULT '',
                    IsActive BIT DEFAULT 1,
                    LastLoginDate DATETIME NULL,
                    CreatedDate DATETIME DEFAULT GETDATE(),
                    CreatedBy INT NULL
                )");

            ExecuteNonQuery(connection, @"
                IF OBJECT_ID('Categories', 'U') IS NULL
                CREATE TABLE Categories (
                    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
                    CategoryName NVARCHAR(100) UNIQUE NOT NULL,
                    Description NVARCHAR(500) DEFAULT '',
                    ParentCategoryId INT NULL,
                    ImagePath NVARCHAR(255) DEFAULT '',
                    IsActive BIT DEFAULT 1,
                    SortOrder INT DEFAULT 0,
                    CreatedDate DATETIME DEFAULT GETDATE()
                )");

            ExecuteNonQuery(connection, @"
                IF OBJECT_ID('Products', 'U') IS NULL
                CREATE TABLE Products (
                    ProductId INT IDENTITY(1,1) PRIMARY KEY,
                    ProductCode NVARCHAR(50) NOT NULL,
                    Name NVARCHAR(200) NOT NULL,
                    Description NVARCHAR(1000) DEFAULT '',
                    CategoryId INT NOT NULL,
                    Brand NVARCHAR(100) DEFAULT '',
                    Model NVARCHAR(100) DEFAULT '',
                    Price DECIMAL(10,2) NOT NULL,
                    Stock INT NOT NULL DEFAULT 0,
                    MinStockLevel INT DEFAULT 5,
                    MainImagePath NVARCHAR(255) DEFAULT '',
                    Tags NVARCHAR(500) DEFAULT '',
                    IsFeatured BIT DEFAULT 0,
                    IsActive BIT DEFAULT 1,
                    CreatedDate DATETIME DEFAULT GETDATE(),
                    UpdatedDate DATETIME DEFAULT GETDATE(),
                    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
                )");

            ExecuteNonQuery(connection, @"
                IF OBJECT_ID('Orders', 'U') IS NULL
                CREATE TABLE Orders (
                    OrderId INT IDENTITY(1,1) PRIMARY KEY,
                    OrderNumber NVARCHAR(50) NOT NULL,
                    UserId INT NOT NULL,
                    OrderDate DATETIME DEFAULT GETDATE(),
                    Status NVARCHAR(50) DEFAULT 'Pending',
                    SubTotal DECIMAL(10,2) NOT NULL,
                    TaxAmount DECIMAL(10,2) DEFAULT 0,
                    ShippingCost DECIMAL(10,2) DEFAULT 0,
                    DiscountAmount DECIMAL(10,2) DEFAULT 0,
                    TotalAmount DECIMAL(10,2) NOT NULL,
                    PaymentMethod NVARCHAR(50) DEFAULT '',
                    PaymentStatus NVARCHAR(50) DEFAULT 'Pending',
                    ShippingAddress NVARCHAR(500) DEFAULT '',
                    BillingAddress NVARCHAR(500) DEFAULT '',
                    Phone NVARCHAR(20) DEFAULT '',
                    PaymentReference NVARCHAR(100) DEFAULT '',
                    CurrencyCode NVARCHAR(10) DEFAULT 'ETB',
                    PaymentProofImage NVARCHAR(500) DEFAULT '',
                    CreatedDate DATETIME DEFAULT GETDATE(),
                    UpdatedDate DATETIME DEFAULT GETDATE(),
                    FOREIGN KEY (UserId) REFERENCES Users(UserId)
                )");

            ExecuteNonQuery(connection, @"
                IF OBJECT_ID('OrderDetails', 'U') IS NULL
                CREATE TABLE OrderDetails (
                    OrderDetailId INT IDENTITY(1,1) PRIMARY KEY,
                    OrderId INT NOT NULL,
                    ProductId INT NOT NULL,
                    ProductName NVARCHAR(200) NOT NULL,
                    ProductCode NVARCHAR(50) DEFAULT '',
                    Quantity INT NOT NULL,
                    UnitPrice DECIMAL(10,2) NOT NULL,
                    DiscountAmount DECIMAL(10,2) DEFAULT 0,
                    TotalPrice DECIMAL(10,2) NOT NULL,
                    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
                    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
                )");

            ExecuteNonQuery(connection, @"
                IF OBJECT_ID('OrderHistory', 'U') IS NULL
                CREATE TABLE OrderHistory (
                    HistoryId INT IDENTITY(1,1) PRIMARY KEY,
                    OrderId INT NOT NULL,
                    Status NVARCHAR(50),
                    PaymentStatus NVARCHAR(50),
                    UpdateNotes NVARCHAR(500),
                    UpdateDate DATETIME DEFAULT GETDATE(),
                    UpdatedBy NVARCHAR(100),
                    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId)
                )");

            ExecuteNonQuery(connection, @"
                IF OBJECT_ID('Wishlist', 'U') IS NULL
                CREATE TABLE Wishlist (
                    WishlistId INT IDENTITY(1,1) PRIMARY KEY,
                    UserId INT NOT NULL,
                    ProductId INT NOT NULL,
                    AddedDate DATETIME DEFAULT GETDATE(),
                    FOREIGN KEY (UserId) REFERENCES Users(UserId),
                    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
                )");

            ExecuteNonQuery(connection, @"
                IF OBJECT_ID('CurrencyRates', 'U') IS NULL
                CREATE TABLE CurrencyRates (
                    CurrencyCode NVARCHAR(10) PRIMARY KEY,
                    RateToETB DECIMAL(18,4) NOT NULL,
                    Symbol NVARCHAR(10) DEFAULT '',
                    LastUpdated DATETIME DEFAULT GETDATE()
                )");

            ExecuteNonQuery(connection, @"
                IF OBJECT_ID('Notifications', 'U') IS NULL
                CREATE TABLE Notifications (
                    NotificationId INT IDENTITY(1,1) PRIMARY KEY,
                    UserId INT NULL,
                    Type NVARCHAR(50) DEFAULT 'General',
                    Message NVARCHAR(1000) NOT NULL,
                    IsRead BIT DEFAULT 0,
                    CreatedDate DATETIME DEFAULT GETDATE()
                )");

            ExecuteNonQuery(connection, @"
                IF OBJECT_ID('Reviews', 'U') IS NULL
                CREATE TABLE Reviews (
                    ReviewId INT IDENTITY(1,1) PRIMARY KEY,
                    ProductId INT NOT NULL,
                    UserId INT NOT NULL,
                    OrderId INT NULL,
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
                )");

            ExecuteNonQuery(connection, @"
                IF OBJECT_ID('Announcements', 'U') IS NULL
                CREATE TABLE Announcements (
                    AnnouncementId INT IDENTITY(1,1) PRIMARY KEY,
                    Title NVARCHAR(200) NOT NULL,
                    Content NVARCHAR(MAX) NOT NULL,
                    IsActive BIT DEFAULT 1,
                    CreatedBy INT NULL,
                    CreatedDate DATETIME DEFAULT GETDATE()
                )");

            ExecuteNonQuery(connection, @"
                IF OBJECT_ID('Complaints', 'U') IS NULL
                CREATE TABLE Complaints (
                    ComplaintId INT IDENTITY(1,1) PRIMARY KEY,
                    UserId INT NOT NULL,
                    Subject NVARCHAR(200) NOT NULL,
                    Message NVARCHAR(MAX) NOT NULL,
                    Status NVARCHAR(50) DEFAULT 'Pending',
                    CreatedDate DATETIME DEFAULT GETDATE(),
                    FOREIGN KEY (UserId) REFERENCES Users(UserId)
                )");

        }

        private static void MigrateSchema(SqlConnection connection)
        {
            string[] columns = {
                "IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Orders') AND name = 'PaymentReference') ALTER TABLE Orders ADD PaymentReference NVARCHAR(100) DEFAULT '';",
                "IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Orders') AND name = 'Phone') ALTER TABLE Orders ADD Phone NVARCHAR(20) DEFAULT '';",
                "IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Orders') AND name = 'CurrencyCode') ALTER TABLE Orders ADD CurrencyCode NVARCHAR(10) DEFAULT 'ETB';",
                "IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Orders') AND name = 'PaymentProofImage') ALTER TABLE Orders ADD PaymentProofImage NVARCHAR(500) DEFAULT '';",
                "IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Products') AND name = 'Model') ALTER TABLE Products ADD Model NVARCHAR(100) DEFAULT '';",
                "IF OBJECT_ID('Announcements', 'U') IS NULL CREATE TABLE Announcements (AnnouncementId INT IDENTITY(1,1) PRIMARY KEY, Title NVARCHAR(200) NOT NULL, Content NVARCHAR(MAX) NOT NULL, IsActive BIT DEFAULT 1, CreatedBy INT NULL, CreatedDate DATETIME DEFAULT GETDATE());",
                "IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Announcements') AND name = 'CreatedBy') ALTER TABLE Announcements ADD CreatedBy INT NULL;",
                "IF OBJECT_ID('Complaints', 'U') IS NULL CREATE TABLE Complaints (ComplaintId INT IDENTITY(1,1) PRIMARY KEY, UserId INT NOT NULL, Subject NVARCHAR(200) NOT NULL, Message NVARCHAR(MAX) NOT NULL, Status NVARCHAR(50) DEFAULT 'Pending', CreatedDate DATETIME DEFAULT GETDATE(), FOREIGN KEY (UserId) REFERENCES Users(UserId));",
                "IF OBJECT_ID('Reviews', 'U') IS NULL CREATE TABLE Reviews (ReviewId INT IDENTITY(1,1) PRIMARY KEY, ProductId INT NOT NULL, UserId INT NOT NULL, OrderId INT NULL, Rating INT NOT NULL CHECK (Rating >= 1 AND Rating <= 5), Title NVARCHAR(200), Comment NVARCHAR(1000), IsVerifiedPurchase BIT DEFAULT 0, IsApproved BIT DEFAULT 0, HelpfulCount INT DEFAULT 0, CreatedDate DATETIME DEFAULT GETDATE());",
                "IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Reviews') AND name = 'IsApproved') ALTER TABLE Reviews ADD IsApproved BIT DEFAULT 0;",
                "IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'PreferredCurrency') ALTER TABLE Users ADD PreferredCurrency NVARCHAR(10) DEFAULT 'ETB';",
                "IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Products') AND name = 'DiscountExpiry') ALTER TABLE Products ADD DiscountExpiry DATETIME NULL;",
                "IF OBJECT_ID('GetDashboardStats', 'P') IS NOT NULL DROP PROCEDURE GetDashboardStats;",
                "EXEC('CREATE PROCEDURE GetDashboardStats AS BEGIN SELECT (SELECT COUNT(*) FROM Users WHERE IsActive = 1) AS TotalCustomers, (SELECT COUNT(*) FROM Products WHERE IsActive = 1) AS TotalProducts, (SELECT COUNT(*) FROM Orders) AS TotalOrders, (SELECT ISNULL(SUM(TotalAmount), 0) FROM Orders WHERE Status = ''Delivered'' OR Status = ''Completed'') AS TotalRevenue, (SELECT COUNT(*) FROM Orders WHERE Status = ''Delivered'' OR Status = ''Completed'') AS DeliveredOrders, (SELECT COUNT(*) FROM Orders WHERE Status = ''Cancelled'') AS CancelledOrders, (SELECT COUNT(*) FROM Orders WHERE Status = ''Pending'') AS PendingOrders, (SELECT COUNT(*) FROM Reviews WHERE IsApproved = 0) AS PendingReviews, (SELECT COUNT(*) FROM Complaints WHERE Status = ''Pending'') AS PendingComplaints END');",
                "UPDATE Orders SET Status = 'Delivered' WHERE Status = 'Completed';",
                "UPDATE Products SET MainImagePath = 'Resources\\Products\\laptop.png', IsFeatured = 1 WHERE ProductCode = 'LAPTOP001';",
                "UPDATE Products SET MainImagePath = 'Resources\\Products\\smartphone.png', IsFeatured = 1 WHERE ProductCode = 'PHONE001';",
                "UPDATE Products SET MainImagePath = 'Resources\\Products\\shirt.png', IsFeatured = 1 WHERE ProductCode IN ('SHIRT001', 'JEANS001', 'SNEAKERS001');",
                "UPDATE Products SET MainImagePath = 'Resources\\Products\\book.png', IsFeatured = 1 WHERE ProductCode = 'BOOK001';",
                "UPDATE Products SET MainImagePath = 'Resources\\Products\\laptop.png' WHERE CategoryId = 2 AND MainImagePath = '';",
                "UPDATE Products SET MainImagePath = 'Resources\\Products\\shirt.png' WHERE CategoryId IN (4, 5, 6) AND MainImagePath = '';",
                "UPDATE Products SET MainImagePath = 'Resources\\Products\\book.png' WHERE CategoryId = 7 AND MainImagePath = '';",
                "UPDATE Products SET MainImagePath = 'Resources\\Products\\smartphone.png' WHERE CategoryId = 3 OR Name LIKE '%Phone%' OR Name LIKE '%Mobile%';",
                "IF NOT EXISTS (SELECT * FROM CurrencyRates WHERE CurrencyCode = 'GBP') INSERT INTO CurrencyRates (CurrencyCode, RateToETB, Symbol) VALUES ('GBP', 72.50, '£');",
                "IF NOT EXISTS (SELECT * FROM CurrencyRates WHERE CurrencyCode = 'JPY') INSERT INTO CurrencyRates (CurrencyCode, RateToETB, Symbol) VALUES ('JPY', 0.38, '¥');",
                "IF NOT EXISTS (SELECT * FROM CurrencyRates WHERE CurrencyCode = 'CNY') INSERT INTO CurrencyRates (CurrencyCode, RateToETB, Symbol) VALUES ('CNY', 7.85, '¥');",
                "IF NOT EXISTS (SELECT * FROM CurrencyRates WHERE CurrencyCode = 'INR') INSERT INTO CurrencyRates (CurrencyCode, RateToETB, Symbol) VALUES ('INR', 0.68, '₹');",
                "IF NOT EXISTS (SELECT * FROM CurrencyRates WHERE CurrencyCode = 'CAD') INSERT INTO CurrencyRates (CurrencyCode, RateToETB, Symbol) VALUES ('CAD', 41.20, 'C$');",
                "IF NOT EXISTS (SELECT * FROM CurrencyRates WHERE CurrencyCode = 'AUD') INSERT INTO CurrencyRates (CurrencyCode, RateToETB, Symbol) VALUES ('AUD', 37.40, 'A$');",
                "IF NOT EXISTS (SELECT * FROM CurrencyRates WHERE CurrencyCode = 'SAR') INSERT INTO CurrencyRates (CurrencyCode, RateToETB, Symbol) VALUES ('SAR', 15.10, '﷼');",
                "IF NOT EXISTS (SELECT * FROM CurrencyRates WHERE CurrencyCode = 'AED') INSERT INTO CurrencyRates (CurrencyCode, RateToETB, Symbol) VALUES ('AED', 15.40, 'د.إ');",
                "IF NOT EXISTS (SELECT * FROM CurrencyRates WHERE CurrencyCode = 'ZAR') INSERT INTO CurrencyRates (CurrencyCode, RateToETB, Symbol) VALUES ('ZAR', 3.05, 'R');",
                "IF NOT EXISTS (SELECT * FROM CurrencyRates WHERE CurrencyCode = 'RUB') INSERT INTO CurrencyRates (CurrencyCode, RateToETB, Symbol) VALUES ('RUB', 0.62, '₽');",
                "IF NOT EXISTS (SELECT * FROM CurrencyRates WHERE CurrencyCode = 'KRW') INSERT INTO CurrencyRates (CurrencyCode, RateToETB, Symbol) VALUES ('KRW', 0.043, '₩');",
                "IF NOT EXISTS (SELECT * FROM CurrencyRates WHERE CurrencyCode = 'TRY') INSERT INTO CurrencyRates (CurrencyCode, RateToETB, Symbol) VALUES ('TRY', 1.85, '₺');"
            };
            foreach (var sql in columns) ExecuteNonQuery(connection, sql);
        }

        private static void SeedData(SqlConnection connection)
        {
            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM Admins", connection))
            {
                if ((int)cmd.ExecuteScalar() == 0)
                {
                    var hashResult = SecurityHelper.HashPassword("admin123");
                    ExecuteNonQuery(connection, $@"INSERT INTO Admins (Username, PasswordHash, Salt, Email, FullName, Role, Permissions) 
                                                  VALUES ('admin', '{hashResult.Item1}', '{hashResult.Item2}', 'admin@ecommerce.com', 'Administrator', 'SuperAdmin', 'ALL')");
                }
            }
        }

        private static void RegisterStoredProcedures(SqlConnection connection)
        {
            try
            {
                // Try multiple possible paths to find the SQL file
                string[] possiblePaths = {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Database", "StoredProcedures.sql"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "StoredProcedures.sql"),
                    Path.Combine(Directory.GetCurrentDirectory(), "Database", "StoredProcedures.sql")
                };

                string scriptPath = "";
                foreach (var path in possiblePaths)
                {
                    if (File.Exists(path)) { scriptPath = path; break; }
                }

                if (string.IsNullOrEmpty(scriptPath))
                {
                    Console.WriteLine("Warning: StoredProcedures.sql not found.");
                    return;
                }

                string script = File.ReadAllText(scriptPath);
                // Split by GO but handle case sensitivity and whitespace
                string[] commands = System.Text.RegularExpressions.Regex.Split(script, @"^\s*GO\s*$", System.Text.RegularExpressions.RegexOptions.Multiline | System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                
                foreach (string cmdText in commands)
                {
                    if (string.IsNullOrWhiteSpace(cmdText)) continue;
                    try
                    {
                        using (var cmd = new SqlCommand(cmdText, connection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log specific SP error but continue
                        Console.WriteLine($"Error in SP command: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error registering stored procedures: {ex.Message}", "Database Setup Warning");
            }
        }

        private static void ExecuteNonQuery(SqlConnection connection, string query)
        {
            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static SqlCommand CreateStoredProcedureCommand(string spName, SqlConnection connection)
        {
            var cmd = new SqlCommand(spName, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        public static string GetConnectionString()
        {
            return connectionString;
        }
    }
}