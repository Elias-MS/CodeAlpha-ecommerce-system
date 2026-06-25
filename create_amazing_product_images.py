"""
Create Amazing, Attractive Product Images
Generates realistic, professional product images that attract users
"""

import os
import django

# Setup Django
os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'ecommerce.settings')
django.setup()

from products.models import Product, Category
from PIL import Image, ImageDraw, ImageFont, ImageFilter
from io import BytesIO
from django.core.files.base import ContentFile
import random

def create_amazing_product_image(product_name, category_name, price, width=1000, height=1000):
    """Create an amazing, attractive product image with professional styling"""
    
    # Category-specific styling
    category_styles = {
        'Electronics': {
            'bg_colors': [(15, 23, 42), (30, 41, 59)],  # Dark blue gradient
            'accent': (59, 130, 246),  # Bright blue
            'icon': '📱',
            'pattern': 'tech'
        },
        'Fashion': {
            'bg_colors': [(251, 207, 232), (252, 231, 243)],  # Pink gradient
            'accent': (236, 72, 153),  # Hot pink
            'icon': '👗',
            'pattern': 'fashion'
        },
        'Home & Kitchen': {
            'bg_colors': [(236, 253, 245), (209, 250, 229)],  # Mint gradient
            'accent': (16, 185, 129),  # Green
            'icon': '🏠',
            'pattern': 'home'
        },
        'Sports & Fitness': {
            'bg_colors': [(254, 242, 242), (254, 226, 226)],  # Light red gradient
            'accent': (239, 68, 68),  # Red
            'icon': '⚽',
            'pattern': 'sport'
        },
        'Books': {
            'bg_colors': [(237, 233, 254), (221, 214, 254)],  # Purple gradient
            'accent': (139, 92, 246),  # Purple
            'icon': '📚',
            'pattern': 'book'
        },
        'Beauty & Personal Care': {
            'bg_colors': [(252, 231, 243), (251, 207, 232)],  # Pink gradient
            'accent': (219, 39, 119),  # Deep pink
            'icon': '💄',
            'pattern': 'beauty'
        },
        'Toys & Games': {
            'bg_colors': [(254, 249, 195), (254, 240, 138)],  # Yellow gradient
            'accent': (245, 158, 11),  # Orange
            'icon': '🎮',
            'pattern': 'toy'
        },
        'Automotive': {
            'bg_colors': [(241, 245, 249), (226, 232, 240)],  # Gray gradient
            'accent': (71, 85, 105),  # Dark gray
            'icon': '🚗',
            'pattern': 'auto'
        },
        'Office Supplies': {
            'bg_colors': [(219, 234, 254), (191, 219, 254)],  # Light blue gradient
            'accent': (37, 99, 235),  # Blue
            'icon': '📎',
            'pattern': 'office'
        },
        'Pet Supplies': {
            'bg_colors': [(254, 243, 199), (253, 230, 138)],  # Warm yellow gradient
            'accent': (217, 119, 6),  # Orange
            'icon': '🐾',
            'pattern': 'pet'
        },
        'Garden & Outdoor': {
            'bg_colors': [(220, 252, 231), (187, 247, 208)],  # Green gradient
            'accent': (34, 197, 94),  # Green
            'icon': '🌿',
            'pattern': 'garden'
        },
        'Clothing': {
            'bg_colors': [(224, 231, 255), (199, 210, 254)],  # Indigo gradient
            'accent': (99, 102, 241),  # Indigo
            'icon': '👕',
            'pattern': 'clothing'
        },
    }
    
    # Get style for category
    style = category_styles.get(category_name, {
        'bg_colors': [(243, 244, 246), (229, 231, 235)],
        'accent': (107, 114, 128),
        'icon': '🛍️',
        'pattern': 'default'
    })
    
    # Create base image with gradient
    image = Image.new('RGB', (width, height), color='white')
    draw = ImageDraw.Draw(image)
    
    # Draw smooth gradient background
    for y in range(height):
        ratio = y / height
        r = int(style['bg_colors'][0][0] * (1 - ratio) + style['bg_colors'][1][0] * ratio)
        g = int(style['bg_colors'][0][1] * (1 - ratio) + style['bg_colors'][1][1] * ratio)
        b = int(style['bg_colors'][0][2] * (1 - ratio) + style['bg_colors'][1][2] * ratio)
        draw.rectangle([(0, y), (width, y + 1)], fill=(r, g, b))
    
    # Add decorative pattern based on category
    draw_decorative_pattern(draw, width, height, style)
    
    # Add white card in center for product info
    card_margin = 100
    card_x1 = card_margin
    card_y1 = card_margin
    card_x2 = width - card_margin
    card_y2 = height - card_margin
    
    # Draw card shadow
    shadow_offset = 15
    draw.rounded_rectangle(
        [(card_x1 + shadow_offset, card_y1 + shadow_offset), (card_x2 + shadow_offset, card_y2 + shadow_offset)],
        radius=30,
        fill=(0, 0, 0, 30)
    )
    
    # Draw white card
    draw.rounded_rectangle(
        [(card_x1, card_y1), (card_x2, card_y2)],
        radius=30,
        fill='white'
    )
    
    # Load fonts
    try:
        font_paths = [
            "C:/Windows/Fonts/arial.ttf",
            "C:/Windows/Fonts/calibri.ttf",
            "C:/Windows/Fonts/segoeui.ttf",
        ]
        
        font_huge = None
        font_large = None
        font_medium = None
        font_small = None
        
        for font_path in font_paths:
            if os.path.exists(font_path):
                font_huge = ImageFont.truetype(font_path, 120)
                font_large = ImageFont.truetype(font_path, 70)
                font_medium = ImageFont.truetype(font_path, 50)
                font_small = ImageFont.truetype(font_path, 40)
                break
        
        if not font_huge:
            font_huge = ImageFont.load_default()
            font_large = ImageFont.load_default()
            font_medium = ImageFont.load_default()
            font_small = ImageFont.load_default()
    except:
        font_huge = ImageFont.load_default()
        font_large = ImageFont.load_default()
        font_medium = ImageFont.load_default()
        font_small = ImageFont.load_default()
    
    # Draw category icon at top
    icon_text = style['icon']
    try:
        bbox = draw.textbbox((0, 0), icon_text, font=font_huge)
        icon_width = bbox[2] - bbox[0]
        icon_x = (width - icon_width) // 2
        draw.text((icon_x, card_y1 + 80), icon_text, font=font_huge)
    except:
        pass
    
    # Draw category badge
    category_text = category_name.upper()
    bbox = draw.textbbox((0, 0), category_text, font=font_small)
    badge_width = bbox[2] - bbox[0] + 60
    badge_height = 60
    badge_x = (width - badge_width) // 2
    badge_y = card_y1 + 250
    
    # Draw badge background
    draw.rounded_rectangle(
        [(badge_x, badge_y), (badge_x + badge_width, badge_y + badge_height)],
        radius=30,
        fill=style['accent']
    )
    
    # Draw category text
    text_x = badge_x + 30
    text_y = badge_y + 10
    draw.text((text_x, text_y), category_text, fill='white', font=font_small)
    
    # Draw product name (multi-line if needed)
    words = product_name.split()
    lines = []
    current_line = []
    
    for word in words:
        current_line.append(word)
        test_line = ' '.join(current_line)
        bbox = draw.textbbox((0, 0), test_line, font=font_large)
        if bbox[2] - bbox[0] > (card_x2 - card_x1 - 100):
            current_line.pop()
            if current_line:
                lines.append(' '.join(current_line))
            current_line = [word]
    
    if current_line:
        lines.append(' '.join(current_line))
    
    # Limit to 3 lines
    lines = lines[:3]
    
    # Draw product name centered
    name_start_y = badge_y + 120
    for i, line in enumerate(lines):
        bbox = draw.textbbox((0, 0), line, font=font_large)
        text_width = bbox[2] - bbox[0]
        text_x = (width - text_width) // 2
        text_y = name_start_y + i * 80
        
        # Draw text with slight shadow
        draw.text((text_x + 3, text_y + 3), line, fill=(0, 0, 0, 50), font=font_large)
        draw.text((text_x, text_y), line, fill='#1f2937', font=font_large)
    
    # Draw price tag at bottom
    price_text = f"${price}"
    bbox = draw.textbbox((0, 0), price_text, font=font_large)
    price_width = bbox[2] - bbox[0] + 80
    price_height = 90
    price_x = (width - price_width) // 2
    price_y = card_y2 - 200
    
    # Draw price background (accent color)
    draw.rounded_rectangle(
        [(price_x, price_y), (price_x + price_width, price_y + price_height)],
        radius=45,
        fill=style['accent']
    )
    
    # Draw price text
    price_text_x = price_x + 40
    price_text_y = price_y + 10
    draw.text((price_text_x, price_text_y), price_text, fill='white', font=font_large)
    
    # Add "NEW" badge in corner
    new_badge_size = 120
    new_x = card_x2 - new_badge_size - 50
    new_y = card_y1 + 50
    
    draw.ellipse(
        [(new_x, new_y), (new_x + new_badge_size, new_y + new_badge_size)],
        fill=(239, 68, 68)
    )
    
    new_text = "NEW"
    bbox = draw.textbbox((0, 0), new_text, font=font_small)
    new_text_width = bbox[2] - bbox[0]
    new_text_height = bbox[3] - bbox[1]
    new_text_x = new_x + (new_badge_size - new_text_width) // 2
    new_text_y = new_y + (new_badge_size - new_text_height) // 2 - 10
    draw.text((new_text_x, new_text_y), new_text, fill='white', font=font_small)
    
    # Add star rating
    stars = "⭐⭐⭐⭐⭐"
    bbox = draw.textbbox((0, 0), stars, font=font_medium)
    stars_width = bbox[2] - bbox[0]
    stars_x = (width - stars_width) // 2
    stars_y = price_y - 100
    draw.text((stars_x, stars_y), stars, font=font_medium)
    
    return image

