# Import Database to MySQL - Step by Step Guide 📥

## ✅ Export Completed Successfully!

**File Created**: `database_export.sql`  
**Location**: `c:\xampp\htdocs\Simple E-commerce Store\database_export.sql`

---

## 📊 What Was Exported:

✅ **143 Products** (products_product)  
✅ **12 Categories** (products_category)  
✅ **7 Users** (auth_user)  
✅ **6 Orders** (orders_order)  
✅ **7 Order Items** (orders_orderitem)  
✅ **4 Carts** (cart_cart)  
✅ **2 Cart Items** (cart_cartitem)  
✅ **1 Complaint** (users_complaint)  
✅ **2 Announcements** (users_announcement)  
✅ **7 User Profiles** (users_userprofile)  
✅ **76 Permissions** (auth_permission)  

**Total: 24 tables exported!**

---

## 🚀 How to Import to MySQL (phpMyAdmin)

### Step 1: Open phpMyAdmin
- URL: http://localhost/phpmyadmin/
- You're already there! ✅

### Step 2: Select Database
- Click on **"simple ecommerce system"** in the left sidebar
- You should see "No tables found in database" (current status)

### Step 3: Go to Import Tab
- Click the **"Import"** tab at the top
- You'll see the import file interface

### Step 4: Choose File
- Click **"Choose File"** button
- Navigate to: `c:\xampp\htdocs\Simple E-commerce Store\`
- Select file: **`database_export.sql`**

### Step 5: Import Settings
- **Format**: SQL ✅ (should be auto-selected)
- **Character set**: utf8mb4 (recommended)
- Leave other settings as default

### Step 6: Execute Import
- Scroll down
- Click the **"Go"** button at the bottom right
- Wait for the import to complete (should take 5-10 seconds)

### Step 7: Verify Import
- You should see a success message
- Click on the **"Structure"** tab
- You should now see **24 tables** instead of "No tables found"

---

## 📋 Expected Result After Import:

Your database will contain:

### Products Tables:
- ✅ products_category (12 categories)
- ✅ products_product (143 products)
- ✅ products_productreview (0 reviews)

### Users Tables:
- ✅ auth_user (7 users including admin)
- ✅ users_userprofile (7 profiles)
- ✅ users_complaint (1 complaint)
- ✅ users_announcement (2 announcements)
- ✅ users_notification (0 notifications)
- ✅ users_contactmessage (0 messages)
- ✅ users_userreport (0 reports)

### Orders Tables:
- ✅ orders_order (6 orders)
- ✅ orders_orderitem (7 items)

### Cart Tables:
- ✅ cart_cart (4 carts)
- ✅ cart_cartitem (2 items)

### Auth Tables:
- ✅ auth_permission (76 permissions)
- ✅ auth_group (0 groups)
- ✅ auth_user_groups (0 entries)

---

## 🔧 Update Django Settings for MySQL

After successful import, update your Django settings to use MySQL:

**File**: `ecommerce/settings.py`

**Replace**:
```python
DATABASES = {
    'default': {
        'ENGINE': 'django.db.backends.sqlite3',
        'NAME': BASE_DIR / 'db.sqlite3',
    }
}
```

**With**:
```python
DATABASES = {
    'default': {
        'ENGINE': 'django.db.backends.mysql',
        'NAME': 'simple ecommerce system',
        'USER': 'root',  # Your MySQL username
        'PASSWORD': '',  # Your MySQL password (empty by default in XAMPP)
        'HOST': 'localhost',
        'PORT': '3306',
        'OPTIONS': {
            'init_command': "SET sql_mode='STRICT_TRANS_TABLES'",
            'charset': 'utf8mb4',
        }
    }
}
```

**Install MySQL connector**:
```bash
pip install mysqlclient
```

---

## ⚠️ Troubleshooting

### Issue: Import fails with "Max execution time"
**Solution**:
- Edit `php.ini` in XAMPP
- Increase `max_execution_time = 300`
- Restart Apache

### Issue: Import fails with "File too large"
**Solution**:
- Edit `php.ini`
- Increase `upload_max_filesize = 50M`
- Increase `post_max_size = 50M`
- Restart Apache

### Issue: Character encoding errors
**Solution**:
- In phpMyAdmin import settings
- Select **"utf8mb4_unicode_ci"** as collation
- Re-import

### Issue: "Table already exists"
**Solution**:
- Drop all tables first
- Or select "Drop tables before import" in import options
- Re-import

---

## 📝 Quick Verification SQL Queries

After import, run these in phpMyAdmin SQL tab to verify:

```sql
-- Check products count
SELECT COUNT(*) as total_products FROM products_product;
-- Expected: 143

-- Check categories
SELECT COUNT(*) as total_categories FROM products_category;
-- Expected: 12

-- Check users
SELECT COUNT(*) as total_users FROM auth_user;
-- Expected: 7

-- Check orders
SELECT COUNT(*) as total_orders FROM orders_order;
-- Expected: 6

-- View sample products
SELECT id, name, price, stock FROM products_product LIMIT 5;

-- View categories
SELECT id, name FROM products_category;
```

---

## ✅ Success Checklist

After import, verify:
- [ ] 24 tables visible in Structure tab
- [ ] 143 products in products_product table
- [ ] 12 categories in products_category table
- [ ] 7 users in auth_user table
- [ ] 6 orders in orders_order table
- [ ] No error messages
- [ ] Can view table data by clicking table names

---

## 🎯 Next Steps After Import

1. **Update Django settings** to use MySQL (see above)
2. **Restart Django server**
3. **Test website** to ensure everything works
4. **Backup MySQL database** regularly
5. **Consider removing** SQLite file (db.sqlite3) after confirming MySQL works

---

## 📂 File Locations

- **Export File**: `c:\xampp\htdocs\Simple E-commerce Store\database_export.sql`
- **SQLite Original**: `c:\xampp\htdocs\Simple E-commerce Store\db.sqlite3`
- **Settings File**: `c:\xampp\htdocs\Simple E-commerce Store\ecommerce\settings.py`

---

## 🆘 Need Help?

If you encounter any issues:
1. Check phpMyAdmin error messages
2. Check XAMPP Apache/MySQL logs
3. Verify MySQL service is running in XAMPP Control Panel
4. Try importing table by table if full import fails
5. Check file permissions on database_export.sql

---

**Ready to import! Follow the steps above and your database will be in MySQL!** 🚀
