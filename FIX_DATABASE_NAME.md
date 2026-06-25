# Fix Database Name Issue 🔧

## ⚠️ Problem
Django cannot connect because the database name has spaces: `simple ecommerce system`
MySQL with spaces needs special handling.

## ✅ Solution: Rename Database

### **Option 1: Rename in phpMyAdmin (EASIEST)**

1. **Open phpMyAdmin**: http://localhost/phpmyadmin/
2. **Click on** `simple ecommerce system` in left sidebar
3. **Click** "Operations" tab (top menu)
4. **Find** "Rename database to" section
5. **Enter new name**: `simple_ecommerce_system` (with underscores)
6. **Click** "Go" button
7. **Confirm** the rename

✅ **Done!** The database is now named `simple_ecommerce_system`

---

### **Option 2: Create New Database and Re-Import**

1. **Create new database** in phpMyAdmin:
   - Click "New" in left sidebar
   - Database name: `simple_ecommerce_system`
   - Collation: `utf8mb4_unicode_ci`
   - Click "Create"

2. **Import the data**:
   - Select `simple_ecommerce_system` database
   - Click "Import" tab
   - Choose file: `database_export.sql`
   - Click "Go"

3. **Delete old database** (optional):
   - Select `simple ecommerce system`
   - Click "Operations" tab
   - Scroll down to "Remove database"
   - Click "Drop the database (DROP)"

---

## 🚀 After Renaming

**The Django settings are already updated!**
- Database name in settings.py: `simple_ecommerce_system` ✅

**Just restart the Django server:**
- The server will automatically reconnect
- Or I'll restart it for you

---

## 📋 Current Configuration

**Django settings.py:**
```python
DATABASES = {
    'default': {
        'ENGINE': 'django.db.backends.mysql',
        'NAME': 'simple_ecommerce_system',  # Underscores!
        'USER': 'root',
        'PASSWORD': '',
        'HOST': 'localhost',
        'PORT': '3306',
    }
}
```

---

## ✅ Verification

After renaming, you should see in phpMyAdmin:
- Old name: ❌ `simple ecommerce system`
- New name: ✅ `simple_ecommerce_system`

---

## 🆘 Alternative: Keep Spaces (Not Recommended)

If you want to keep the name with spaces, update settings.py:
```python
'NAME': '`simple ecommerce system`',  # Backticks around name
```

But using underscores is **much better**!

---

**Please rename the database to `simple_ecommerce_system` in phpMyAdmin, then let me know!** 🙏
