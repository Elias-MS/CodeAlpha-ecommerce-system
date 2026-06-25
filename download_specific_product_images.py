"""
Download Specific Product Images from Unsplash
Matches product type with appropriate images
Example: Laptop product gets laptop image, Speaker gets speaker image
"""

import os
import django
import urllib.request
import urllib.parse
import json
import time

# Setup Django
os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'ecommerce.settings')
django.setup()

from products.models import Product
from PIL import Image, ImageDraw, ImageFont
from io import BytesIO
from django.core.files.base import ContentFile

# Unsplash API Access Key (Free - 50 requests/hour)
# You can get your own key at: https://unsplash.com/developers
UNSPLASH_ACCESS_KEY = "demo"  # Using demo mode for now

def extract_product_keyword(product_name):
    """Extract the main product keyword from product name"""
    
    # Product keyword mapping
    keywords_map = {
        # Electronics
        'laptop': ['laptop', 'notebook', 'macbook'],
        'phone': ['phone', 'smartphone', 'mobile', 'iphone'],
        'speaker': ['speaker', 'bluetooth speaker', 'audio'],
        'headphone': ['headphone', 'earbuds', 'earphone', 'airpods'],
        'keyboard': ['keyboard', 'mechanical keyboard'],
        'mouse': ['mouse', 'gaming mouse'],
        'webcam': ['webcam', 'camera'],
        'watch': ['smartwatch', 'watch', 'apple watch'],
        'tablet': ['tablet', 'ipad'],
        'monitor': ['monitor', 'display', 'screen'],
        'cable': ['cable', 'hdmi', 'usb'],
        'charger': ['charger', 'power bank', 'battery'],
        'light': ['led light', 'bulb', 'lamp'],
        
        # Fashion & Clothing
        'jeans': ['jeans', 'denim'],
        'tshirt': ['t-shirt', 'tee', 'shirt'],
        'jacket': ['jacket', 'coat'],
        'shoes': ['shoes', 'sneakers', 'running shoes'],
        'bag': ['bag', 'backpack', 'handbag'],
        'sunglasses': ['sunglasses', 'glasses'],
        'watch': ['watch', 'wristwatch'],
        
        # Home & Kitchen
        'blender': ['blender', 'mixer'],
        'coffee': ['coffee maker', 'coffee machine'],
        'knife': ['knife', 'kitchen knife'],
        'cookware': ['cookware', 'pot', 'pan'],
        'vacuum': ['vacuum', 'vacuum cleaner'],
        'air fryer': ['air fryer', 'fryer'],
        
        # Sports
        'dumbbell': ['dumbbell', 'weights'],
        'yoga mat': ['yoga mat', 'exercise mat'],
        'bicycle': ['bicycle', 'bike'],
        'ball': ['ball', 'football', 'basketball'],
        
        # Books
        'book': ['book', 'books', 'reading'],
    }
    
    product_lower = product_name.lower()
    
    # Check each keyword category
    for keyword, variations in keywords_map.items():
        for variation in variations:
            if variation in product_lower:
                return keyword
    
    # Default: use first word of product name
    first_word = product_name.split()[0].lower()
    return first_word


def download_unsplash_image(keyword, width=800, height=800):
    """Download image from Unsplash based on keyword"""
    try:
        # Using Unsplash Source (no API key needed)
        # Format: https://source.unsplash.com/{width}x{height}/?{keyword}
        encoded_keyword = urllib.parse.quote(keyword)
        url = f"https://source.unsplash.com/{width}x{height}/?{encoded_keyword}"
        
        print(f"      Downloading: {keyword} image...", end=" ")
        
        with urllib.request.urlopen(url, timeout=15) as response:
            image_data = response.read()
            image = Image.open(BytesIO(image_data))
            print("✅")
            return image
            
    except Exception as e:
        print(f"❌ ({str(e)[:30]})")
        return None

def create_fallback_image(keyword, width=800, height=800):
    """Create a nice fallback image if download fails"""
    image = Image.new('RGB', (width, height))
    draw = ImageDraw.Draw(image)
    
    # Gradient background
    colors = [(59, 130, 246), (147, 51, 234)]  # Blue to Purple
    
    for y in range(height):
        ratio = y / height
        r = int(colors[0][0] * (1 - ratio) + colors[1][0] * ratio)
        g = int(colors[0][1] * (1 - ratio) + colors[1][1] * ratio)
        b = int(colors[0][2] * (1 - ratio) + colors[1][2] * ratio)
        draw.rectangle([(0, y), (width, y + 1)], fill=(r, g, b))
    
    # Add text
    try:
        font = ImageFont.truetype("C:/Windows/Fonts/arial.ttf", 60)
    except:
        font = ImageFont.load_default()
    
    text = keyword.upper()
    bbox = draw.textbbox((0, 0), text, font=font)
    text_width = bbox[2] - bbox[0]
    text_x = (width - text_width) // 2
    text_y = height // 2 - 30
    
    draw.text((text_x, text_y), text, fill='white', font=font)
    
    return image

