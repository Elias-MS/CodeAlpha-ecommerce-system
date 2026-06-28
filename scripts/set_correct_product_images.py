"""
Set CORRECT Product Images
Creates product-specific images with icons and product representation
"""

import os
import django

os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'ecommerce.settings')
django.setup()

from products.models import Product
from PIL import Image, ImageDraw, ImageFont
from io import BytesIO
from django.core.files.base import ContentFile

def get_product_type_and_color(product_name, category_name):
    """Determine product type and appropriate color scheme"""
    
    name_lower = product_name.lower()
    
    # Electronics - Dark backgrounds with tech colors
    if 'laptop' in name_lower or 'notebook' in name_lower:
        return 'LAPTOP', (30, 30, 40), (100, 150, 255), '💻'
    elif 'phone' in name_lower or 'smartphone' in name_lower or 'mobile' in name_lower:
        return 'SMARTPHONE', (20, 20, 30), (80, 200, 255), '📱'
    elif 'speaker' in name_lower:
        return 'SPEAKER', (40, 40, 50), (255, 100, 100), '🔊'
    elif 'headphone' in name_lower or 'earbuds' in name_lower or 'earphone' in name_lower:
        return 'HEADPHONES', (30, 30, 40), (200, 100, 255), '🎧'
    elif 'keyboard' in name_lower:
        return 'KEYBOARD', (35, 35, 45), (150, 200, 255), '⌨️'
    elif 'mouse' in name_lower:
        return 'MOUSE', (30, 30, 40), (255, 150, 100), '🖱️'
    elif 'watch' in name_lower or 'smartwatch' in name_lower:
        return 'SMARTWATCH', (20, 20, 30), (255, 200, 100), '⌚'
    elif 'webcam' in name_lower or 'camera' in name_lower:
        return 'CAMERA', (25, 25, 35), (255, 180, 100), '📷'
    elif 'monitor' in name_lower or 'display' in name_lower:
        return 'MONITOR', (30, 30, 40), (100, 180, 255), '🖥️'
    elif 'tablet' in name_lower:
        return 'TABLET', (25, 25, 35), (150, 150, 255), '📱'
    
    # Clothing
    elif 'jeans' in name_lower or 'denim' in name_lower:
        return 'JEANS', (40, 60, 120), (100, 140, 200), '👖'
    elif 't-shirt' in name_lower or 'tshirt' in name_lower or 'tee' in name_lower:
        return 'T-SHIRT', (240, 240, 245), (100, 100, 120), '👕'
    elif 'jacket' in name_lower or 'coat' in name_lower:
        return 'JACKET', (60, 60, 70), (120, 120, 140), '🧥'
    elif 'shoes' in name_lower or 'sneakers' in name_lower:
        return 'SNEAKERS', (250, 250, 255), (80, 80, 100), '👟'
    
    # Home & Kitchen
    elif 'blender' in name_lower:
        return 'BLENDER', (240, 240, 245), (200, 50, 50), '🥤'
    elif 'coffee' in name_lower:
        return 'COFFEE MAKER', (60, 40, 30), (200, 150, 100), '☕'
    elif 'air fryer' in name_lower or 'fryer' in name_lower:
        return 'AIR FRYER', (240, 240, 245), (255, 100, 50), '🍳'
    elif 'vacuum' in name_lower:
        return 'VACUUM', (240, 240, 245), (100, 100, 120), '🧹'
    
    # Fashion
    elif 'bag' in name_lower or 'backpack' in name_lower:
        return 'BAG', (80, 60, 40), (150, 120, 90), '🎒'
    elif 'sunglasses' in name_lower or 'glasses' in name_lower:
        return 'SUNGLASSES', (20, 20, 25), (255, 200, 100), '🕶️'
    
    # Books
    elif category_name == 'Books':
        return 'BOOK', (139, 69, 19), (210, 180, 140), '📚'
    
    # Default
    else:
        return product_name.split()[0].upper()[:15], (100, 100, 120), (200, 200, 220), '📦'

