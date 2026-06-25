# Database Connection Information 🗄️

## Database Type
**SQLite3** - Lightweight, file-based database

## Database Location
📁 **File Path**: `c:\xampp\htdocs\Simple E-commerce Store\db.sqlite3`

## Database Configuration (from settings.py)
```python
DATABASES = {
    'default': {
        'ENGINE': 'django.db.backends.sqlite3',
        'NAME': BASE_DIR / 'db.sqlite3',
    }
}
```

---

## 📊 How to Connect to the Database

### Method 1: DB Browser for SQLite (Recommended - Free & Easy)

**Download**: https://sqlitebrowser.org/dl/

**Steps:**
1. Download and install "DB Browser for SQLite"
2. Open the application
3. Click **"Open Database"**
4. Navigate to: `c:\xampp\htdocs\Simple E-commerce Store\db.sqlite3`
5. Click **"Open"**

**✅ You can now:**
- View all tables
- Browse data
- Execute SQL queries
- Export data
- Edit records

---

### Method 2: SQLite Command Line

**If you have sqlite3 installed:**
```bash
sqlite3 "c:\xampp\htdocs\Simple E-commerce Store\db.sqlite3"
```

**Common Commands:**
```sql
-- List all tables
.tables

-- View table structure
.schema products_product

-- Query data
SELECT * FROM products_product;

-- Exit
.quit
```

---

### Method 3: Python Script

**Create a file** `connect_db.py`:
```python
import sqlite3

# Connect to database
conn = sqlite3.connect('db.sqlite3')
cursor = conn.cursor()

# List all tables
cursor.execute("SELECT name FROM sqlite_master WHERE type='table';")
tables = cursor.fetchall()
print("Tables in database:")
for table in tables:
    print(f"  - {table[0]}")

# Query products
cursor.execute("SELECT id, name, price, stock FROM products_product LIMIT 10;")
products = cursor.fetchall()
print("\nFirst 10 products:")
for product in products:
    print(f"  ID: {product[0]}, Name: {product[1]}, Price: ${product[2]}, Stock: {product[3]}")

conn.close()
```

**Run it:**
```bash
cd "c:\xampp\htdocs\Simple E-commerce Store"
python connect_db.py
```

---

### Method 4: Django Shell

**Open Django shell:**
```bash
cd "c:\xampp\htdocs\Simple E-commerce Store"
python manage.py shell
```

**Query data using Django ORM:**
```python
from products.models import Product, Category
from users.models import User, Complaint
from orders.models import Order, OrderItem

# Get all products
products = Product.objects.all()
print(f"Total products: {products.count()}")

# Get specific product
product = Product.objects.get(id=143)
print(f"Product: {product.name}, Price: ${product.price}")

# Get all orders
orders = Order.objects.all()
print(f"Total orders: {orders.count()}")

# Get all users
users = User.objects.all()
print(f"Total users: {users.count()}")

# Get complaints
complaints = Complaint.objects.all()
print(f"Total complaints: {complaints.count()}")
```

---

## 📋 Database Tables

### Products App
- `products_category` - Product categories
- `products_product` - All products (143 items)
- `products_productreview` - Product reviews

### Users App
- `auth_user` - User accounts
- `users_userprofile` - User profiles
- `users_complaint` - Customer complaints
- `users_announcement` - Announcements
- `users_notification` - User notifications
- `users_contactmessage` - Contact messages

### Orders App
- `orders_order` - Customer orders
- `orders_orderitem` - Order line items

### Cart App
- `cart_cart` - Shopping carts
- `cart_cartitem` - Cart items

---

## 🔍 Useful SQL Queries

### View All Products
```sql
SELECT id, name, category_id, price, stock, rating 
FROM products_product 
ORDER BY created_at DESC 
LIMIT 20;
```

### View Products with Category Names
```sql
SELECT 
    p.id,
    p.name,
    c.name as category,
    p.price,
    p.stock,
    p.rating
FROM products_product p
JOIN products_category c ON p.category_id = c.id
ORDER BY p.name;
```

### View All Orders
```sql
SELECT 
    o.id,
    u.username,
    o.total_price,
    o.order_status,
    o.created_at
FROM orders_order o
JOIN auth_user u ON o.user_id = u.id
ORDER BY o.created_at DESC;
```

### View Complaints
```sql
SELECT 
    c.id,
    u.username,
    c.subject,
    c.status,
    c.is_urgent,
    c.created_at
FROM users_complaint c
JOIN auth_user u ON c.user_id = u.id
ORDER BY c.created_at DESC;
```

### Count Statistics
```sql
SELECT 
    (SELECT COUNT(*) FROM products_product) as total_products,
    (SELECT COUNT(*) FROM auth_user) as total_users,
    (SELECT COUNT(*) FROM orders_order) as total_orders,
    (SELECT COUNT(*) FROM users_complaint) as total_complaints;
```

---

## 🛠️ Database Tools Comparison

| Tool | Type | Best For | Free |
|------|------|----------|------|
| **DB Browser for SQLite** | GUI | Beginners, Visual editing | ✅ Yes |
| **SQLite CLI** | Command Line | Quick queries, Scripts | ✅ Yes |
| **Django Shell** | Python ORM | Django-specific queries | ✅ Yes |
| **DBeaver** | GUI | Advanced users | ✅ Yes |
| **SQLite Expert** | GUI | Professional use | ⚠️ Paid |

---

## 📥 Backup Database

**Manual Backup:**
```bash
copy "c:\xampp\htdocs\Simple E-commerce Store\db.sqlite3" "c:\xampp\htdocs\Simple E-commerce Store\db.sqlite3.backup"
```

**With timestamp:**
```bash
copy "c:\xampp\htdocs\Simple E-commerce Store\db.sqlite3" "c:\xampp\htdocs\Simple E-commerce Store\db_backup_%date:~-4,4%%date:~-10,2%%date:~-7,2%.sqlite3"
```

---

## 🔒 Security Notes

⚠️ **Important:**
- Never share the `db.sqlite3` file publicly (contains user passwords)
- Always backup before making manual database changes
- Use Django admin or ORM for safe data manipulation
- SQLite is for development - use PostgreSQL/MySQL for production

---

## 📊 Database Statistics

Based on your current database:
- **Total Products**: 143
- **Total Categories**: 12
- **All products have images**: 100% coverage
- **Database File Size**: ~2-5 MB (approximate)

---

## 🆘 Troubleshooting

**Database is locked:**
- Close Django server first
- Close any other programs accessing the database
- Wait a few seconds and try again

**Cannot open database:**
- Check file path is correct
- Ensure you have read/write permissions
- File should be `db.sqlite3` not `db.sqlite3.backup`

**Want to reset database:**
```bash
# Backup first!
copy db.sqlite3 db.sqlite3.backup

# Delete database
del db.sqlite3

# Recreate with migrations
python manage.py migrate

# Create superuser
python manage.py createsuperuser
```

---

## 📞 Quick Access

**Database File:**
```
c:\xampp\htdocs\Simple E-commerce Store\db.sqlite3
```

**Open in Explorer:**
- Press `Windows + E`
- Navigate to: `c:\xampp\htdocs\Simple E-commerce Store`
- Find `db.sqlite3` file

**Size Check:**
```bash
dir "c:\xampp\htdocs\Simple E-commerce Store\db.sqlite3"
```

---

**Ready to connect! Choose your preferred method above and start exploring your database!** 🚀
