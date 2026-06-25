import os
import django

os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'ecommerce.settings')
django.setup()

from products.models import Category, Product
from decimal import Decimal

print("Creating sample data...")

# Create categories
categories_data = [
    {"name": "Electronics", "description": "Electronic devices and gadgets"},
    {"name": "Clothing", "description": "Fashion and apparel"},
    {"name": "Books", "description": "Books and literature"},
    {"name": "Home & Kitchen", "description": "Home and kitchen items"},
]

categories = {}
for cat_data in categories_data:
    category, created = Category.objects.get_or_create(
        name=cat_data["name"],
        defaults={"description": cat_data["description"]}
    )
    categories[cat_data["name"]] = category
    if created:
        print(f"✓ Created category: {cat_data['name']}")
    else:
        print(f"  Category already exists: {cat_data['name']}")

# Create products
products_data = [
    {
        "name": "Wireless Headphones",
        "description": "Premium wireless headphones with noise cancellation and 30-hour battery life. Perfect for music lovers and professionals.",
        "price": "99.99",
        "category": "Electronics",
        "stock": 50,
        "rating": "4.5"
    },
    {
        "name": "Smart Watch",
        "description": "Feature-rich smartwatch with fitness tracking, heart rate monitor, and GPS. Stay connected on the go.",
        "price": "199.99",
        "category": "Electronics",
        "stock": 30,
        "rating": "4.7"
    },
    {
        "name": "Laptop Backpack",
        "description": "Durable laptop backpack with multiple compartments and USB charging port. Perfect for students and professionals.",
        "price": "49.99",
        "category": "Electronics",
        "stock": 75,
        "rating": "4.3"
    },
    {
        "name": "Bluetooth Speaker",
        "description": "Portable Bluetooth speaker with 360-degree sound and waterproof design. Great for outdoor activities.",
        "price": "79.99",
        "category": "Electronics",
        "stock": 40,
        "rating": "4.6"
    },
    {
        "name": "Cotton T-Shirt",
        "description": "Comfortable 100% cotton t-shirt in various colors. Soft, breathable, and perfect for everyday wear.",
        "price": "19.99",
        "category": "Clothing",
        "stock": 100,
        "rating": "4.2"
    },
    {
        "name": "Denim Jeans",
        "description": "Classic fit denim jeans with stretch comfort. Durable and stylish for any occasion.",
        "price": "59.99",
        "category": "Clothing",
        "stock": 80,
        "rating": "4.4"
    },
    {
        "name": "Running Shoes",
        "description": "Lightweight running shoes with cushioned sole and breathable mesh. Perfect for athletes and fitness enthusiasts.",
        "price": "89.99",
        "category": "Clothing",
        "stock": 60,
        "rating": "4.8"
    },
    {
        "name": "Winter Jacket",
        "description": "Warm winter jacket with water-resistant outer layer. Stay cozy in cold weather.",
        "price": "129.99",
        "category": "Clothing",
        "stock": 35,
        "rating": "4.5"
    },
    {
        "name": "Python Programming",
        "description": "Complete guide to Python programming for beginners. Learn coding from scratch with practical examples.",
        "price": "39.99",
        "category": "Books",
        "stock": 60,
        "rating": "4.8"
    },
    {
        "name": "Web Development Book",
        "description": "Modern web development with HTML, CSS, and JavaScript. Build responsive websites from scratch.",
        "price": "44.99",
        "category": "Books",
        "stock": 45,
        "rating": "4.6"
    },
    {
        "name": "Data Science Handbook",
        "description": "Comprehensive guide to data science with Python. Learn machine learning and data analysis.",
        "price": "54.99",
        "category": "Books",
        "stock": 30,
        "rating": "4.9"
    },
    {
        "name": "Coffee Maker",
        "description": "Programmable coffee maker with thermal carafe. Brew perfect coffee every morning.",
        "price": "79.99",
        "category": "Home & Kitchen",
        "stock": 40,
        "rating": "4.5"
    },
    {
        "name": "Blender",
        "description": "High-speed blender for smoothies and shakes. Powerful motor with multiple speed settings.",
        "price": "69.99",
        "category": "Home & Kitchen",
        "stock": 50,
        "rating": "4.4"
    },
    {
        "name": "Cookware Set",
        "description": "Non-stick cookware set with pots and pans. Complete kitchen solution for home chefs.",
        "price": "149.99",
        "category": "Home & Kitchen",
        "stock": 25,
        "rating": "4.7"
    },
    {
        "name": "Air Fryer",
        "description": "Digital air fryer for healthy cooking. Fry, bake, and roast with little to no oil.",
        "price": "99.99",
        "category": "Home & Kitchen",
        "stock": 35,
        "rating": "4.6"
    },
]

for prod_data in products_data:
    product, created = Product.objects.get_or_create(
        name=prod_data["name"],
        defaults={
            "description": prod_data["description"],
            "price": Decimal(prod_data["price"]),
            "category": categories[prod_data["category"]],
            "stock": prod_data["stock"],
            "rating": Decimal(prod_data["rating"])
        }
    )
    if created:
        print(f"✓ Created product: {prod_data['name']}")
    else:
        print(f"  Product already exists: {prod_data['name']}")

print("\n✓ Sample data creation completed!")
print(f"  Total Categories: {Category.objects.count()}")
print(f"  Total Products: {Product.objects.count()}")
