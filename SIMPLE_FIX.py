import sqlite3
import os

print("=" * 50)
print("DATABASE FIX TOOL")
print("=" * 50)
print()

# Check if database exists
if not os.path.exists('db.sqlite3'):
    print("❌ ERROR: db.sqlite3 not found!")
    print("Make sure you're in the correct directory.")
    input("\nPress Enter to close...")
    exit()

print("📁 Found database: db.sqlite3")
print("🔧 Attempting to fix...")
print()

try:
    conn = sqlite3.connect('db.sqlite3')
    cursor = conn.cursor()
    
    # Check if column exists
    cursor.execute("PRAGMA table_info(users_complaint)")
    columns = [column[1] for column in cursor.fetchall()]
    
    if 'is_urgent' in columns:
        print("✅ Column 'is_urgent' already exists!")
        print("✅ Database is OK!")
        print()
        print("=" * 50)
        print("SOLUTION: Just refresh your browser")
        print("Press Ctrl + F5 in your browser")
        print("=" * 50)
    else:
        print("⚠️  Column 'is_urgent' is missing")
        print("➕ Adding column now...")
        
        # Add the missing column
        cursor.execute("ALTER TABLE users_complaint ADD COLUMN is_urgent BOOLEAN DEFAULT 0")
        conn.commit()
        
        print("✅ SUCCESS! Column added!")
        print()
        print("=" * 50)
        print("DATABASE FIXED!")
        print("Now refresh your browser (Ctrl + F5)")
        print("=" * 50)
    
    conn.close()
    
except sqlite3.OperationalError as e:
    print(f"❌ ERROR: {e}")
    print()
    print("=" * 50)
    print("ALTERNATIVE SOLUTION:")
    print("=" * 50)
    print("1. Stop the Django server (Ctrl + C)")
    print("2. Run: python manage.py migrate")
    print("3. Start server: python manage.py runserver")
    print("4. Refresh browser (Ctrl + F5)")
    print("=" * 50)
except Exception as e:
    print(f"❌ Unexpected error: {e}")

print()
input("Press Enter to close...")
