import sqlite3
import os

# Change to the script directory
os.chdir(r'c:\xampp\htdocs\Simple E-commerce Store')

# Connect to database
conn = sqlite3.connect('db.sqlite3')
cursor = conn.cursor()

try:
    # Add is_active column
    cursor.execute('ALTER TABLE products_product ADD COLUMN is_active INTEGER DEFAULT 1')
    conn.commit()
    print('✅ SUCCESS: is_active column added to products_product table')
    print('✅ All existing products are now active by default')
except sqlite3.OperationalError as e:
    if 'duplicate column name' in str(e).lower():
        print('✅ Column already exists - no action needed')
    else:
        print(f'❌ ERROR: {e}')
finally:
    conn.close()

print('\n🚀 You can now start the server!')
print('Run: C:\\Users\\User\\AppData\\Local\\Python\\bin\\python.exe manage.py runserver')
