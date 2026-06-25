# Installation Guide - E-Commerce Store

## Step-by-Step Installation Instructions

### Prerequisites

Before you begin, ensure you have the following installed:
- Python 3.8 or higher
- pip (Python package installer)
- Git (optional, for cloning)

### Step 1: Download/Clone the Project

**Option A: Download ZIP**
1. Download the project ZIP file
2. Extract it to your desired location
3. Open terminal/command prompt in the extracted folder

**Option B: Clone with Git**
```bash
git clone <repository-url>
cd ecommerce
```

### Step 2: Create Virtual Environment

**Windows:**
```bash
python -m venv venv
venv\Scripts\activate
```

**Linux/Mac:**
```bash
python3 -m venv venv
source venv/bin/activate
```

You should see `(venv)` in your terminal prompt.

### Step 3: Install Dependencies

```bash
pip install -r requirements.txt
```

This will install:
- Django 6.0.5
- Pillow 12.2.0
- Other required packages

### Step 4: Create Required Directories

**Windows:**
```bash
mkdir media
mkdir media\products
mkdir static
mkdir static\css
mkdir static\js
mkdir static\images
```

**Linux/Mac:**
```bash
mkdir -p media/products
mkdir -p static/{css,js,images}
```

### Step 5: Database Setup

Run migrations to create database tables:

```bash
python manage.py makemigrations
python manage.py migrate
```

Expected output:
```
Operations to perform:
  Apply all migrations: admin, auth, contenttypes, sessions, products, users, cart, orders
Running migrations:
  Applying contenttypes.0001_initial... OK
  Applying auth.0001_initial... OK
  ...
```

### Step 6: Create Superuser (Admin Account)

```bash
python manage.py createsuperuser
```

You'll be prompted to enter:
- Username: (e.g., admin)
- Email: (e.g., admin@example.com)
- Password: (enter a secure password)
- Password (again): (confirm password)

### Step 7: Load Sample Data (Optional)

You can add sample data through the admin panel or Django shell.

**Using Django Shell:**
```bash
python manage.py shell
```

Then run:
```python
from products.models import Category, Product
from decimal import Decimal

# Create categories
electronics = Category.objects.create(name="Electronics", description="Electronic devices and gadgets")
clothing = Category.objects.create(name="Clothing", description="Fashion and apparel")
books = Category.objects.create(name="Books", description="Books and literature")
home = Category.objects.create(name="Home & Kitchen", description="Home and kitchen items")

# Create sample products
Product.objects.create(
    name="Wireless Headphones",
    description="High-quality wireless headphones with noise cancellation",
    price=Decimal("99.99"),
    category=electronics,
    stock=50,
    rating=Decimal("4.5")
)

Product.objects.create(
    name="Smart Watch",
    description="Feature-rich smartwatch with fitness tracking",
    price=Decimal("199.99"),
    category=electronics,
    stock=30,
    rating=Decimal("4.7")
)

Product.objects.create(
    name="Cotton T-Shirt",
    description="Comfortable cotton t-shirt in various colors",
    price=Decimal("19.99"),
    category=clothing,
    stock=100,
    rating=Decimal("4.3")
)

print("Sample data created successfully!")
exit()
```

### Step 8: Run Development Server

```bash
python manage.py runserver
```

Expected output:
```
Watching for file changes with StatReloader
Performing system checks...

System check identified no issues (0 silenced).
May 29, 2026 - 10:00:00
Django version 6.0.5, using settings 'ecommerce.settings'
Starting development server at http://127.0.0.1:8000/
Quit the server with CTRL-BREAK.
```

### Step 9: Access the Application

Open your web browser and navigate to:

- **Main Site:** http://127.0.0.1:8000/
- **Admin Panel:** http://127.0.0.1:8000/admin/

Login to admin panel with the superuser credentials you created.

### Step 10: Add Products via Admin Panel

