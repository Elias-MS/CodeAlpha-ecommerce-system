# Database Schema - E-Commerce Store

## Entity Relationship Diagram (ERD)

```
┌─────────────────┐
│      User       │
│  (Django Auth)  │
├─────────────────┤
│ id (PK)         │
│ username        │
│ email           │
│ password        │
│ first_name      │
│ last_name       │
│ is_staff        │
│ is_active       │
│ date_joined     │
└────────┬────────┘
         │
         │ 1:1
         │
┌────────▼────────┐
│  UserProfile    │
├─────────────────┤
│ id (PK)         │
│ user_id (FK)    │
│ phone           │
│ address         │
│ city            │
│ state           │
│ zipcode         │
│ created_at      │
└─────────────────┘

┌─────────────────┐
│    Category     │
├─────────────────┤
│ id (PK)         │
│ name            │
│ description     │
│ created_at      │
└────────┬────────┘
         │
         │ 1:N
         │
┌────────▼────────┐
│    Product      │
├─────────────────┤
│ id (PK)         │
│ name            │
│ description     │
│ price           │
│ category_id(FK) │
│ image           │
│ stock           │
│ rating          │
│ created_at      │
│ updated_at      │
└────────┬────────┘
         │
         │ 1:N
         │
┌────────▼────────┐
│ ProductReview   │
├─────────────────┤
│ id (PK)         │
│ product_id (FK) │
│ user_id (FK)    │
│ rating          │
│ comment         │
│ created_at      │
└─────────────────┘

┌─────────────────┐
│      User       │
└────────┬────────┘
         │
         │ 1:1
         │
┌────────▼────────┐
│      Cart       │
├─────────────────┤
│ id (PK)         │
│ user_id (FK)    │
│ created_at      │
│ updated_at      │
└────────┬────────┘
         │
         │ 1:N
         │
┌────────▼────────┐
│    CartItem     │
├─────────────────┤
│ id (PK)         │
│ cart_id (FK)    │
│ product_id (FK) │
│ quantity        │
│ added_at        │
└─────────────────┘

┌─────────────────┐
│      User       │
└────────┬────────┘
         │
         │ 1:N
         │
┌────────▼────────┐
│     Order       │
├─────────────────┤
│ id (PK)         │
│ order_id (UK)   │
│ user_id (FK)    │
│ total_price     │
│ full_name       │
│ email           │
│ phone           │
│ shipping_addr   │
│ city            │
│ state           │
│ zipcode         │
│ payment_method  │
│ payment_status  │
│ order_status    │
│ created_at      │
│ updated_at      │
└────────┬────────┘
         │
         │ 1:N
         │
┌────────▼────────┐
│   OrderItem     │
├─────────────────┤
│ id (PK)         │
│ order_id (FK)   │
│ product_id (FK) │
│ quantity        │
│ price           │
└─────────────────┘
```

## Table Descriptions

### 1. User (Django's Built-in Auth User)

**Purpose:** Stores user authentication and basic information

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| id | Integer | PRIMARY KEY, AUTO_INCREMENT | Unique user identifier |
| username | VARCHAR(150) | UNIQUE, NOT NULL | User's login name |
| email | VARCHAR(254) | NOT NULL | User's email address |
| password | VARCHAR(128) | NOT NULL | Hashed password |
| first_name | VARCHAR(150) | NULL | User's first name |
| last_name | VARCHAR(150) | NULL | User's last name |
| is_staff | Boolean | DEFAULT FALSE | Admin access flag |
| is_active | Boolean | DEFAULT TRUE | Account active status |
| date_joined | DateTime | NOT NULL | Registration timestamp |

**Relationships:**
- One-to-One with UserProfile
- One-to-Many with ProductReview
- One-to-One with Cart
- One-to-Many with Order

---

### 2. UserProfile

**Purpose:** Extended user information for shipping and contact

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| id | Integer | PRIMARY KEY, AUTO_INCREMENT | Unique profile identifier |
| user_id | Integer | FOREIGN KEY (User), UNIQUE | Reference to User |
| phone | VARCHAR(15) | NULL | Contact phone number |
| address | Text | NULL | Street address |
| city | VARCHAR(100) | NULL | City name |
| state | VARCHAR(100) | NULL | State/Province |
| zipcode | VARCHAR(10) | NULL | Postal code |
| created_at | DateTime | NOT NULL | Profile creation time |

**Relationships:**
- Many-to-One with User (user_id)

