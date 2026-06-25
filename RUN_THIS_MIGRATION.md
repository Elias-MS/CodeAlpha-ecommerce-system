# ⚠️ IMPORTANT - Run Migration

## 🔴 Error Fix Required

You need to run the database migration to update the complaint model.

## 📝 Steps to Fix:

### Option 1: Using Command Prompt

1. Open Command Prompt
2. Navigate to project folder:
   ```
   cd c:\xampp\htdocs\Simple E-commerce Store
   ```

3. Run migration:
   ```
   C:\Users\User\AppData\Local\Python\bin\python.exe manage.py migrate
   ```

### Option 2: Using Python Directly

1. Open Command Prompt in project folder
2. Run:
   ```
   python manage.py migrate
   ```

### Option 3: If Python Not in PATH

1. Use full path:
   ```
   C:\Users\User\AppData\Local\Python\bin\python.exe manage.py migrate
   ```

## ✅ After Migration:

1. Refresh your browser (Ctrl + F5)
2. Dashboard will work
3. All features will be available

## 🔗 Then Access:

**Dashboard:**
```
http://127.0.0.1:8000/users/dashboard/
```

**Admin Login:**
```
http://127.0.0.1:8000/users/login/
Username: admin
Password: admin123
```

**User Login:**
```
http://127.0.0.1:8000/users/login/
Username: testuser
Password: test123
```

---

## 📊 What This Migration Does:

- Removes: `complaint_type` field (simplified)
- Removes: `is_announced` field (replaced with is_urgent)
- Removes: `announcement_title` field (auto-generated)
- Adds: `is_urgent` field (for urgent announcements)
- Updates: Status choices (Pending, Replied, Resolved)

---

## ⚠️ If Migration Fails:

The migration might fail if you have existing data with the old structure.

**Solution: Reset Database (Development Only)**

1. Delete `db.sqlite3` file
2. Run:
   ```
   python manage.py migrate
   python manage.py createsuperuser
   ```
3. Create admin account again

---

**After running migration, everything will work!** ✅
