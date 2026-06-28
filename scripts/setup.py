#!/usr/bin/env python
"""
E-Commerce Store Setup Script
Automates the initial setup process
"""

import os
import sys
import subprocess


def print_header(text):
    """Print formatted header"""
    print("\n" + "=" * 60)
    print(f"  {text}")
    print("=" * 60 + "\n")


def print_step(step, text):
    """Print step information"""
    print(f"[{step}] {text}")


def run_command(command, description):
    """Run a shell command"""
    print(f"\n→ {description}...")
    try:
        result = subprocess.run(command, shell=True, check=True, capture_output=True, text=True)
        print(f"✓ {description} completed successfully!")
        return True
    except subprocess.CalledProcessError as e:
        print(f"✗ Error: {description} failed!")
        print(f"  {e.stderr}")
        return False


def check_python():
    """Check Python version"""
    print_step(1, "Checking Python version")
    version = sys.version_info
    print(f"   Python {version.major}.{version.minor}.{version.micro}")
    
    if version.major < 3 or (version.major == 3 and version.minor < 8):
        print("✗ Error: Python 3.8 or higher is required!")
        return False
    
    print("✓ Python version is compatible!")
    return True


def install_dependencies():
    """Install required packages"""
    print_step(2, "Installing dependencies")
    
    packages = [
        ("django", "Django web framework"),
        ("pillow", "Image processing library"),
    ]
    
    for package, description in packages:
        print(f"\n   Installing {package} ({description})...")
        if not run_command(f"pip install {package}", f"Install {package}"):
            return False
    
    return True


def create_directories():
    """Create required directories"""
    print_step(3, "Creating directories")
    
    directories = [
        "media",
        "media/products",
        "staticfiles",
    ]
    
    for directory in directories:
        if not os.path.exists(directory):
            os.makedirs(directory)
            print(f"✓ Created {directory}/")
        else:
            print(f"  {directory}/ already exists")
    
    return True


def setup_database():
    """Setup database"""
    print_step(4, "Setting up database")
    
    if not run_command("python manage.py makemigrations", "Create migrations"):
        return False
    
    if not run_command("python manage.py migrate", "Apply migrations"):
        return False
    
    return True


def create_superuser():
    """Create superuser account"""
    print_step(5, "Creating superuser account")
    
    print("\n   Please enter superuser credentials:")
    print("   (You can skip this and create it later with: python manage.py createsuperuser)")
    
    response = input("\n   Create superuser now? (y/n): ").lower()
    
    if response == 'y':
        os.system("python manage.py createsuperuser")
        return True
    else:
        print("   Skipped. You can create it later.")
        return True


def create_sample_data():
    """Create sample data"""
    print_step(6, "Creating sample data")
    
    response = input("\n   Create sample products and categories? (y/n): ").lower()
    
    if response == 'y':
        script = """
from products.models import Category, Product
from decimal import Decimal

# Create categories
electronics = Category.objects.create(name="Electronics", description="Electronic devices and gadgets")
clothing = Category.objects.create(name="Clothing", description="Fashion and apparel")
books = Category.objects.create(name="Books", description="Books and literature")
home = Category.objects.create(name="Home & Kitchen", description="Home and kitchen items")

# Create products
Product.objects.create(
    name="Wireless Headphones",
    description="Premium wireless headphones with noise cancellation and 30-hour battery life",
    price=Decimal("99.99"),
    category=electronics,
    stock=50,
    rating=Decimal("4.5")
)

Product.objects.create(
    name="Smart Watch",
    description="Feature-rich smartwatch with fitness tracking and heart rate monitor",
    price=Decimal("199.99"),
    category=electronics,
    stock=30,
    rating=Decimal("4.7")
)

Product.objects.create(
    name="Laptop Backpack",
    description="Durable laptop backpack with multiple compartments",
    price=Decimal("49.99"),
    category=electronics,
    stock=75,
    rating=Decimal("4.3")
)

Product.objects.create(
    name="Cotton T-Shirt",
    description="Comfortable 100% cotton t-shirt in various colors",
    price=Decimal("19.99"),
    category=clothing,
    stock=100,
    rating=Decimal("4.2")
)

Product.objects.create(
    name="Denim Jeans",
    description="Classic fit denim jeans with stretch comfort",
    price=Decimal("59.99"),
    category=clothing,
    stock=80,
    rating=Decimal("4.4")
)

Product.objects.create(
    name="Python Programming",
    description="Complete guide to Python programming for beginners",
    price=Decimal("39.99"),
    category=books,
    stock=60,
    rating=Decimal("4.8")
)

Product.objects.create(
    name="Web Development Book",
    description="Modern web development with HTML, CSS, and JavaScript",
    price=Decimal("44.99"),
    category=books,
    stock=45,
    rating=Decimal("4.6")
)

Product.objects.create(
    name="Coffee Maker",
    description="Programmable coffee maker with thermal carafe",
    price=Decimal("79.99"),
    category=home,
    stock=40,
    rating=Decimal("4.5")
)

print("Sample data created successfully!")
"""
        
        # Write script to temporary file
        with open('temp_setup_data.py', 'w') as f:
            f.write(script)
        
        # Run script
        result = os.system("python manage.py shell < temp_setup_data.py")
        
        # Clean up
        if os.path.exists('temp_setup_data.py'):
            os.remove('temp_setup_data.py')
        
        if result == 0:
            print("✓ Sample data created successfully!")
        else:
            print("✗ Error creating sample data")
        
        return True
    else:
        print("   Skipped. You can add data later via admin panel.")
        return True


def print_completion():
    """Print completion message"""
    print_header("Setup Complete!")
    
    print("✓ All setup steps completed successfully!\n")
    print("Next steps:")
    print("  1. Start the development server:")
    print("     python manage.py runserver\n")
    print("  2. Open your browser and go to:")
    print("     http://127.0.0.1:8000/\n")
    print("  3. Access the admin panel at:")
    print("     http://127.0.0.1:8000/admin/\n")
    print("  4. Read the documentation:")
    print("     - README.md - Main documentation")
    print("     - QUICKSTART.md - Quick start guide")
    print("     - INSTALLATION.md - Detailed installation\n")
    print("Happy coding! 🚀\n")


def main():
    """Main setup function"""
    print_header("E-Commerce Store Setup")
    print("This script will set up your E-Commerce Store application.\n")
    
    # Check if manage.py exists
    if not os.path.exists('manage.py'):
        print("✗ Error: manage.py not found!")
        print("  Please run this script from the project root directory.")
        sys.exit(1)
    
    # Run setup steps
    steps = [
        check_python,
        install_dependencies,
        create_directories,
        setup_database,
        create_superuser,
        create_sample_data,
    ]
    
    for step in steps:
        if not step():
            print("\n✗ Setup failed! Please fix the errors and try again.")
            sys.exit(1)
    
    # Print completion message
    print_completion()


if __name__ == "__main__":
    try:
        main()
    except KeyboardInterrupt:
        print("\n\n✗ Setup cancelled by user.")
        sys.exit(1)
    except Exception as e:
        print(f"\n✗ Unexpected error: {e}")
        sys.exit(1)
