# E-Commerce Speed Management System

## Overview
**E-Commerce Speed** is a modern, feature-rich desktop application built with C# and Windows Forms (WinForms). It serves as a comprehensive e-commerce platform featuring both a highly interactive **Customer Dashboard** for shopping and a powerful **Admin Dashboard** for complete store management. The application uses a SQL Server database for robust data persistence.

## Key Features

### 🛒 Customer Experience
*   **Modern Shopping Interface**: A dynamic, visually appealing product catalog with real-time category filtering and searching.
*   **Global Multi-Currency Support**: Customers can select their preferred currency (e.g., ETB, USD, EUR, GBP) from the top navigation. Prices dynamically convert based on the latest exchange rates.
*   **Shopping Cart & Checkout**: Add single or multiple items to the cart. Proceed to checkout, enter shipping details, and upload payment proof (receipt/screenshot) to finalize orders.
*   **Wishlist System**: Save favorite products to a personal wishlist for future purchases.
*   **Order History**: View past orders and track current order statuses (Pending, Processing, Delivered, Cancelled).
*   **User Profiles**: Manage personal information, shipping addresses, and default currency preferences.
*   **Support & Alerts**: Integrated messaging system allowing users to send complaints or questions directly to administrators, and view global store announcements.

### ⚙️ Administrator Controls (Admin Dashboard Pro)
*   **Real-Time Analytics Dashboard**: Visual overview of total revenue, orders, customers, pending orders, and alerts.
*   **Product Management (CRUD)**: Add, edit, delete, or hide products. Features stock tracking and automated low-stock highlighting. 
*   **Order Management**: Review customer orders, update statuses, and verify uploaded payment proofs through a built-in image gallery. Ability to approve or reject transactions.
*   **Customer Management**: View the registered user base and manually activate or deactivate accounts to control access.
*   **Dynamic Currency Management**: Admins can seamlessly add new global currencies (Code, Rate to ETB, and Symbol) directly from the dashboard, which instantly updates across the entire system.
*   **Sales Reports & Exporting**: Generate daily, weekly, monthly, or annual sales reports. Easily export financial data to CSV for external accounting.
*   **Alert Center**: Directly respond to user queries or complaints, resolving them from a dedicated messaging interface.
*   **News & Announcements**: Broadcast global announcements that scroll across the customer dashboard ticker.

## System Architecture
*   **Frontend**: C# Windows Forms using custom, flat-style modern UI components, gradient panels, and responsive grid layouts.
*   **Backend**: C# Object-Oriented design implementing the Service Layer pattern (e.g., `ProductService`, `OrderService`, `CurrencyService`, `UserService`).
*   **Database**: SQL Server. Features structured tables (`Users`, `Products`, `Orders`, `OrderDetails`, `CurrencyRates`, `Categories`, `Complaints`, `Announcements`) and stored procedures for complex dashboard aggregations.
*   **Security**: SHA-256 password hashing with unique per-user salt generation. Secure authentication flows.

## Core Modules

1.  **FormMain**: The public landing page showcasing featured products, value propositions, and navigation to Login/Register.
2.  **FormCustomerDashboard**: The central hub for authenticated users (and guests) to browse the catalog, filter products, and manage their session.
3.  **FormAdminDashboard**: A heavy-duty, tabbed interface giving store owners absolute control over the platform's data and operations.
4.  **FormCart & FormCheckout**: Secure transaction handling, integrating local file uploads for payment verification.
5.  **CurrencyService**: Handles fetching database rates, live conversion mathematics, and synchronizing dropdown menus system-wide.

## Setup Instructions
1.  **Database Configuration**: Execute the provided SQL scripts (e.g., `AdvancedECommerceDB.sql` and `StoredProcedures.sql`) in SQL Server Management Studio to generate the schema and seed initial data.
2.  **Connection String**: Ensure the `DatabaseHelper.cs` file points to your local SQL Server instance.
3.  **Dependencies**: The project relies on standard .NET Framework / .NET Core WinForms libraries. No external third-party UI packages are required; all styling is built natively.
4.  **Run**: Build the solution in Visual Studio and start the application. Use default admin credentials (if seeded) to access the Admin Dashboard.

---
*Built to deliver a premium, scalable, and responsive desktop e-commerce experience.*
