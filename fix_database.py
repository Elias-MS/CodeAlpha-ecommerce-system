"""
Quick Database Fix Script
Run this to fix the complaint table
"""

import sqlite3
import os

# Database path
db_path = 'db.sqlite3'

if not os.path.exists(db_path):
    print("❌ Database file not found!")
    exit()

# Connect to database
conn = sqlite3.connect(db_path)
cursor = conn.cursor()

print("🔧 Fixing database...")

try:
    # Check if column exists
    cursor.execute("PRAGMA table_info(users_complaint)")
    columns = [column[1] for column in cursor.fetchall()]
    
    # Remove old columns if they exist
    if 'complaint_type' in columns or 'is_announced' in columns:
        print("⚠️  Old columns found. Need to recreate table...")
        
        # Create new table with correct structure
        cursor.execute("""
            CREATE TABLE IF NOT EXISTS users_complaint_new (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                user_id INTEGER NOT NULL,
                subject VARCHAR(200) NOT NULL,
                message TEXT NOT NULL,
                image VARCHAR(100),
                status VARCHAR(20) DEFAULT 'pending',
                is_urgent BOOLEAN DEFAULT 0,
                admin_reply TEXT,
                created_at DATETIME NOT NULL,
                updated_at DATETIME NOT NULL,
                replied_at DATETIME,
                FOREIGN KEY (user_id) REFERENCES auth_user(id)
            )
        """)
        
        # Copy data from old table (only compatible columns)
        cursor.execute("""
            INSERT INTO users_complaint_new 
            (id, user_id, subject, message, image, status, admin_reply, created_at, updated_at, replied_at)
            SELECT id, user_id, subject, message, image, 
                   CASE 
                       WHEN status = 'under_review' THEN 'replied'
                       WHEN status = 'closed' THEN 'resolved'
                       ELSE status
                   END,
                   admin_reply, created_at, updated_at, replied_at
            FROM users_complaint
        """)
        
        # Drop old table
        cursor.execute("DROP TABLE users_complaint")
        
        # Rename new table
        cursor.execute("ALTER TABLE users_complaint_new RENAME TO users_complaint")
        
        print("✅ Table recreated successfully!")
    
    elif 'is_urgent' not in columns:
        # Just add the missing column
        print("➕ Adding is_urgent column...")
        cursor.execute("ALTER TABLE users_complaint ADD COLUMN is_urgent BOOLEAN DEFAULT 0")
        print("✅ Column added successfully!")
    
    else:
        print("✅ Database is already up to date!")
    
    # Commit changes
    conn.commit()
    print("✅ Database fixed successfully!")
    print("\n🎉 You can now access the dashboard!")
    print("🔗 http://127.0.0.1:8000/users/dashboard/")
    
except Exception as e:
    print(f"❌ Error: {e}")
    conn.rollback()

finally:
    conn.close()

print("\n✅ Done! Refresh your browser (Ctrl + F5)")
