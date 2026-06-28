"""
Add Product Images to All Products
Creates attractive placeholder images for all products
"""

import os
import django

# Setup Django
os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'ecommerce.settings')
django.setup()

from products.models import Product, Category
from PIL import Image, ImageDraw, ImageFont
from io import BytesIO
from django.core.files.base import ContentFile

def create_product_image(product_name, category_name, width=800, height=800):
    """Create an attractive product image with gradient background"""
    
    # Category color schemes
    category_colors = {
        'Electronics': [(59, 130, 246), (147, 51, 234)],  # Blue to Purple
        'Fashion': [(236, 72, 153), (251, 146, 60)],  # Pink to Orange
        'Home & Kitchen': [(34, 197, 94), (59, 130, 246)],  # Green to Blue
        'Sports & Fitness': [(239, 68, 68), (251, 146, 60)],  # Red to Orange
        'Books': [(168, 85, 247), (236, 72, 153)],  # Purple to Pink
        'Beauty & Personal Care': [(236, 72, 153), (168, 85, 247)],  # Pink to Purple
        'Toys & Games': [(251, 146, 60), (251, 191, 36)],  # Orange to Yellow
        'Automotive': [(71, 85, 105), (100, 116, 139)],  # Dark Gray to Gray
        'Office Supplies': [(59, 130, 246), (34, 197, 94)],  # Blue to Green
        'Pet Supplies': [(251, 146, 60), (239, 68, 68)],  # Orange to Red
        'Garden & Outdoor': [(34, 197, 94), (132, 204, 22)],  # Green to Lime
        'Clothing': [(147, 51, 234), (59, 130, 246)],  # Purple to Blue
    }
    
    # Get colors for category
    colors = category_colors.get(category_name, [(99, 102, 241), (168, 85, 247)])
    
    # Create image with gradient
    image = Image.new('RGB', (width, height), color='white')
    draw = ImageDraw.Draw(image)
    
    # Draw gradient background
    for y in range(height):
        ratio = y / height
        r = int(colors[0][0] * (1 - ratio) + colors[1][0] * ratio)
        g = int(colors[0][1] * (1 - ratio) + colors[1][1] * ratio)
        b = int(colors[0][2] * (1 - ratio) + colors[1][2] * ratio)
        draw.rectangle([(0, y), (width, y + 1)], fill=(r, g, b))
    
    # Add white overlay for better text visibility
    overlay = Image.new('RGBA', (width, height), (255, 255, 255, 180))
    image = Image.alpha_composite(image.convert('RGBA'), overlay).convert('RGB')
    
    # Add decorative elements
    draw = ImageDraw.Draw(image)
    
    # Draw circles
    for i in range(3):
        x = width // 4 + i * width // 4
        y = height // 4 + i * height // 6
        radius = 80 + i * 20
        draw.ellipse(
            [(x - radius, y - radius), (x + radius, y + radius)],
            outline=colors[i % 2],
            width=3
        )
    
    # Try to use a nice font, fallback to default
    try:
        # Try different font paths
        font_paths = [
            "C:/Windows/Fonts/arial.ttf",
            "C:/Windows/Fonts/calibri.ttf",
            "/usr/share/fonts/truetype/dejavu/DejaVuSans-Bold.ttf",
            "/System/Library/Fonts/Helvetica.ttc"
        ]
        
        font_large = None
        font_medium = None
        font_small = None
        
        for font_path in font_paths:
            if os.path.exists(font_path):
                font_large = ImageFont.truetype(font_path, 60)
                font_medium = ImageFont.truetype(font_path, 40)
                font_small = ImageFont.truetype(font_path, 30)
                break
        
        if not font_large:
            font_large = ImageFont.load_default()
            font_medium = ImageFont.load_default()
            font_small = ImageFont.load_default()
    except:
        font_large = ImageFont.load_default()
        font_medium = ImageFont.load_default()
        font_small = ImageFont.load_default()
    
    # Add category label at top
    category_text = category_name.upper()
    bbox = draw.textbbox((0, 0), category_text, font=font_small)
    category_width = bbox[2] - bbox[0]
    category_x = (width - category_width) // 2
    
    # Draw category background
    draw.rectangle(
        [(category_x - 20, 50), (category_x + category_width + 20, 100)],
        fill=colors[0]
    )
    draw.text((category_x, 60), category_text, fill='white', font=font_small)
    
    # Add product name in center
    # Split long names into multiple lines
    words = product_name.split()
    lines = []
    current_line = []
    
    for word in words:
        current_line.append(word)
        test_line = ' '.join(current_line)
        bbox = draw.textbbox((0, 0), test_line, font=font_medium)
        if bbox[2] - bbox[0] > width - 100:
            current_line.pop()
            if current_line:
                lines.append(' '.join(current_line))
            current_line = [word]
    
    if current_line:
        lines.append(' '.join(current_line))
    
    # Limit to 3 lines
    lines = lines[:3]
    
    # Draw product name
    total_height = len(lines) * 50
    start_y = (height - total_height) // 2
    
    for i, line in enumerate(lines):
        bbox = draw.textbbox((0, 0), line, font=font_medium)
        text_width = bbox[2] - bbox[0]
        text_x = (width - text_width) // 2
        text_y = start_y + i * 50
        
        # Draw text shadow
        draw.text((text_x + 2, text_y + 2), line, fill=(0, 0, 0, 100), font=font_medium)
        # Draw text
        draw.text((text_x, text_y), line, fill='#262626', font=font_medium)
    
    # Add "E-Shop" watermark at bottom
    watermark = "E-Shop"
    bbox = draw.textbbox((0, 0), watermark, font=font_small)
    watermark_width = bbox[2] - bbox[0]
    watermark_x = (width - watermark_width) // 2
    draw.text((watermark_x, height - 80), watermark, fill=colors[1], font=font_small)
    
    return image

def add_images_to_products():
    """Add images to all products"""
    
    print("🖼️  Adding Product Images...")
    print("=" * 60)
    
    products = Product.objects.all()
    total = products.count()
    updated = 0
    
    for index, product in enumerate(products, 1):
        try:
            # Create image
            image = create_product_image(product.name, product.category.name)
            
            # Save to BytesIO
            image_io = BytesIO()
            image.save(image_io, format='JPEG', quality=90)
            image_io.seek(0)
            
            # Create filename
            filename = f"{product.name.lower().replace(' ', '_')[:50]}.jpg"
            
            # Save to product
            product.image.save(filename, ContentFile(image_io.read()), save=True)
            
            updated += 1
            print(f"  [{index}/{total}] ✅ {product.name}")
            
        except Exception as e:
            print(f"  [{index}/{total}] ❌ {product.name} - Error: {str(e)}")
    
    print("\n" + "=" * 60)
    print(f"🎉 Successfully added images to {updated}/{total} products!")
    print("=" * 60)

if __name__ == '__main__':
    add_images_to_products()
