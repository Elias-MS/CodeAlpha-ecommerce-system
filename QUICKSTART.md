# Quick Start Guide - E-Commerce Store

Get your E-Commerce Store up and running in 5 minutes!

## Prerequisites

- Python 3.8+ installed
- pip installed
- Terminal/Command Prompt access

## Quick Installation (5 Steps)

### Step 1: Install Dependencies (1 minute)

```bash
pip install django pillow
```

### Step 2: Setup Database (1 minute)

```bash
python manage.py migrate
```

### Step 3: Create Admin Account (1 minute)

```bash
python manage.py createsuperuser
```

Enter:
- Username: `admin`
- Email: `admin@example.com`
- Password: `admin123` (or your choice)

### Step 4: Start Server (30 seconds)

```bash
python manage.py runserver
```

### Step 5: Access Application (30 seconds)

Open browser and go to:
- **Main Site:** http://127.0.0.1:8000/
- **Admin Panel:** http://127.0.0.1:8000/admin/

## Add Sample Data (Optional - 2 minutes)

### Using Admin Panel

1. Go to http://127.0.0.1:8000/admin/
2. Login with admin credentials
3. Click "Categories" → "Add Category"
4. Add categories:
   - Electronics
   - Clothing
   - Books
   - Home & Kitchen

5. Click "Products" → "Add Product"
6. Add products with details

### Using Django Shell (Faster)

```bash
python manage.py shell
```

Copy and paste:

```python
from products.models import Category, Product
from decimal import Decimal

# Create categories
electronics = Category.objects.create(name="Electronics", description="Electronic devices")
clothing = Category.objects.create(name="Clothing", description="Fashion items")
books = Category.objects.create(name="Books", description="Books and literature")

# Create products
Product.objects.create(
    name="Wireless Headphones",
    description="Premium wireless headphones with noise cancellation and 30-hour battery life",
    price=Decimal("99.99"),
    category=electronics,
    stock=50,
    rating=Decimal("4.5")
)

Product.objects.create(
    name="Smart Watch",
    description="Feature-rich smartwatch with fitness tracking and heart rate monitor",
    price=Decimal("199.99"),
    category=electronics,
    stock=30,
    rating=Decimal("4.7")
)

Product.objects.create(
    name="Laptop Backpack",
    description="Durable laptop backpack with multiple compartments",
    price=Decimal("49.99"),
    category=electronics,
    stock=75,
    rating=Decimal("4.3")
)

Product.objects.create(
    name="Cotton T-Shirt",
    description="Comfortable 100% cotton t-shirt in various colors",
    price=Decimal("19.99"),
    category=clothing,
    stock=100,
    rating=Decimal("4.2")
)

Product.objects.create(
    name="Denim Jeans",
    description="Classic fit denim jeans with stretch comfort",
    price=Decimal("59.99"),
    category=clothing,
    stock=80,
    rating=Decimal("4.4")
)

Product.objects.create(
    name="Python Programming",
    description="Complete guide to Python programming for beginners",
    price=Decimal("39.99"),
    category=books,
    stock=60,
    rating=Decimal("4.8")
)

Product.objects.create(
    name="Web Development Book",
    description="Modern web development with HTML, CSS, and JavaScript",
    price=Decimal("44.99"),
    category=books,
    stock=45,
    rating=Decimal("4.6")
)

Product.objects.create(
    name="Coffee Maker",
    description="Programmable coffee maker with thermal carafe",
    price=Decimal("79.99"),
    category=Category.objects.create(name="Home & Kitchen", description="Home items"),
    stock=40,
    rating=Decimal("4.5")
)

print("✅ Sample data created successfully!")
print("📦 8 products added across 4 categories")
exit()
```

## Test the Application (2 minutes)

### 1. Browse Products
- Go to http://127.0.0.1:8000/
- Click "Products" in navigation
- View product listings

### 2. Register User Account
- Click "Register"
- Fill in:
  - Username: `testuser`
  - Email: `test@example.com`
  - Password: `test123`
  - Confirm Password: `test123`
- Click "Register"

### 3. Login
- Click "Login"
- Enter credentials
- Click "Login"

### 4. Add to Cart
- Click on any product
- Select quantity
- Click "Add to Cart"

