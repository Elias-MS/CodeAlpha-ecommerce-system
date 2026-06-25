# 🔧 HOW TO FIX THE DATABASE ERROR

## Error Message
```
OperationalError at /users/complaints/
no such column: users_complaint.is_urgent
```

## ✅ QUICK FIX (Recommended)

### Option 1: Run the Fix Script
1. **Double-click** `FIX_DATABASE_NOW.bat`
2. Wait for it to complete
3. **Refresh your browser** with `Ctrl + F5`
4. Done! ✅

### Option 2: Run Python Script Directly
1. Open Command Prompt in this folder
2. Run: `python SIMPLE_FIX.py`
3. **Refresh your browser** with `Ctrl + F5`
4. Done! ✅

---

## 🔄 ALTERNATIVE FIX (If above doesn't work)

### Using Django Migrations
1. **Stop the server** (Press `Ctrl + C` in the terminal)
2. Run: `python manage.py migrate`
3. **Start the server**: `python manage.py runserver`
4. **Refresh your browser** with `Ctrl + F5`
5. Done! ✅

---

## 📋 What This Fix Does

The fix adds a missing database column called `is_urgent` to the complaints table. This column is needed for the complaint system to work properly.

- **is_urgent**: Marks complaints that need to be announced publicly by admin

---

## ⚠️ If Nothing Works

If the error persists after trying all methods:

1. **Backup your data** (if you have important data)
2. **Delete** `db.sqlite3`
3. Run: `python manage.py migrate`
4. Run: `python create_admin.py` (to recreate admin account)
5. Run: `python create_sample_data.py` (to add sample products)
6. **Start server**: `python manage.py runserver`

---

## 🎯 After Fix - Test These Features

Once fixed, you should be able to:

✅ View Dashboard (both admin and user)
✅ Submit complaints
✅ View complaint history
✅ Admin can reply to complaints
✅ Admin can mark complaints as urgent
✅ Admin can create announcements from urgent complaints

---

## 📞 Need Help?

If you still see errors after trying all fixes, check:
- Is the server running?
- Did you refresh the browser with Ctrl + F5?
- Are you in the correct directory?
- Is Python installed correctly?
