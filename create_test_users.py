import os
import django

os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'ecommerce.settings')
django.setup()

from django.contrib.auth.models import User
from users.models import UserProfile

print("Creating test user accounts...")
print("=" * 60)

# Test users data
test_users = [
    {
        "username": "testuser",
        "email": "test@example.com",
        "password": "test123",
        "first_name": "Test",
        "last_name": "User"
    },
    {
        "username": "john",
        "email": "john@example.com",
        "password": "john123",
        "first_name": "John",
        "last_name": "Doe"
    },
    {
        "username": "jane",
        "email": "jane@example.com",
        "password": "jane123",
        "first_name": "Jane",
        "last_name": "Smith"
    },
    {
        "username": "customer",
        "email": "customer@example.com",
        "password": "customer123",
        "first_name": "Customer",
        "last_name": "Demo"
    }
]

created_users = []

for user_data in test_users:
    username = user_data["username"]
    
    if not User.objects.filter(username=username).exists():
        user = User.objects.create_user(
            username=username,
            email=user_data["email"],
            password=user_data["password"],
            first_name=user_data["first_name"],
            last_name=user_data["last_name"]
        )
        
        # Create user profile
        UserProfile.objects.create(
            user=user,
            phone="1234567890",
            address="123 Test Street",
            city="Test City",
            state="Test State",
            zipcode="12345"
        )
        
        created_users.append(user_data)
        print(f"✓ Created user: {username}")
    else:
        print(f"  User already exists: {username}")

print("=" * 60)
print("\n✓ Test users creation completed!")
print(f"  Total test users: {len(test_users)}")
print("\n" + "=" * 60)
print("TEST USER CREDENTIALS:")
print("=" * 60)

for user_data in test_users:
    print(f"\nUsername: {user_data['username']}")
    print(f"Password: {user_data['password']}")
    print(f"Email: {user_data['email']}")
    print(f"Name: {user_data['first_name']} {user_data['last_name']}")
    print("-" * 60)

print("\n" + "=" * 60)
print("ADMIN CREDENTIALS:")
print("=" * 60)
print("\nUsername: admin")
print("Password: admin123")
print("Email: admin@example.com")
print("-" * 60)

print("\n✓ All accounts are ready to use!")
print("\nLogin at: http://127.0.0.1:8000/users/login/")
print("Admin panel: http://127.0.0.1:8000/admin/")
