# 🚀 Setup Instructions - Simple E-commerce Store

## Prerequisites
- Python 3.8 or higher
- pip (Python package manager)

---

## 📋 Step-by-Step Setup

### 1️⃣ **Create Virtual Environment (Recommended)**

```bash
# Navigate to project directory
cd "c:\wamp64\www\Simple E-commerce Store"

# Create virtual environment
python -m venv venv

# Activate virtual environment
# For Windows:
venv\Scripts\activate
```

---

### 2️⃣ **Install Dependencies**

```bash
pip install -r requirements.txt
```

**Dependencies installed:**
- Django 6.0.5 - Web framework
- Pillow 12.2.0 - Image handling
- gunicorn 21.2.0 - Production server
- whitenoise 6.6.0 - Static file serving
- psycopg2-binary 2.9.9 - PostgreSQL adapter
- python-decouple 3.8 - Configuration management
- dj-database-url 2.1.0 - Database URL parsing

---

### 3️⃣ **Database Setup**

```bash
# Run migrations to create database tables
python manage.py makemigrations
python manage.py migrate
```

---

### 4️⃣ **Create Superuser (Admin Account)**

```bash
python manage.py createsuperuser
```

**You'll be prompted to enter:**
- Username (e.g., admin)
- Email (optional, press Enter to skip)
- Password (enter twice)

---

### 5️⃣ **Create Media Directory**

```bash
# Create media folder for uploaded images
mkdir media
mkdir media\products
mkdir media\complaints
```

---

### 6️⃣ **Run Development Server**

```bash
python manage.py runserver
```

**Server will start at:** `http://127.0.0.1:8000/`

---

## 🌐 Access the Application

### **Customer Side:**
- **Home Page:** http://127.0.0.1:8000/
- **Products:** http://127.0.0.1:8000/products/
- **Register:** http://127.0.0.1:8000/users/register/
- **Login:** http://127.0.0.1:8000/users/login/
- **Cart:** http://127.0.0.1:8000/cart/
- **Dashboard:** http://127.0.0.1:8000/users/dashboard/

### **Admin Side:**
- **Django Admin Panel:** http://127.0.0.1:8000/admin/
- **Admin Dashboard:** http://127.0.0.1:8000/users/dashboard/ (login with superuser)
- **Manage Products:** http://127.0.0.1:8000/products/manage/
- **Manage Orders:** http://127.0.0.1:8000/orders/manage/
- **Manage Users:** http://127.0.0.1:8000/users/manage/
- **Manage Complaints:** http://127.0.0.1:8000/users/complaints/manage/

---

## 📦 Optional: Load Sample Data

### **Add Sample Categories:**

```bash
python manage.py shell
```

Then in the Python shell:

```python
from products.models import Category

categories = [
    {'name': 'Electronics', 'description': 'Electronic devices and gadgets'},
    {'name': 'Clothing', 'description': 'Fashion and apparel'},
    {'name': 'Books', 'description': 'Books and publications'},
    {'name': 'Home & Kitchen', 'description': 'Home and kitchen items'},
    {'name': 'Sports', 'description': 'Sports and outdoor equipment'},
]

for cat in categories:
    Category.objects.get_or_create(name=cat['name'], defaults={'description': cat['description']})

print("Categories created successfully!")
exit()
```

### **Add Sample Products (Optional):**

The project includes several Python scripts to add sample products:

```bash
# Add attractive products with real-looking data
python add_attractive_products.py

# Add more products
python add_more_products.py

# Add product images
python add_product_images_all.py
```

---

## 🔧 Troubleshooting

### **Issue: "No module named 'PIL'"**
```bash
pip install Pillow
```

### **Issue: "No module named 'django'"**
```bash
pip install Django==6.0.5
```

### **Issue: Port 8000 already in use**
```bash
# Run on different port
python manage.py runserver 8080
```

### **Issue: Database errors**
```bash
# Delete database and recreate
del db.sqlite3
python manage.py migrate
python manage.py createsuperuser
```

### **Issue: Static files not loading**
```bash
python manage.py collectstatic
```

---

## 🎯 Quick Start Commands

```bash
# Full setup in one go
python -m venv venv
venv\Scripts\activate
pip install -r requirements.txt
python manage.py migrate
python manage.py createsuperuser
python manage.py runserver
```

---

## 📱 Test the Application

1. **Register a new customer account**
2. **Browse products** (if no products, add them via admin panel)
3. **Add products to cart**
4. **Complete checkout**
5. **View order history**
6. **Login as admin** (superuser account)
7. **Manage products, orders, and users**

---

## 🔒 Default Admin Credentials

**After running `createsuperuser`:**
- URL: http://127.0.0.1:8000/admin/
- Username: (what you entered)
- Password: (what you entered)

---

## 📝 Notes

- **Database:** Currently uses SQLite (db.sqlite3) - perfect for development
- **Payment:** Payment gateway is not integrated (simulation only)
- **Email:** Uses console backend (emails print in terminal)
- **Media Files:** Product images stored in `media/products/`
- **Debug Mode:** Enabled by default (turn off for production)

---

## 🎨 Customization

### **Change Currency:**
Edit `ecommerce/settings.py`:
```python
CURRENCY_SYMBOL = '₹'  # Change to your currency
CURRENCY_CODE = 'INR'  # Change currency code
```

### **Change Language:**
Edit `ecommerce/settings.py`:
```python
LANGUAGE_CODE = 'en'  # Change to 'am', 'hi', 'ar', etc.
```

---

## 🚀 Production Deployment

For production deployment, see:
- `app.yaml` - Google Cloud App Engine configuration
- `.env.production` - Production environment variables

**Important for Production:**
1. Set `DEBUG = False` in settings.py
2. Configure proper email backend
3. Use PostgreSQL/MySQL instead of SQLite
4. Set up proper static file serving
5. Configure allowed hosts
6. Use environment variables for secrets

---

## 💡 Tips

- **Add Products First:** Login as admin and add products before testing shopping
- **Test Workflows:** Try both customer and admin workflows
- **Check Console:** Emails are printed in the terminal where you run the server
- **Use Superuser:** Admin features only work with staff/superuser accounts
- **Stock Management:** Products with 0 stock cannot be added to cart

---

## 📞 Need Help?

If you encounter issues:
1. Check the terminal for error messages
2. Ensure virtual environment is activated
3. Verify all migrations are applied
4. Check if required directories exist (media, static)

---

**Happy Shopping! 🛍️**
