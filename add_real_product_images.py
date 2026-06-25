import os
import django
from pathlib import Path
from PIL import Image, ImageDraw, ImageFont
import urllib.request
import ssl

os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'ecommerce.settings')
django.setup()

from products.models import Product
from django.core.files import File

print("Adding real product images...")

# Create media/products directory if it doesn't exist
media_dir = Path('media/products')
media_dir.mkdir(parents=True, exist_ok=True)

# Create SSL context to handle HTTPS
ssl_context = ssl.create_default_context()
ssl_context.check_hostname = False
ssl_context.verify_mode = ssl.CERT_NONE

# Real product image URLs (using free stock images)
product_images = {
    "Coffee Maker": "https://images.unsplash.com/photo-1495474472287-4d71bcdd2085?w=400&h=400&fit=crop",
    "Wireless Headphones": "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=400&h=400&fit=crop",
    "Smart Watch": "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=400&h=400&fit=crop",
    "Laptop Backpack": "https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=400&h=400&fit=crop",
    "Bluetooth Speaker": "https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?w=400&h=400&fit=crop",
    "Cotton T-Shirt": "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?w=400&h=400&fit=crop",
    "Denim Jeans": "https://images.unsplash.com/photo-1542272604-787c3835535d?w=400&h=400&fit=crop",
    "Running Shoes": "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=400&h=400&fit=crop",
    "Winter Jacket": "https://images.unsplash.com/photo-1551028719-00167b16eac5?w=400&h=400&fit=crop",
    "Python Programming": "https://images.unsplash.com/photo-1526374965328-7f61d4dc18c5?w=400&h=400&fit=crop",
    "Web Development Book": "https://images.unsplash.com/photo-1532012197267-da84d127e765?w=400&h=400&fit=crop",
    "Data Science Handbook": "https://images.unsplash.com/photo-1543002588-bfa74002ed7e?w=400&h=400&fit=crop",
    "Blender": "https://images.unsplash.com/photo-1585515320310-259814833e62?w=400&h=400&fit=crop",
    "Cookware Set": "https://images.unsplash.com/photo-1556909114-f6e7ad7d3136?w=400&h=400&fit=crop",
    "Air Fryer": "https://images.unsplash.com/photo-1585515320310-259814833e62?w=400&h=400&fit=crop",
}

def download_image(url, product_name):
    """Download image from URL"""
    try:
        print(f"  Downloading image for {product_name}...")
        
        # Create request with headers
        req = urllib.request.Request(url, headers={'User-Agent': 'Mozilla/5.0'})
        
        # Download image
        with urllib.request.urlopen(req, context=ssl_context, timeout=10) as response:
            image_data = response.read()
        
        # Save to temp file
        filename = f"{product_name.lower().replace(' ', '_')}.jpg"
        filepath = media_dir / filename
        
        with open(filepath, 'wb') as f:
            f.write(image_data)
        
        return filepath
        
    except Exception as e:
        print(f"  ✗ Error downloading image for {product_name}: {e}")
        return None

# Update products with real images
success_count = 0
for product_name, image_url in product_images.items():
    try:
        product = Product.objects.get(name=product_name)
        
        # Download image
        image_path = download_image(image_url, product_name)
        if image_path and image_path.exists():
            # Save to product
            with open(image_path, 'rb') as f:
                filename = f"{product_name.lower().replace(' ', '_')}.jpg"
                product.image.save(filename, File(f), save=True)
            
            print(f"✓ Added real image to: {product_name}")
            success_count += 1
        
    except Product.DoesNotExist:
        print(f"  ✗ Product not found: {product_name}")
    except Exception as e:
        print(f"  ✗ Error processing {product_name}: {e}")

print(f"\n✓ Image addition completed!")
print(f"  Successfully added: {success_count}/{len(product_images)} images")
print(f"  Total Products: {Product.objects.count()}")
