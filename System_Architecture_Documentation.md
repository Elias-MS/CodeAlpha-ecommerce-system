# E-Commerce Speed: System Architecture & Design Documentation

This document outlines the high-level architecture, data models, logic flows, and use cases of the **E-Commerce Speed** Management System. 

---

## 1. Entity-Relationship Diagram (ERD)
This diagram illustrates the core database schema and table relationships. The database is relational, centering around `Users`, `Products`, and `Orders`.

```mermaid
erDiagram
    Users ||--o{ Orders : places
    Users ||--o{ Cart : has
    Users ||--o{ Wishlist : saves
    Users ||--o{ Reviews : writes
    
    Categories ||--o{ Products : groups
    Categories ||--o{ Categories : subcategories
    
    Products ||--o{ OrderDetails : included_in
    Products ||--o{ Inventory : tracked_in
    Products ||--o{ Reviews : receives
    Products ||--o{ Cart : added_to
    
    Orders ||--|{ OrderDetails : contains
    Orders ||--o| Payments : paid_via
    
    Users {
        int UserId PK
        string Username
        string Email
        string PasswordHash
        string PreferredCurrency
    }
    Products {
        int ProductId PK
        int CategoryId FK
        string Name
        decimal Price
        int Stock
        bool IsActive
    }
    Categories {
        int CategoryId PK
        int ParentCategoryId FK
        string CategoryName
    }
    Orders {
        int OrderId PK
        int UserId FK
        string OrderNumber
        decimal TotalAmount
        string Status
        string PaymentStatus
    }
    OrderDetails {
        int OrderDetailId PK
        int OrderId FK
        int ProductId FK
        int Quantity
        decimal UnitPrice
        decimal TotalPrice
    }
    Payments {
        int PaymentId PK
        int OrderId FK
        string PaymentMethod
        decimal PaymentAmount
        string PaymentStatus
        string TransactionId
    }
    CurrencyRates {
        string CurrencyCode PK
        decimal RateToETB
        string Symbol
        datetime LastUpdated
    }
```

---

## 2. Class / Entity Diagram
This diagram highlights the primary Object-Oriented C# models that handle business logic before persisting data to the database.

```mermaid
classDiagram
    class User {
        +int UserId
        +string Username
        +string Email
        +string FirstName
        +string LastName
        +string PreferredCurrency
        +bool IsActive
        +string FullName
    }
    
    class Product {
        +int ProductId
        +string ProductCode
        +string Name
        +decimal Price
        +int Stock
        +string CategoryName
        +int CategoryId
        +string MainImagePath
    }
    
    class Order {
        +int OrderId
        +string OrderNumber
        +int UserId
        +decimal TotalAmount
        +string Status
        +string PaymentMethod
        +string PaymentReference
        +DateTime OrderDate
        +List~OrderDetail~ Items
    }

    class OrderDetail {
        +int OrderDetailId
        +int ProductId
        +string ProductName
        +int Quantity
        +decimal UnitPrice
        +decimal TotalPrice
    }
    
    class CurrencyRate {
        +string CurrencyCode
        +decimal RateToETB
        +string Symbol
        +DateTime LastUpdated
    }
    
    Order "1" *-- "many" OrderDetail : contains
    User "1" -- "many" Order : places
```

---

## 3. Sequence Diagram: Checkout Flow
This sequence demonstrates the logic flow when a customer initiates the checkout process and successfully uploads payment evidence.

```mermaid
sequenceDiagram
    actor Customer
    participant FormCart
    participant FormCheckout
    participant ShoppingCartService
    participant OrderService
    participant Database

    Customer->>FormCart: Clicks "Proceed to Checkout"
    FormCart->>FormCheckout: Opens Checkout UI
    FormCheckout->>ShoppingCartService: GetCartItems()
    ShoppingCartService-->>FormCheckout: Returns Cart Items
    
    Customer->>FormCheckout: Fills Shipping Address
    Customer->>FormCheckout: Selects "Bank Transfer"
    Customer->>FormCheckout: Uploads Payment Receipt
    Customer->>FormCheckout: Clicks "Confirm Order"
    
    FormCheckout->>OrderService: CreateOrder(CartItems, ShippingInfo, ReceiptFile)
    
    OrderService->>Database: BEGIN TRANSACTION
    OrderService->>Database: INSERT Order
    OrderService->>Database: INSERT OrderDetails (Multiple)
    OrderService->>Database: UPDATE Products (Deduct Stock)
    OrderService->>Database: COMMIT TRANSACTION
    
    Database-->>OrderService: Success
    OrderService->>ShoppingCartService: ClearCart()
    ShoppingCartService-->>OrderService: Cart Cleared
    
    OrderService-->>FormCheckout: Order Placed Successfully
    FormCheckout-->>Customer: Shows Success Message & Redirects
```

---

## 4. Flow Diagram: Navigation & Routing
This diagram visualizes how users transition between the main Windows Forms based on authentication and roles.

```mermaid
flowchart TD
    Start((App Launch)) --> FormMain
    FormMain --> |"Browse as Guest"| FormCustomerDashboard
    FormMain --> |"Click Login"| FormLogin
    FormMain --> |"Click Register"| FormRegister
    
    FormRegister --> |"Success"| FormLogin
    
    FormLogin --> |"Authenticate User"| AuthService
    AuthService --> |"Role == Admin"| FormAdminDashboard
    AuthService --> |"Role == Customer"| FormCustomerDashboard
    
    subgraph Admin Area
        FormAdminDashboard --> ProductsTab
        FormAdminDashboard --> OrdersTab
        FormAdminDashboard --> UsersTab
        FormAdminDashboard --> ReportsTab
        FormAdminDashboard --> FormAddCurrency
    end
    
    subgraph Customer Area
        FormCustomerDashboard --> FormProductDetail
        FormCustomerDashboard --> FormCart
        FormCart --> FormCheckout
        FormCustomerDashboard --> FormWishlist
        FormCustomerDashboard --> FormOrderHistory
        FormCustomerDashboard --> FormSupportHistory
    end
```

---

## 5. Use Case Diagram
This diagram outlines the distinct capabilities separated by the primary system actors (Guest, Customer, and Administrator).

```mermaid
flowchart LR
    Guest(("Guest User"))
    Customer(("Logged-in Customer"))
    Admin(("Administrator"))
    
    subgraph Store
        Browse[Browse Products]
        Search[Search & Filter]
    end
    
    subgraph Shopping
        Cart[Manage Cart]
        Wishlist[Manage Wishlist]
        Checkout[Checkout & Pay]
        History[View Order History]
        Support[Send Complaints]
    end
    
    subgraph Administration
        ManageProd[CRUD Products]
        ManageOrders[Verify & Fulfill Orders]
        ManageUsers[Enable/Disable Users]
        ManageCurr[Add New Currencies]
        ViewReports[View Analytics & Reports]
    end
    
    Guest --> Browse
    Guest --> Search
    
    Customer --> Browse
    Customer --> Search
    Customer --> Cart
    Customer --> Wishlist
    Customer --> Checkout
    Customer --> History
    Customer --> Support
    
    Admin --> ManageProd
    Admin --> ManageOrders
    Admin --> ManageUsers
    Admin --> ManageCurr
    Admin --> ViewReports
```
