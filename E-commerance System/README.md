# E-Commerce System — C# WinForms with SQL Server

## Project Overview
A complete E-Commerce System developed using **C# .NET Framework 4.8**, **Windows Forms**, and **SQL Server**. Built for the **Event Driven Programming** course, demonstrating OOP concepts, event-driven techniques, 3-layer architecture, and ADO.NET database connectivity.

## Features

### Customer Module
- ✅ User registration with input validation
- ✅ Secure login with password hashing (PBKDF2)
- ✅ Browse products with DataGridView
- ✅ Filter products by category
- ✅ Search products by name, description, or brand
- ✅ Add products to shopping cart with quantity
- ✅ Update quantity / remove items from cart
- ✅ Place orders with shipping address
- ✅ View order history with line item details

### Admin Module
- ✅ Admin login (separate admin table)
- ✅ Dashboard with summary cards (Products, Orders, Customers, Revenue)
- ✅ Add new products
- ✅ Edit existing products
- ✅ Delete products (soft delete)
- ✅ View and manage all customer orders
- ✅ Update order status
- ✅ View sales reports by date

### Technical Features
- ✅ 3-Layer Architecture (Presentation → Business Logic → Data Access)
- ✅ ADO.NET with parameterized queries (prevents SQL injection)
- ✅ PBKDF2 password hashing with random salt
- ✅ Transaction-based order processing
- ✅ Event-driven programming throughout all forms
- ✅ Comprehensive input validation and error handling
- ✅ Professional UI with gradient headers and styled grids

## Project Structure

```
E-commerance System/
├── Data/
│   └── DatabaseHelper.cs          # Database connection, table creation, seeding
├── Models/
│   ├── User.cs                    # Customer model
│   ├── Admin.cs                   # Admin model
│   ├── Product.cs                 # Product model
│   ├── Category.cs                # Category model
│   ├── Order.cs                   # Order + OrderDetail models
│   ├── CartItem.cs                # Shopping cart item model
│   ├── Payment.cs                 # Payment model
│   ├── Report.cs                  # Report data models
│   ├── Review.cs                  # Product review model
│   └── Wishlist.cs                # Wishlist model
├── Services/
│   ├── UserService.cs             # User authentication & management
│   ├── AdminService.cs            # Admin authentication & management
│   ├── ProductService.cs          # Product CRUD operations
│   ├── CategoryService.cs         # Category CRUD operations
│   ├── OrderService.cs            # Order processing & management
│   └── ShoppingCartService.cs     # In-memory shopping cart
├── Forms/
│   ├── FormLogin.cs               # Login form (Customer + Admin)
│   ├── FormRegister.cs            # Customer registration form
│   ├── FormCustomerDashboard.cs   # Product browsing & shopping
│   ├── FormCart.cs                # Shopping cart & checkout
│   ├── FormOrderHistory.cs        # Past orders with details
│   ├── FormAdminDashboard.cs      # Admin control panel
│   └── FormProductManage.cs       # Add/Edit product form
├── Utils/
│   └── SecurityHelper.cs          # Password hashing (PBKDF2)
├── Database/
│   ├── CreateECommerceDB.sql      # Database creation script
│   └── AdvancedECommerceDB.sql    # Advanced schema reference
├── App.config                     # Connection string configuration
├── Program.cs                     # Application entry point
└── README.md                      # This file
```

## System Architecture

```
┌─────────────────────────────────────────────────────┐
│              PRESENTATION LAYER (Forms)              │
│  Login → Register                                    │
│       → CustomerDashboard → Cart → OrderHistory      │
│       → AdminDashboard → ProductManage               │
├─────────────────────────────────────────────────────┤
│            BUSINESS LOGIC LAYER (Services)           │
│  UserService  ProductService  OrderService           │
│  AdminService  CategoryService  ShoppingCartService  │
├─────────────────────────────────────────────────────┤
│             DATA ACCESS LAYER (Data)                 │
│  DatabaseHelper (ADO.NET / SqlConnection)            │
├─────────────────────────────────────────────────────┤
│                    DATABASE                          │
│  SQL Server — ECommerceDB                            │
│  Tables: Users, Admins, Categories, Products,        │
│          Cart, Orders, OrderDetails                  │
└─────────────────────────────────────────────────────┘
```

## Database Tables

| Table | Description |
|-------|-------------|
| **Users** | Customer accounts with hashed passwords |
| **Admins** | Admin accounts with roles and permissions |
| **Categories** | Product categories (Electronics, Clothing, etc.) |
| **Products** | Full product catalog with pricing and stock |
| **Cart** | Shopping cart items per user |
| **Orders** | Customer orders with status tracking |
| **OrderDetails** | Individual line items within orders |

## Setup Instructions

### Prerequisites
1. **SQL Server** (Express, Developer, or Standard edition)
2. **Visual Studio** (2017 or later) with .NET Framework 4.8
3. **SQL Server Management Studio** (optional — for manual DB inspection)

### Step 1: Configure Connection String
Open `App.config` and update the connection string:
```xml
<!-- For Windows Authentication -->
<add name="ECommerceDB" 
     connectionString="Server=YOUR_SERVER_NAME;Database=ECommerceDB;Integrated Security=true;" 
     providerName="System.Data.SqlClient" />

<!-- For SQL Server Authentication -->
<add name="ECommerceDB" 
     connectionString="Server=YOUR_SERVER_NAME;Database=ECommerceDB;User Id=YOUR_USER;Password=YOUR_PASSWORD;" 
     providerName="System.Data.SqlClient" />
```

### Step 2: Build and Run
1. Open the solution in Visual Studio
2. Build → Build Solution (Ctrl+Shift+B)
3. Run → Start Debugging (F5)
4. The database is **automatically created** on first run with sample data

### Step 3: Login
| Role | Username | Password |
|------|----------|----------|
| **Admin** | `admin` | `admin123` |
| **Customer** | Register a new account through the app |

## Event-Driven Programming Concepts Used

| Concept | Where Used |
|---------|------------|
| **Button.Click** | Login, Register, Add to Cart, Place Order, CRUD operations |
| **ComboBox.SelectedIndexChanged** | Category filtering on dashboard |
| **DataGridView.SelectionChanged** | Order details loading, product selection |
| **TextBox.KeyDown** | Enter key to submit login/search |
| **LinkLabel.LinkClicked** | Navigation between Login and Register |
| **Form.FormClosed** | Return to login when dashboard closes |
| **Form.Paint** | Custom gradient rendering on headers |

## Technologies Used

- **Framework:** .NET Framework 4.8
- **Language:** C#
- **Database:** SQL Server with ADO.NET
- **UI:** Windows Forms (WinForms)
- **Security:** PBKDF2 password hashing (Rfc2898DeriveBytes)
- **Architecture:** 3-Layer (Presentation, Business Logic, Data Access)

## Academic Project Information

**Course:** Event Driven Programming  
**Institution:** Axum University College of AIT  
**Faculty:** Computing Technology  
**Department:** Information Technology

**Group Members:**
- Elias Shumuye
- Yordanos Zenebe
- Filmon Kahsay

## License
This project is developed for educational purposes as part of the Event Driven Programming course.