### 5. Checkout
- Go to cart
- Click "Proceed to Checkout"
- Fill in shipping information
- Select payment method
- Click "Place Order"

### 6. View Order
- Click "Orders" in navigation
- View order history
- Click on order to see details

## Common Commands

### Start Server
```bash
python manage.py runserver
```

### Stop Server
Press `CTRL + C`

### Create Admin User
```bash
python manage.py createsuperuser
```

### Reset Database
```bash
# Windows
del db.sqlite3
python manage.py migrate

# Linux/Mac
rm db.sqlite3
python manage.py migrate
```

### Run on Different Port
```bash
python manage.py runserver 8080
```

### Access from Other Devices
```bash
python manage.py runserver 0.0.0.0:8000
```

Then access via: `http://YOUR_IP:8000/`

## Troubleshooting

### Issue: "python: command not found"
**Solution:** Use `py` or `python3` instead

### Issue: "Port already in use"
**Solution:** 
```bash
python manage.py runserver 8080
```

### Issue: "No module named django"
**Solution:**
```bash
pip install django pillow
```

### Issue: Static files not loading
**Solution:**
```bash
python manage.py collectstatic
```

## Next Steps

1. **Customize Design**
   - Edit `static/css/style.css`
   - Modify templates in `templates/`

2. **Add More Products**
   - Use admin panel
   - Upload product images

3. **Configure Email**
   - Set up email backend in `settings.py`
   - Enable email notifications

4. **Deploy to Production**
   - See deployment guide
   - Use PostgreSQL/MySQL
   - Set up HTTPS

## Useful URLs

| Page | URL |
|------|-----|
| Home | http://127.0.0.1:8000/ |
| Products | http://127.0.0.1:8000/products/ |
| Login | http://127.0.0.1:8000/users/login/ |
| Register | http://127.0.0.1:8000/users/register/ |
| Cart | http://127.0.0.1:8000/cart/ |
| Orders | http://127.0.0.1:8000/orders/history/ |
| Admin | http://127.0.0.1:8000/admin/ |

## Default Credentials

### Admin Account (after createsuperuser)
- Username: `admin` (or what you entered)
- Password: `admin123` (or what you entered)

### Test User Account (after registration)
- Username: `testuser`
- Password: `test123`

## Features Checklist

After setup, you should be able to:

- ✅ View home page
- ✅ Browse products
- ✅ Search products
- ✅ Filter by category
- ✅ View product details
- ✅ Register account
- ✅ Login/Logout
- ✅ Add to cart
- ✅ Update cart
- ✅ Checkout
- ✅ Place order
- ✅ View order history
- ✅ Access admin panel
- ✅ Manage products (admin)
- ✅ Manage orders (admin)

## Performance Tips

1. **Use Virtual Environment**
   ```bash
   python -m venv venv
   venv\Scripts\activate  # Windows
   source venv/bin/activate  # Linux/Mac
   ```

2. **Enable Debug Toolbar (Development)**
   ```bash
   pip install django-debug-toolbar
   ```

3. **Optimize Images**
   - Use compressed images
   - Recommended size: 800x800px
   - Format: JPG or PNG

4. **Database Optimization**
   - For production, use PostgreSQL
   - Enable database indexing
   - Use connection pooling

## Getting Help

- **Documentation:** See README.md
- **Installation Issues:** See INSTALLATION.md
- **Database Schema:** See DATABASE_SCHEMA.md
- **API Reference:** See API_DOCUMENTATION.md

## Quick Reference Card

```
┌─────────────────────────────────────────┐
│     E-Commerce Store Quick Reference    │
├─────────────────────────────────────────┤
│ Start Server:                           │
│   python manage.py runserver            │
│                                         │
│ Create Admin:                           │
│   python manage.py createsuperuser      │
│                                         │
│ Reset Database:                         │
│   del db.sqlite3 (Windows)              │
│   rm db.sqlite3 (Linux/Mac)             │
│   python manage.py migrate              │
│                                         │
│ Main URLs:                              │
│   Home: http://127.0.0.1:8000/          │
│   Admin: http://127.0.0.1:8000/admin/   │
│                                         │
│ Stop Server:                            │
│   CTRL + C                              │
└─────────────────────────────────────────┘
```

---

**You're all set! Happy coding! 🚀**

For detailed documentation, see README.md
