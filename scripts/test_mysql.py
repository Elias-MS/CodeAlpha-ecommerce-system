import pymysql

try:
    # Connect to MySQL
    connection = pymysql.connect(
        host='localhost',
        user='root',
        password='',
        charset='utf8mb4'
    )
    
    cursor = connection.cursor()
    cursor.execute("SHOW DATABASES")
    databases = cursor.fetchall()
    
    print("Available databases:")
    for db in databases:
        print(f"  - {db[0]}")
        if 'simple' in db[0].lower() or 'ecommerce' in db[0].lower():
            print(f"    ✅ This is your database: '{db[0]}'")
    
    cursor.close()
    connection.close()
    
except Exception as e:
    print(f"Error: {e}")