**Indexes:**
- user_id (UNIQUE)

---

### 3. Category

**Purpose:** Product categorization

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| id | Integer | PRIMARY KEY, AUTO_INCREMENT | Unique category identifier |
| name | VARCHAR(100) | UNIQUE, NOT NULL | Category name |
| description | Text | NULL | Category description |
| created_at | DateTime | NOT NULL | Creation timestamp |

**Relationships:**
- One-to-Many with Product

**Indexes:**
- name (UNIQUE)

---

### 4. Product

**Purpose:** Store product information

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| id | Integer | PRIMARY KEY, AUTO_INCREMENT | Unique product identifier |
| name | VARCHAR(200) | NOT NULL | Product name |
| description | Text | NOT NULL | Product description |
| price | Decimal(10,2) | NOT NULL | Product price |
| category_id | Integer | FOREIGN KEY (Category) | Product category |
| image | VARCHAR(100) | DEFAULT 'products/default.jpg' | Image path |
| stock | Integer | DEFAULT 0 | Available quantity |
| rating | Decimal(3,2) | DEFAULT 0.00 | Average rating |
| created_at | DateTime | NOT NULL | Creation timestamp |
| updated_at | DateTime | NOT NULL | Last update timestamp |

**Relationships:**
- Many-to-One with Category (category_id)
- One-to-Many with ProductReview
- One-to-Many with CartItem
- One-to-Many with OrderItem

**Indexes:**
- category_id
- created_at

**Computed Properties:**
- is_available: Returns True if stock > 0

---

### 5. ProductReview

**Purpose:** Store customer product reviews

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| id | Integer | PRIMARY KEY, AUTO_INCREMENT | Unique review identifier |
| product_id | Integer | FOREIGN KEY (Product) | Reviewed product |
| user_id | Integer | FOREIGN KEY (User) | Reviewer |
| rating | Integer | CHECK (1-5) | Star rating |
| comment | Text | NOT NULL | Review text |
| created_at | DateTime | NOT NULL | Review timestamp |

**Relationships:**
- Many-to-One with Product (product_id)
- Many-to-One with User (user_id)

**Constraints:**
- UNIQUE (product_id, user_id) - One review per user per product

**Indexes:**
- product_id
- user_id
- created_at

---

### 6. Cart

**Purpose:** Shopping cart for each user

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| id | Integer | PRIMARY KEY, AUTO_INCREMENT | Unique cart identifier |
| user_id | Integer | FOREIGN KEY (User), UNIQUE | Cart owner |
| created_at | DateTime | NOT NULL | Cart creation time |
| updated_at | DateTime | NOT NULL | Last update time |

**Relationships:**
- Many-to-One with User (user_id)
- One-to-Many with CartItem

**Indexes:**
- user_id (UNIQUE)

**Computed Properties:**
- total_price: Sum of all cart items subtotals
- total_items: Sum of all cart items quantities

---

### 7. CartItem

**Purpose:** Individual items in shopping cart

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| id | Integer | PRIMARY KEY, AUTO_INCREMENT | Unique cart item identifier |
| cart_id | Integer | FOREIGN KEY (Cart) | Parent cart |
| product_id | Integer | FOREIGN KEY (Product) | Product in cart |
| quantity | Integer | DEFAULT 1, CHECK (>0) | Item quantity |
| added_at | DateTime | NOT NULL | Addition timestamp |

**Relationships:**
- Many-to-One with Cart (cart_id)
- Many-to-One with Product (product_id)

**Constraints:**
- UNIQUE (cart_id, product_id) - One entry per product per cart

**Indexes:**
- cart_id
- product_id

**Computed Properties:**
- subtotal: product.price * quantity

---

### 8. Order

**Purpose:** Customer orders

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| id | Integer | PRIMARY KEY, AUTO_INCREMENT | Unique order identifier |
| order_id | VARCHAR(100) | UNIQUE, NOT NULL | Human-readable order ID |
| user_id | Integer | FOREIGN KEY (User) | Customer |
| total_price | Decimal(10,2) | NOT NULL | Order total |
| full_name | VARCHAR(200) | NOT NULL | Recipient name |
| email | VARCHAR(254) | NOT NULL | Contact email |
| phone | VARCHAR(15) | NOT NULL | Contact phone |
| shipping_address | Text | NOT NULL | Delivery address |
| city | VARCHAR(100) | NOT NULL | City |
| state | VARCHAR(100) | NOT NULL | State/Province |
| zipcode | VARCHAR(10) | NOT NULL | Postal code |
| payment_method | VARCHAR(20) | NOT NULL | Payment type |
| payment_status | Boolean | DEFAULT FALSE | Payment received |
| order_status | VARCHAR(20) | DEFAULT 'pending' | Order status |
| created_at | DateTime | NOT NULL | Order timestamp |
| updated_at | DateTime | NOT NULL | Last update |

