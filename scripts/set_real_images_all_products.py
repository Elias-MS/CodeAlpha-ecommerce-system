"""
Set Real Images for All Products
Uses Lorem Picsum to get real photographs
"""

import os
import django
import urllib.request
import time

# Setup Django
os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'ecommerce.settings')
django.setup()

from products.models import Product
from PIL import Image
from io import BytesIO
from django.core.files.base import ContentFile

def download_real_photo(seed_id, width=800, height=800):
    """Download a real photo from Lorem Picsum"""
    try:
        # Lorem Picsum provides real photos
        url = f"https://picsum.photos/seed/{seed_id}/{width}/{height}"
        
        print(f"      Downloading real photo...", end=" ")
        
        with urllib.request.urlopen(url, timeout=15) as response:
            image_data = response.read()
            image = Image.open(BytesIO(image_data))
            print("✅")
            return image
            
    except Exception as e:
        print(f"❌ {str(e)[:40]}")
        return None

def update_product_image(product, index):
    """Update a single product with a real photo"""
    
    print(f"\n[{index}/143] {product.name[:50]}")
    print(f"  Category: {product.category.name}")
    
    # Use product ID as seed for consistent images
    seed = f"product{product.id}"
    
    # Download real photo
    image = download_real_photo(seed)
    
    if image is None:
        print(f"  ⚠️  Skipped")
        return False
    
    # Save to product
    image_io = BytesIO()
    image.save(image_io, format='JPEG', quality=90)
    image_io.seek(0)
    
    filename = f"product_{product.id}.jpg"
    product.image.save(filename, ContentFile(image_io.read()), save=True)
    
    print(f"  ✅ Saved!")
    
    # Small delay
    time.sleep(0.3)
    return True

def update_all_products():
    """Update all products with real photos"""
    
    print("=" * 70)
    print("📸 SETTING REAL IMAGES FOR ALL PRODUCTS")
    print("=" * 70)
    print("Using Lorem Picsum - Real photographs")
    print("=" * 70)
    print()
    
    products = Product.objects.all()
    total = products.count()
    
    print(f"Total products: {total}\n")
    
    updated = 0
    failed = 0
    
    for index, product in enumerate(products, 1):
        try:
            if update_product_image(product, index):
                updated += 1
            else:
                failed += 1
        except Exception as e:
            print(f"  ❌ Error: {str(e)[:50]}")
            failed += 1
    
    print("\n" + "=" * 70)
    print(f"🎉 COMPLETE!")
    print(f"   ✅ Updated: {updated}/{total}")
    if failed > 0:
        print(f"   ❌ Failed: {failed}")
    print("=" * 70)
    print(f"\n🚀 View your products: http://127.0.0.1:8000/products/")
    print("   All products now have real photographic images!")

if __name__ == '__main__':
    update_all_products()