def add_product_overlay(image, product_name, price, category_name):
    """Add minimal overlay with product info"""
    width, height = image.size
    
    # Create semi-transparent overlay at bottom
    overlay = Image.new('RGBA', (width, height), (0, 0, 0, 0))
    draw = ImageDraw.Draw(overlay)
    
    # Dark gradient at bottom
    gradient_height = 200
    for y in range(gradient_height):
        alpha = int((y / gradient_height) * 180)
        draw.rectangle(
            [(0, height - gradient_height + y), (width, height - gradient_height + y + 1)],
            fill=(0, 0, 0, alpha)
        )
    
    # Composite overlay
    image = image.convert('RGBA')
    image = Image.alpha_composite(image, overlay)
    image = image.convert('RGB')
    
    draw = ImageDraw.Draw(image)

    
    # Load fonts
    try:
        font_large = ImageFont.truetype("C:/Windows/Fonts/arialbd.ttf", 48)
        font_medium = ImageFont.truetype("C:/Windows/Fonts/arialbd.ttf", 36)
        font_small = ImageFont.truetype("C:/Windows/Fonts/arialbd.ttf", 28)
    except:
        font_large = ImageFont.load_default()
        font_medium = ImageFont.load_default()
        font_small = ImageFont.load_default()
    
    # Category badge at top
    badge_text = category_name.upper()
    bbox = draw.textbbox((0, 0), badge_text, font=font_small)
    badge_width = bbox[2] - bbox[0] + 40
    badge_height = 50
    badge_x = 20
    badge_y = 20
    
    draw.rounded_rectangle(
        [(badge_x, badge_y), (badge_x + badge_width, badge_y + badge_height)],
        radius=25,
        fill=(100, 100, 100, 200)
    )
    draw.text((badge_x + 20, badge_y + 10), badge_text, fill='white', font=font_small)
    
    # Product name at bottom
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
    
    lines = lines[:2]
    
    text_y = height - 150
    for i, line in enumerate(lines):
        draw.text((30, text_y + i * 55), line, fill='white', font=font_large)
    
    # Price at bottom right
    price_text = f"${price}"
    bbox = draw.textbbox((0, 0), price_text, font=font_large)
    price_width = bbox[2] - bbox[0]
    price_x = width - price_width - 30
    price_y = height - 70
    
    draw.rounded_rectangle(
        [(price_x - 20, price_y - 10), (price_x + price_width + 20, price_y + 60)],
        radius=10,
        fill=(255, 106, 0)
    )
    draw.text((price_x, price_y), price_text, fill='white', font=font_large)
    
    return image

def update_product_with_specific_image(product):
    """Update a single product with keyword-specific image"""
    
    # Extract keyword from product name
    keyword = extract_product_keyword(product.name)
    
    print(f"  [{product.id}] {product.name[:40]}")
    print(f"      Keyword: '{keyword}'")
    
    # Download image from Unsplash
    base_image = download_unsplash_image(keyword)
    
    # Use fallback if download failed
    if base_image is None:
        print(f"      Using fallback image...")
        base_image = create_fallback_image(keyword)
    
    # Add product overlay
    final_image = add_product_overlay(
        base_image,
        product.name,
        product.price,
        product.category.name
    )
    
    # Save to product
    image_io = BytesIO()
    final_image.save(image_io, format='JPEG', quality=92)
    image_io.seek(0)
    
    filename = f"product_{product.id}.jpg"
    product.image.save(filename, ContentFile(image_io.read()), save=True)
    
    print(f"      Saved! ✅\n")
    
    # Small delay to avoid rate limiting
    time.sleep(0.5)

def update_all_products():
    """Update all products with specific images"""
    
    print("=" * 70)
    print("📸 DOWNLOADING SPECIFIC PRODUCT IMAGES FROM UNSPLASH")
    print("=" * 70)
    print("✨ Each product gets an image matching its type")
    print("   Example: Laptop → Laptop photo, Speaker → Speaker photo")
    print("=" * 70)
    print()
    
    products = Product.objects.all()
    total = products.count()
    
    print(f"Total products to update: {total}\n")
    
    for index, product in enumerate(products, 1):
        print(f"[{index}/{total}]")
        try:
            update_product_with_specific_image(product)
        except Exception as e:
            print(f"      ❌ Error: {str(e)}\n")
    
    print("=" * 70)
    print(f"🎉 COMPLETE! Updated {total} products with specific images")
    print("=" * 70)
    print("\n💡 View your products at: http://127.0.0.1:8000/products/")
    print("   Each product now has an image matching its type!")

if __name__ == '__main__':
    update_all_products()