def draw_decorative_pattern(draw, width, height, style):
    """Draw decorative background pattern"""
    accent = style['accent']
    pattern = style['pattern']
    
    # Draw subtle circles in background
    for i in range(8):
        x = random.randint(0, width)
        y = random.randint(0, height)
        radius = random.randint(50, 150)
        
        # Very transparent circles
        draw.ellipse(
            [(x - radius, y - radius), (x + radius, y + radius)],
            outline=accent + (30,),
            width=3
        )
    
    # Draw corner decorations
    corner_size = 200
    
    # Top left
    draw.arc([(0, 0), (corner_size, corner_size)], 0, 90, fill=accent, width=5)
    
    # Top right
    draw.arc([(width - corner_size, 0), (width, corner_size)], 90, 180, fill=accent, width=5)
    
    # Bottom left
    draw.arc([(0, height - corner_size), (corner_size, height)], 270, 360, fill=accent, width=5)
    
    # Bottom right
    draw.arc([(width - corner_size, height - corner_size), (width, height)], 180, 270, fill=accent, width=5)

def add_amazing_images_to_products():
    """Add amazing images to all products"""
    
    print("🎨 Creating Amazing Product Images...")
    print("=" * 70)
    print("✨ Professional, attractive images that will wow your users!")
    print("=" * 70)
    
    products = Product.objects.all()
    total = products.count()
    updated = 0
    
    for index, product in enumerate(products, 1):
        try:
            # Create amazing image
            image = create_amazing_product_image(
                product.name,
                product.category.name,
                product.price
            )
            
            # Save to BytesIO
            image_io = BytesIO()
            image.save(image_io, format='JPEG', quality=95)
            image_io.seek(0)
            
            # Create filename
            filename = f"product_{product.id}.jpg"
            
            # Save to product
            product.image.save(filename, ContentFile(image_io.read()), save=True)
            
            updated += 1
            print(f"  [{index}/{total}] ✅ {product.name[:50]}")
            
        except Exception as e:
            print(f"  [{index}/{total}] ❌ {product.name[:50]} - Error: {str(e)}")
    
    print("\n" + "=" * 70)
    print(f"🎉 Successfully created {updated}/{total} amazing product images!")
    print("=" * 70)
    print("\n💡 Features of the new images:")
    print("   • Professional gradient backgrounds")
    print("   • Category-specific color schemes")
    print("   • Large category icons (emojis)")
    print("   • Clean white product cards")
    print("   • Prominent price tags")
    print("   • 5-star ratings")
    print("   • 'NEW' badges")
    print("   • Decorative patterns")
    print("   • High resolution (1000x1000px)")
    print("   • 95% JPEG quality")
    print("\n🚀 Your products now look AMAZING and will attract users!")

if __name__ == '__main__':
    add_amazing_images_to_products()