1. Go to http://127.0.0.1:8000/admin/
2. Login with superuser credentials
3. Click on "Products" → "Add Product"
4. Fill in product details:
   - Name
   - Description
   - Price
   - Category
   - Stock
   - Upload image (optional)
5. Click "Save"

Repeat for multiple products.

## Verification Checklist

✅ Virtual environment activated
✅ Dependencies installed
✅ Migrations completed
✅ Superuser created
✅ Server running without errors
✅ Can access home page
✅ Can access admin panel
✅ Can login to admin
✅ Can add products

## Common Issues and Solutions

### Issue 1: "python: command not found"

**Solution:**
- Windows: Use `py` instead of `python`
- Linux/Mac: Use `python3` instead of `python`

### Issue 2: "pip: command not found"

**Solution:**
```bash
python -m pip install -r requirements.txt
```

### Issue 3: Port 8000 already in use

**Solution:**
```bash
python manage.py runserver 8080
```

### Issue 4: Static files not loading

**Solution:**
```bash
python manage.py collectstatic --noinput
```

### Issue 5: Database locked error

**Solution:**
- Close all terminals/processes using the database
- Delete `db.sqlite3` and run migrations again

### Issue 6: Module not found errors

**Solution:**
```bash
pip install --upgrade pip
pip install -r requirements.txt --force-reinstall
```

### Issue 7: Permission denied (Linux/Mac)

**Solution:**
```bash
sudo chown -R $USER:$USER .
chmod -R 755 .
```

## Next Steps

After successful installation:

1. **Add Categories:**
   - Go to Admin → Categories → Add Category
   - Create: Electronics, Clothing, Books, Home & Kitchen

2. **Add Products:**
   - Go to Admin → Products → Add Product
   - Add at least 5-10 products with images

3. **Test User Registration:**
   - Go to main site
   - Click "Register"
   - Create a test user account

4. **Test Shopping Flow:**
   - Browse products
   - Add items to cart
   - Proceed to checkout
   - Place an order

5. **Verify Order in Admin:**
   - Check Admin → Orders
   - Verify order details

## Development Tips

### Running on Different Port

```bash
python manage.py runserver 0.0.0.0:8080
```

### Accessing from Other Devices

1. Update `settings.py`:
```python
ALLOWED_HOSTS = ['*']  # For development only
```

2. Find your IP address:
```bash
# Windows
ipconfig

# Linux/Mac
ifconfig
```

3. Access from other device:
```
http://YOUR_IP_ADDRESS:8000/
```

### Creating Database Backup

```bash
# Backup
python manage.py dumpdata > backup.json

# Restore
python manage.py loaddata backup.json
```

### Resetting Database

```bash
# Delete database
rm db.sqlite3  # Linux/Mac
del db.sqlite3  # Windows

# Recreate
python manage.py migrate
python manage.py createsuperuser
```

## Production Deployment Notes

For production deployment:

1. Set `DEBUG = False` in settings.py
2. Configure proper `ALLOWED_HOSTS`
3. Use PostgreSQL or MySQL instead of SQLite
4. Set up proper static file serving
5. Use environment variables for secrets
6. Enable HTTPS
7. Use production server (Gunicorn/uWSGI)
8. Set up proper logging

## Support

If you encounter any issues:

1. Check the error message carefully
2. Verify all steps were completed
3. Check Django version compatibility
4. Ensure virtual environment is activated
5. Review Django documentation

## Useful Commands

```bash
# Check Django version
python manage.py --version

# Create new app
python manage.py startapp appname

# Make migrations
python manage.py makemigrations

# Apply migrations
python manage.py migrate

# Create superuser
python manage.py createsuperuser

# Run server
python manage.py runserver

# Collect static files
python manage.py collectstatic

# Django shell
python manage.py shell

# Show migrations
python manage.py showmigrations

# Database shell
python manage.py dbshell
```

---

**Installation Complete! Happy Coding! 🚀**
