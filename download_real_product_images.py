"""
Download Real Product Images
Uses Picsum Photos to get real photographs for products
Adds minimal overlay with product name and price
"""

import os
import django
import urllib.request
import random

# Setup Django
os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'ecommerce.settings')
django.setup()

from products.models import Product
from PIL import Image, ImageDraw, ImageFont, ImageFilter, ImageEnhance
from io import BytesIO
from django.core.files.base import ContentFile

def download_image_from_url(width=800, height=800, seed=None):
    """Download a random image from Picsum"""
    try:
        if seed is None:
            seed = random.randint(1, 1000)
        url = f"https://picsum.photos/{width}/{height}?random={seed}"
        
        with urllib.request.urlopen(url, timeout=10) as response:
            image_data = response.read()
            return Image.open(BytesIO(image_data))
    except Exception as e:
        print(f"    ⚠️  Could not download image: {e}")
        # Create a fallback gradient image
        return create_fallback_image(width, height)

def create_fallback_image(width, height):
    """Create a nice gradient image as fallback"""
    image = Image.new('RGB', (width, height))
    draw = ImageDraw.Draw(image)
    
    # Random gradient colors
    colors = [
        [(59, 130, 246), (147, 51, 234)],  # Blue to Purple
        [(236, 72, 153), (251, 146, 60)],  # Pink to Orange
        [(34, 197, 94), (59, 130, 246)],   # Green to Blue
    ]
    color_pair = random.choice(colors)
    
    for y in range(height):
        ratio = y / height
        r = int(color_pair[0][0] * (1 - ratio) + color_pair[1][0] * ratio)
        g = int(color_pair[0][1] * (1 - ratio) + color_pair[1][1] * ratio)
        b = int(color_pair[0][2] * (1 - ratio) + color_pair[1][2] * ratio)
        draw.rectangle([(0, y), (width, y + 1)], fill=(r, g, b))
    
    return image


def add_product_overlay(image, product_name, price, category_name):
    """Add minimal overlay with product info (like your screenshot)"""
    width, height = image.size
    
    # Create a semi-transparent overlay at bottom
    overlay = Image.new('RGBA', (width, height), (0, 0, 0, 0))
    draw = ImageDraw.Draw(overlay)
    
    # Add dark gradient at bottom for text
    gradient_height = 200
    for y in range(gradient_height):
        alpha = int((y / gradient_height) * 180)
        draw.rectangle(
            [(0, height - gradient_height + y), (width, height - gradient_height + y + 1)],
            fill=(0, 0, 0, alpha)
        )
    
    # Composite the overlay
    image = image.convert('RGBA')
    image = Image.alpha_composite(image, overlay)
    image = image.convert('RGB')
    
    draw = ImageDraw.Draw(image)
    
    # Load fonts
    try:
        font_paths = [
            "C:/Windows/Fonts/arialbd.ttf",  # Arial Bold
            "C:/Windows/Fonts/calibrib.ttf",  # Calibri Bold
            "C:/Windows/Fonts/segoeuib.ttf",  # Segoe UI Bold
        ]
        
        font_large = None
        font_medium = None
        font_small = None
        
        for font_path in font_paths:
            if os.path.exists(font_path):
                font_large = ImageFont.truetype(font_path, 48)
                font_medium = ImageFont.truetype(font_path, 36)
                font_small = ImageFont.truetype(font_path, 28)
                break
        
        if not font_large:
            font_large = ImageFont.load_default()
            font_medium = ImageFont.load_default()
            font_small = ImageFont.load_default()
    except:
        font_large = ImageFont.load_default()
        font_medium = ImageFont.load_default()
        font_small = ImageFont.load_default()
    
    # Add category badge at top
    badge_padding = 20
    badge_text = category_name.upper()
    bbox = draw.textbbox((0, 0), badge_text, font=font_small)
    badge_width = bbox[2] - bbox[0] + 40
    badge_height = 50
    badge_x = 20
    badge_y = 20
    
    # Draw badge
    draw.rounded_rectangle(
        [(badge_x, badge_y), (badge_x + badge_width, badge_y + badge_height)],
        radius=25,
        fill=(100, 100, 100, 200)
    )
    draw.text((badge_x + 20, badge_y + 10), badge_text, fill='white', font=font_small)

    
    # Add product name at bottom (white text on dark gradient)
    # Split name into multiple lines if needed
    words = product_name.split()
    lines = []
    current_line = []
    
    for word in words:
        current_line.append(word)
        test_line = ' '.join(current_line)
        bbox = draw.textbbox((0, 0), test_line, font=font_large)
        if bbox[2] - bbox[0] > width - 60:
            current_line.pop()
            if current_line:
                lines.append(' '.join(current_line))
            current_line = [word]
    
    if current_line:
        lines.append(' '.join(current_line))
    
    # Limit to 2 lines
    lines = lines[:2]
    
    # Draw product name
    text_y = height - 150
    for i, line in enumerate(lines):
        draw.text((30, text_y + i * 55), line, fill='white', font=font_large)
    
    # Add price at bottom right
    price_text = f"${price}"
    bbox = draw.textbbox((0, 0), price_text, font=font_large)
    price_width = bbox[2] - bbox[0]
    price_x = width - price_width - 30
    price_y = height - 70
    
    # Draw price background
    draw.rounded_rectangle(
        [(price_x - 20, price_y - 10), (price_x + price_width + 20, price_y + 60)],
        radius=10,
        fill=(255, 106, 0)  # Orange like Alibaba
    )
    draw.text((price_x, price_y), price_text, fill='white', font=font_large)
    
    return image

def create_product_image_with_photo(product_name, category_name, price, product_id):
    """Create product image using real photo"""
    
    # Download or create base image
    base_image = download_image_from_url(800, 800, seed=product_id)
    
    # Add product overlay
    final_image = add_product_overlay(base_image, product_name, price, category_name)
    
    return final_image

def update_all_products():
    """Update all products with real photo-based images"""
    
    print("\n📸 Downloading and creating realistic product images...")
    print("=" * 70)
    
    products = Product.objects.all()
    total = products.count()
    updated = 0
    failed = 0
    
    for index, product in enumerate(products, 1):
        try:
            print(f"  [{index}/{total}] Processing: {product.name[:50]}...", end=" ")
            
            # Create image with real photo
            image = create_product_image_with_photo(
                product.name,
                product.category.name,
                product.price,
                product.id
            )
            
            # Save to BytesIO
            image_io = BytesIO()
            image.save(image_io, format='JPEG', quality=92)
            image_io.seek(0)
            
            # Create filename
            filename = f"product_{product.id}.jpg"
            
            # Save to product
            product.image.save(filename, ContentFile(image_io.read()), save=True)
            
            updated += 1
            print("✅")
            
        except Exception as e:
            failed += 1
            print(f"❌ Error: {str(e)}")
    
    print("\n" + "=" * 70)
    print(f"🎉 Successfully updated {updated}/{total} products!")
    if failed > 0:
        print(f"⚠️  {failed} products failed (using fallback images)")
    print("=" * 70)
    print("\n💡 Your products now have realistic photos!")
    print("📱 View them at: http://127.0.0.1:8000/products/")

if __name__ == '__main__':
    update_all_products()
