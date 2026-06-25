# 🔧 FIX DATABASE ERROR - QUICK SOLUTION

## ❌ Error You're Seeing:
```
OperationalError at /orders/manage/
no such column: products_product.is_active
```

## ✅ SOLUTION (2 Steps):

### Step 1: Run the Fix Script

**Double-click this file:**
```
FIX_DATABASE.bat
```

**Location:** `c:\xampp\htdocs\Simple E-commerce Store\FIX_DATABASE.bat`

**What it does:**
1. Adds `is_active` column to products table
2. Sets all existing products to active
3. Starts the Django server

---

### Step 2: Refresh Your Browser

After the server starts, refresh your browser:
- **Manage Orders:** http://127.0.0.1:8000/orders/manage/
- **Manage Products:** http://127.0.0.1:8000/products/manage/

---

## 🎯 Alternative: Manual Fix

If the batch file doesn't work, follow these steps:

### 1. Open PowerShell (NEW WINDOW)
- Press `Windows + X`
- Click "Windows PowerShell" or "Terminal"

### 2. Run These Commands:
```powershell
cd "c:\xampp\htdocs\Simple E-commerce Store"

C:\Users\User\AppData\Local\Python\bin\python.exe add_is_active_column.py

C:\Users\User\AppData\Local\Python\bin\python.exe manage.py runserver
```

### 3. Wait for Success Message:
```
✅ SUCCESS: is_active column added to products_product table
✅ All existing products are now active by default

🚀 You can now start the server!
```

### 4. Server Starts:
```
Starting development server at http://127.0.0.1:8000/
```

### 5. Refresh Browser
Go to: http://127.0.0.1:8000/orders/manage/

---

## ✅ What This Fix Does

**Adds `is_active` column to products:**
- All existing products → Active (visible to customers)
- New products → Active by default
- You can deactivate products in Manage Products page

**After fix:**
- ✅ Orders page works
- ✅ Products page works
- ✅ Active/Deactive feature works
- ✅ All features working!

---

## 🔗 Quick Links After Fix

- **Manage Orders:** http://127.0.0.1:8000/orders/manage/
- **Manage Products:** http://127.0.0.1:8000/products/manage/
- **Admin Dashboard:** http://127.0.0.1:8000/users/dashboard/

---

## ❓ Still Having Issues?

The terminal might be stuck with a pending prompt. 

**Solution:**
1. Close ALL PowerShell/Terminal windows
2. Open a NEW PowerShell window
3. Run the commands above

---

## 📝 Summary

**Problem:** Database missing `is_active` column  
**Solution:** Run `FIX_DATABASE.bat` or manual commands  
**Result:** All features working!  

**Just double-click `FIX_DATABASE.bat` and you're done!** 🚀
