import os
import django
from pathlib import Path
from PIL import Image, ImageDraw, ImageFont

os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'ecommerce.settings')
django.setup()

from products.models import Product
from django.core.files import File

print("Adding images to products...")

# Create media/products directory if it doesn't exist
media_dir = Path('media/products')
media_dir.mkdir(parents=True, exist_ok=True)

# Product images with colors
product_colors = {
    "Wireless Headphones": "#4A90E2",
    "Smart Watch": "#50C878",
    "Laptop Backpack": "#FF6B6B",
    "Bluetooth Speaker": "#9B59B6",
    "Cotton T-Shirt": "#3498DB",
    "Denim Jeans": "#2ECC71",
    "Running Shoes": "#E74C3C",
    "Winter Jacket": "#34495E",
    "Python Programming": "#F39C12",
    "Web Development Book": "#1ABC9C",
    "Data Science Handbook": "#8E44AD",
    "Coffee Maker": "#D35400",
    "Blender": "#27AE60",
    "Cookware Set": "#C0392B",
    "Air Fryer": "#16A085",
}

def hex_to_rgb(hex_color):
    """Convert hex color to RGB tuple"""
    hex_color = hex_color.lstrip('#')
    return tuple(int(hex_color[i:i+2], 16) for i in (0, 2, 4))

def create_product_image(product_name, color):
    """Create a simple colored image with product name"""
    try:
        # Create image
        img = Image.new('RGB', (400, 400), color=hex_to_rgb(color))
        draw = ImageDraw.Draw(img)
        
        # Add product name text
        try:
            # Try to use a default font
            font = ImageFont.truetype("arial.ttf", 30)
        except:
            # Fallback to default font
            font = ImageFont.load_default()
        
        # Calculate text position (center)
        text = product_name
        bbox = draw.textbbox((0, 0), text, font=font)
        text_width = bbox[2] - bbox[0]
        text_height = bbox[3] - bbox[1]
        position = ((400 - text_width) / 2, (400 - text_height) / 2)
        
        # Draw text
        draw.text(position, text, fill='white', font=font)
        
        # Save to temp file
        filename = f"{product_name.lower().replace(' ', '_')}.png"
        filepath = media_dir / filename
        img.save(filepath, 'PNG')
        
        return filepath
        
    except Exception as e:
        print(f"  ✗ Error creating image for {product_name}: {e}")
        return None

# Update products with images
for product_name, color in product_colors.items():
    try:
        product = Product.objects.get(name=product_name)
        
        # Skip if product already has an image (not default)
        if product.image and 'default.jpg' not in str(product.image):
            print(f"  Product '{product_name}' already has an image")
            continue
        
        # Create image
        image_path = create_product_image(product_name, color)
        if image_path:
            # Save to product
            with open(image_path, 'rb') as f:
                filename = f"{product_name.lower().replace(' ', '_')}.png"
                product.image.save(filename, File(f), save=True)
            
            print(f"✓ Added image to: {product_name}")
        
    except Product.DoesNotExist:
        print(f"  ✗ Product not found: {product_name}")
    except Exception as e:
        print(f"  ✗ Error processing {product_name}: {e}")

print("\n✓ Image addition completed!")
print(f"  Total Products: {Product.objects.count()}")
print(f"  Products with images: {Product.objects.exclude(image='').exclude(image='products/default.jpg').count()}")