def create_product_specific_image(product_name, category_name, price, width=800, height=800):
    """Create a product-specific image with icon and styling"""
    
    product_type, bg_color, accent_color, emoji = get_product_type_and_color(product_name, category_name)
    
    # Create image with product-appropriate background
    image = Image.new('RGB', (width, height), color=bg_color)
    draw = ImageDraw.Draw(image)
    
    # Add subtle gradient
    for y in range(height):
        ratio = y / height
        r = int(bg_color[0] * (1 - ratio * 0.3) + accent_color[0] * (ratio * 0.3))
        g = int(bg_color[1] * (1 - ratio * 0.3) + accent_color[1] * (ratio * 0.3))
        b = int(bg_color[2] * (1 - ratio * 0.3) + accent_color[2] * (ratio * 0.3))
        draw.rectangle([(0, y), (width, y + 1)], fill=(r, g, b))
    
    # Load fonts
    try:
        font_huge = ImageFont.truetype("C:/Windows/Fonts/arial.ttf", 200)
        font_large = ImageFont.truetype("C:/Windows/Fonts/arialbd.ttf", 80)
        font_medium = ImageFont.truetype("C:/Windows/Fonts/arialbd.ttf", 50)
        font_small = ImageFont.truetype("C:/Windows/Fonts/arial.ttf", 40)
    except:
        font_huge = ImageFont.load_default()
        font_large = ImageFont.load_default()
        font_medium = ImageFont.load_default()
        font_small = ImageFont.load_default()
    
    # Draw large emoji icon in center
    try:
        bbox = draw.textbbox((0, 0), emoji, font=font_huge)
        emoji_width = bbox[2] - bbox[0]
        emoji_x = (width - emoji_width) // 2
        emoji_y = height // 3
        draw.text((emoji_x, emoji_y), emoji, font=font_huge)
    except:
        pass
    
    # Draw product type text
    bbox = draw.textbbox((0, 0), product_type, font=font_large)
    type_width = bbox[2] - bbox[0]
    type_x = (width - type_width) // 2
    type_y = height // 2 + 100
    
    # Text with shadow
    draw.text((type_x + 3, type_y + 3), product_type, fill=(0, 0, 0, 100), font=font_large)
    draw.text((type_x, type_y), product_type, fill='white', font=font_large)
    
    # Draw product name at bottom
    words = product_name.split()
    lines = []
    current_line = []
    
    for word in words:
        current_line.append(word)
        test_line = ' '.join(current_line)
        bbox = draw.textbbox((0, 0), test_line, font=font_small)
        if bbox[2] - bbox[0] > width - 100:
            current_line.pop()
            if current_line:
                lines.append(' '.join(current_line))
            current_line = [word]
    
    if current_line:
        lines.append(' '.join(current_line))
    
    lines = lines[:2]
    
    name_y = height - 150
    for i, line in enumerate(lines):
        bbox = draw.textbbox((0, 0), line, font=font_small)
        line_width = bbox[2] - bbox[0]
        line_x = (width - line_width) // 2
        draw.text((line_x, name_y + i * 50), line, fill='white', font=font_small)
    
    # Draw price badge
    price_text = f"${price}"
    bbox = draw.textbbox((0, 0), price_text, font=font_medium)
    price_width = bbox[2] - bbox[0]
    badge_width = price_width + 60
    badge_x = (width - badge_width) // 2
    badge_y = height - 80
    
    draw.rounded_rectangle(
        [(badge_x, badge_y), (badge_x + badge_width, badge_y + 70)],
        radius=35,
        fill=(255, 106, 0)
    )
    
    price_x = badge_x + 30
    draw.text((price_x, badge_y + 10), price_text, fill='white', font=font_medium)
    
    return image


def update_all_products_with_correct_images():
    """Update all products with correct product-specific images"""
    
    print("=" * 70)
    print("📸 SETTING CORRECT PRODUCT IMAGES")
    print("=" * 70)
    print("Creating product-specific images with icons and styling")
    print("Laptop → 💻, Phone → 📱, Speaker → 🔊, etc.")
    print("=" * 70)
    print()
    
    products = Product.objects.all()
    total = products.count()
    
    print(f"Total products: {total}\n")
    
    updated = 0
    
    for index, product in enumerate(products, 1):
        try:
            print(f"[{index}/{total}] {product.name[:45]}")
            print(f"  Category: {product.category.name}")
            
            # Create product-specific image
            image = create_product_specific_image(
                product.name,
                product.category.name,
                product.price
            )
            
            # Save to product
            image_io = BytesIO()
            image.save(image_io, format='JPEG', quality=92)
            image_io.seek(0)
            
            filename = f"product_{product.id}.jpg"
            product.image.save(filename, ContentFile(image_io.read()), save=True)
            
            updated += 1
            print(f"  ✅ Saved!\n")
            
        except Exception as e:
            print(f"  ❌ Error: {str(e)}\n")
    
    print("=" * 70)
    print(f"🎉 COMPLETE!")
    print(f"   ✅ Updated: {updated}/{total}")
    print("=" * 70)
    print(f"\n🚀 View your products: http://127.0.0.1:8000/products/")
    print("   Each product now has a CORRECT product-specific image!")
    print("\nExamples:")
    print("  • Laptop products → 💻 LAPTOP icon")
    print("  • Phone products → 📱 SMARTPHONE icon")
    print("  • Speaker products → 🔊 SPEAKER icon")
    print("  • Headphones → 🎧 HEADPHONES icon")
    print("  • Jeans → 👖 JEANS icon")
    print("  • T-Shirt → 👕 T-SHIRT icon")
    print("  • And more!")

if __name__ == '__main__':
    update_all_products_with_correct_images()
