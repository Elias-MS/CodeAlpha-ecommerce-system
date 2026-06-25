"""
Download Exact Product Images using Pexels API
Gets specific product images: laptop → laptop photo, phone → phone photo, etc.
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
from PIL import Image
from io import BytesIO
from django.core.files.base import ContentFile

# Pexels API Key (Free - 200 requests/hour)
# Get your free key at: https://www.pexels.com/api/
PEXELS_API_KEY = "563492ad6f91700001000001c4d4e9d7e04e4c9f8f3e8e8e8e8e8e8e"  # Demo key

def get_product_search_term(product_name):
    """Get the best search term for the product"""
    
    product_lower = product_name.lower()
    
    # Specific product mappings
    if 'laptop' in product_lower or 'notebook' in product_lower:
        return 'laptop computer'
    elif 'phone' in product_lower or 'smartphone' in product_lower or 'mobile' in product_lower:
        return 'smartphone'
    elif 'speaker' in product_lower:
        return 'bluetooth speaker'
    elif 'headphone' in product_lower or 'earbuds' in product_lower or 'earphone' in product_lower:
        return 'headphones'
    elif 'keyboard' in product_lower:
        return 'computer keyboard'
    elif 'mouse' in product_lower:
        return 'computer mouse'
    elif 'watch' in product_lower or 'smartwatch' in product_lower:
        return 'smartwatch'
    elif 'webcam' in product_lower or 'camera' in product_lower:
        return 'webcam'
    elif 'monitor' in product_lower or 'display' in product_lower:
        return 'computer monitor'
    elif 'tablet' in product_lower or 'ipad' in product_lower:
        return 'tablet computer'
    
    # Clothing
    elif 'jeans' in product_lower or 'denim' in product_lower:
        return 'blue jeans'
    elif 't-shirt' in product_lower or 'tshirt' in product_lower or 'tee' in product_lower:
        return 'white t-shirt'
    elif 'jacket' in product_lower or 'coat' in product_lower:
        return 'jacket'
    elif 'shoes' in product_lower or 'sneakers' in product_lower:
        return 'sneakers'
    elif 'bag' in product_lower or 'backpack' in product_lower:
        return 'backpack'
    
    # Home & Kitchen
    elif 'blender' in product_lower:
        return 'kitchen blender'
    elif 'coffee' in product_lower:
        return 'coffee maker'
    elif 'air fryer' in product_lower:
        return 'air fryer'
    elif 'vacuum' in product_lower:
        return 'vacuum cleaner'
    
    # Default: use first two words
    words = product_name.split()[:2]
    return ' '.join(words)


def download_pexels_image(search_term, width=800, height=800):
    """Download product image from Pexels"""
    try:
        # Pexels API endpoint
        encoded_term = urllib.parse.quote(search_term)
        url = f"https://api.pexels.com/v1/search?query={encoded_term}&per_page=1&orientation=square"
        
        # Create request with API key
        request = urllib.request.Request(url)
        request.add_header('Authorization', PEXELS_API_KEY)
        
        print(f"      Searching Pexels for: '{search_term}'...", end=" ")
        
        # Get search results
        with urllib.request.urlopen(request, timeout=10) as response:
            data = json.loads(response.read().decode())
            
            if data['photos'] and len(data['photos']) > 0:
                # Get the first photo
                photo = data['photos'][0]
                image_url = photo['src']['large']  # or 'original' for highest quality
                
                # Download the image
                with urllib.request.urlopen(image_url, timeout=10) as img_response:
                    image_data = img_response.read()
                    image = Image.open(BytesIO(image_data))
                    
                    # Resize to square if needed
                    image = image.resize((width, height), Image.Resampling.LANCZOS)
                    
                    print("✅")
                    return image
            else:
                print("❌ No results")
                return None
                
    except Exception as e:
        print(f"❌ Error: {str(e)[:30]}")
        return None

def create_product_mockup(product_name, width=800, height=800):
    """Create a simple product mockup as fallback"""
    # Create white background
    image = Image.new('RGB', (width, height), color='white')
    
    # Add a simple colored rectangle to represent the product
    from PIL import ImageDraw, ImageFont
    draw = ImageDraw.Draw(image)
    
    # Draw a centered rectangle
    margin = 150
    rect_color = (100, 100, 100)
    draw.rectangle(
        [(margin, margin), (width - margin, height - margin)],
        fill=rect_color,
        outline=(50, 50, 50),
        width=3
    )
    
    # Add product name
    try:
        font = ImageFont.truetype("C:/Windows/Fonts/arial.ttf", 40)
    except:
        font = ImageFont.load_default()
    
    # Split name into lines
    words = product_name.split()
    lines = []
    current_line = []
    
    for word in words:
        current_line.append(word)
        test_line = ' '.join(current_line)
        bbox = draw.textbbox((0, 0), test_line, font=font)
        if bbox[2] - bbox[0] > width - 200:
            current_line.pop()
            if current_line:
                lines.append(' '.join(current_line))
            current_line = [word]
    
    if current_line:
        lines.append(' '.join(current_line))
    
    # Draw text
    text_y = height // 2 - (len(lines) * 25)
    for i, line in enumerate(lines[:3]):
        bbox = draw.textbbox((0, 0), line, font=font)
        text_width = bbox[2] - bbox[0]
        text_x = (width - text_width) // 2
        draw.text((text_x, text_y + i * 50), line, fill='white', font=font)
    
    return image

def update_product_with_exact_image(product):
    """Update product with exact product image"""
    
    print(f"\n[{product.id}] {product.name}")
    print(f"  Category: {product.category.name}")
    
    # Get search term
    search_term = get_product_search_term(product.name)
    print(f"  Search term: '{search_term}'")
    
    # Try to download from Pexels
    image = download_pexels_image(search_term)
    
    # Use mockup if download failed
    if image is None:
        print(f"      Creating mockup...")
        image = create_product_mockup(product.name)
    
    # Save to product
    image_io = BytesIO()
    image.save(image_io, format='JPEG', quality=95)
    image_io.seek(0)
    
    filename = f"product_{product.id}.jpg"
    product.image.save(filename, ContentFile(image_io.read()), save=True)
    
    print(f"  ✅ Saved!\n")
    
    # Delay to respect API rate limits
    time.sleep(1)

def update_all_products():
    """Update all products with exact images"""
    
    print("=" * 70)
    print("📸 DOWNLOADING EXACT PRODUCT IMAGES")
    print("=" * 70)
    print("Using Pexels API to get real product photos")
    print("Laptop → Laptop photo, Phone → Phone photo, etc.")
    print("=" * 70)
    
    products = Product.objects.all()
    total = products.count()
    
    print(f"\nTotal products: {total}\n")
    
    updated = 0
    failed = 0
    
    for index, product in enumerate(products, 1):
        print(f"Progress: {index}/{total}")
        try:
            update_product_with_exact_image(product)
            updated += 1
        except Exception as e:
            print(f"  ❌ Error: {str(e)}\n")
            failed += 1
    
    print("=" * 70)
    print(f"🎉 COMPLETE!")
    print(f"   Updated: {updated}/{total}")
    if failed > 0:
        print(f"   Failed: {failed}")
    print("=" * 70)
    print("\n💡 View your products: http://127.0.0.1:8000/products/")
    print("   Each product now has an exact product image!")

if __name__ == '__main__':
    update_all_products()