**Relationships:**
- Many-to-One with User (user_id)
- One-to-Many with OrderItem

**Indexes:**
- order_id (UNIQUE)
- user_id
- created_at
- order_status

**Enums:**
- payment_method: 'cod', 'card', 'upi', 'netbanking'
- order_status: 'pending', 'processing', 'shipped', 'delivered', 'cancelled'

---

### 9. OrderItem

**Purpose:** Products in an order

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| id | Integer | PRIMARY KEY, AUTO_INCREMENT | Unique order item identifier |
| order_id | Integer | FOREIGN KEY (Order) | Parent order |
| product_id | Integer | FOREIGN KEY (Product) | Ordered product |
| quantity | Integer | NOT NULL, CHECK (>0) | Quantity ordered |
| price | Decimal(10,2) | NOT NULL | Price at order time |

**Relationships:**
- Many-to-One with Order (order_id)
- Many-to-One with Product (product_id)

**Indexes:**
- order_id
- product_id

**Computed Properties:**
- subtotal: price * quantity

---

## Relationships Summary

### One-to-One Relationships
1. User ↔ UserProfile
2. User ↔ Cart

### One-to-Many Relationships
1. Category → Product
2. Product → ProductReview
3. Product → CartItem
4. Product → OrderItem
5. User → ProductReview
6. User → Order
7. Cart → CartItem
8. Order → OrderItem

### Many-to-Many Relationships
None (handled through intermediate tables)

---

## Database Constraints

### Foreign Key Constraints
- All foreign keys use CASCADE on delete for related records
- Ensures referential integrity

### Unique Constraints
- User.username
- User.email
- Category.name
- Order.order_id
- UserProfile.user_id
- Cart.user_id
- (ProductReview.product_id, ProductReview.user_id)
- (CartItem.cart_id, CartItem.product_id)

### Check Constraints
- Product.stock >= 0
- Product.rating >= 0 AND rating <= 5
- ProductReview.rating >= 1 AND rating <= 5
- CartItem.quantity > 0
- OrderItem.quantity > 0

---

## Indexes for Performance

### Primary Indexes (Automatic)
- All id columns

### Foreign Key Indexes (Automatic)
- All foreign key columns

### Custom Indexes (Recommended)
```sql
CREATE INDEX idx_product_category ON Product(category_id);
CREATE INDEX idx_product_created ON Product(created_at);
CREATE INDEX idx_order_user ON Order(user_id);
CREATE INDEX idx_order_status ON Order(order_status);
CREATE INDEX idx_order_created ON Order(created_at);
```

---

## Sample Queries

### Get all products in a category
```sql
SELECT * FROM Product 
WHERE category_id = 1 
ORDER BY created_at DESC;
```

### Get user's cart with items
```sql
SELECT ci.*, p.name, p.price 
FROM CartItem ci
JOIN Cart c ON ci.cart_id = c.id
JOIN Product p ON ci.product_id = p.id
WHERE c.user_id = 1;
```

### Get order details with items
```sql
SELECT o.*, oi.*, p.name 
FROM Order o
JOIN OrderItem oi ON oi.order_id = o.id
JOIN Product p ON oi.product_id = p.id
WHERE o.order_id = 'ORD-12345678';
```

### Get user's order history
```sql
SELECT * FROM Order 
WHERE user_id = 1 
ORDER BY created_at DESC;
```

---

## Data Integrity Rules

1. **User Registration:**
   - Username must be unique
   - Email must be unique
   - Password must be hashed

2. **Product Management:**
   - Stock cannot be negative
   - Price must be positive
   - Must belong to a category

3. **Cart Operations:**
   - One cart per user
   - Cannot add more than available stock
   - Quantity must be positive

4. **Order Processing:**
   - Must have at least one item
   - Stock is reduced when order is placed
   - Order ID is auto-generated and unique

5. **Reviews:**
   - One review per user per product
   - Rating must be 1-5
   - User must be authenticated

---

**Database Schema Version:** 1.0
**Last Updated:** May 29, 2026
