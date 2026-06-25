"""
Export SQLite database to MySQL SQL dump
"""
import sqlite3
import os

# Connect to SQLite database
conn = sqlite3.connect('db.sqlite3')
cursor = conn.cursor()

# Get all tables
cursor.execute("SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;")
tables = cursor.fetchall()

print("Found tables:")
for table in tables:
    print(f"  - {table[0]}")

# Create SQL dump file
with open('database_export.sql', 'w', encoding='utf-8') as f:
    f.write("-- MySQL Database Export\n")
    f.write("-- Generated from SQLite database\n")
    f.write("-- Database: simple ecommerce system\n\n")
    f.write("SET SQL_MODE = \"NO_AUTO_VALUE_ON_ZERO\";\n")
    f.write("SET time_zone = \"+00:00\";\n\n")
    
    # Export each table
    for table in tables:
        table_name = table[0]
        
        # Skip Django system tables if you want
        if table_name.startswith('django_') or table_name == 'sqlite_sequence':
            continue
        
        print(f"\nExporting table: {table_name}")
        
        # Get table schema
        cursor.execute(f"PRAGMA table_info({table_name})")
        columns = cursor.fetchall()
        
        # Create CREATE TABLE statement
        f.write(f"\n-- Table: {table_name}\n")
        f.write(f"DROP TABLE IF EXISTS `{table_name}`;\n")
        f.write(f"CREATE TABLE `{table_name}` (\n")
        
        col_defs = []
        for col in columns:
            col_id, col_name, col_type, not_null, default_val, pk = col
            
            # Convert SQLite types to MySQL types
            mysql_type = col_type.upper()
            if 'INTEGER' in mysql_type:
                if pk:
                    mysql_type = 'INT AUTO_INCREMENT PRIMARY KEY'
                else:
                    mysql_type = 'INT'
            elif 'TEXT' in mysql_type or 'VARCHAR' in mysql_type:
                mysql_type = 'TEXT'
            elif 'REAL' in mysql_type or 'DECIMAL' in mysql_type:
                mysql_type = 'DECIMAL(10,2)'
            elif 'DATETIME' in mysql_type:
                mysql_type = 'DATETIME'
            elif 'BOOLEAN' in mysql_type or 'BOOL' in mysql_type:
                mysql_type = 'TINYINT(1)'
            
            col_def = f"  `{col_name}` {mysql_type}"
            if not_null and not pk:
                col_def += " NOT NULL"
            col_defs.append(col_def)
        
        f.write(",\n".join(col_defs))
        f.write("\n) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;\n\n")
        
        # Export data
        cursor.execute(f"SELECT * FROM {table_name}")
        rows = cursor.fetchall()
        
        if rows:
            # Get column names
            col_names = [desc[0] for desc in cursor.description]
            
            f.write(f"-- Data for table: {table_name}\n")
            for row in rows:
                values = []
                for val in row:
                    if val is None:
                        values.append("NULL")
                    elif isinstance(val, str):
                        # Escape single quotes
                        escaped = val.replace("'", "''")
                        values.append(f"'{escaped}'")
                    elif isinstance(val, (int, float)):
                        values.append(str(val))
                    else:
                        values.append(f"'{str(val)}'")
                
                f.write(f"INSERT INTO `{table_name}` ({', '.join([f'`{c}`' for c in col_names])}) VALUES ({', '.join(values)});\n")
            
            f.write("\n")
        
        print(f"  Exported {len(rows)} rows")

conn.close()

print("\n✅ Export completed!")
print("📄 File created: database_export.sql")
print("\nTo import into MySQL:")
print("1. Open phpMyAdmin")
print("2. Select 'simple ecommerce system' database")
print("3. Click 'Import' tab")
print("4. Choose file: database_export.sql")
print("5. Click 'Go'")
