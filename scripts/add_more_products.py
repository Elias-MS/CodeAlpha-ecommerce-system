"""
Add More Attractive Products - Part 2
Adding 50+ more products to reach 120+ total products
"""

import os
import django

# Setup Django
os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'ecommerce.settings')
django.setup()

from products.models import Category, Product
from decimal import Decimal

def add_more_products():
    """Add more attractive products to the store"""
    
    print("🛍️ Adding MORE Attractive Products to Store...")
    print("=" * 60)
    
    # Get categories
    electronics = Category.objects.get(name='Electronics')
    fashion = Category.objects.get(name='Fashion')
    home = Category.objects.get(name='Home & Kitchen')
    sports = Category.objects.get(name='Sports & Fitness')
    books = Category.objects.get(name='Books')
    beauty = Category.objects.get(name='Beauty & Personal Care')
    toys = Category.objects.get(name='Toys & Games')
    automotive = Category.objects.get(name='Automotive')
    
    # Create new categories
    office, _ = Category.objects.get_or_create(
        name='Office Supplies',
        defaults={'description': 'Office supplies and stationery'}
    )
    
    pet, _ = Category.objects.get_or_create(
        name='Pet Supplies',
        defaults={'description': 'Pet food, toys, and accessories'}
    )
    
    garden, _ = Category.objects.get_or_create(
        name='Garden & Outdoor',
        defaults={'description': 'Gardening tools and outdoor equipment'}
    )
    
    print(f"✅ Categories ready")
    
    # More Electronics Products
    more_electronics = [
        {
            'name': 'Wireless Keyboard and Mouse Combo',
            'description': 'Ergonomic wireless keyboard and mouse set with long battery life. Quiet keys and precise tracking.',
            'price': Decimal('44.99'),
            'stock': 135,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Laptop Stand Adjustable Aluminum',
            'description': 'Ergonomic laptop stand with 6 adjustable heights. Improves posture and cooling. Fits 10-17 inch laptops.',
            'price': Decimal('29.99'),
            'stock': 170,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Bluetooth Speaker Waterproof',
            'description': 'Portable Bluetooth speaker with 360° sound, 24-hour battery, and IPX7 waterproof rating.',
            'price': Decimal('54.99'),
            'stock': 145,
            'rating': Decimal('4.8')
        },
        {
            'name': 'USB Flash Drive 128GB (3-Pack)',
            'description': 'High-speed USB 3.0 flash drives with keychain design. Transfer speeds up to 100MB/s.',
            'price': Decimal('24.99'),
            'stock': 220,
            'rating': Decimal('4.5')
        },
        {
            'name': 'HDMI Cable 4K 10ft (2-Pack)',
            'description': 'Premium HDMI 2.0 cables supporting 4K@60Hz, HDR, and ARC. Gold-plated connectors.',
            'price': Decimal('16.99'),
            'stock': 280,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Phone Screen Protector Tempered Glass',
            'description': 'Ultra-clear tempered glass screen protector with easy installation kit. 9H hardness.',
            'price': Decimal('9.99'),
            'stock': 350,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Laptop Cooling Pad with 6 Fans',
            'description': 'Powerful cooling pad with 6 quiet fans, adjustable height, and dual USB ports.',
            'price': Decimal('34.99'),
            'stock': 115,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Smart LED Light Bulbs (4-Pack)',
            'description': 'WiFi smart bulbs with 16 million colors, voice control, and scheduling. Works with Alexa.',
            'price': Decimal('39.99'),
            'stock': 160,
            'rating': Decimal('4.7')
        },
    ]
    
    # More Fashion Products
    more_fashion = [
        {
            'name': 'Sunglasses Polarized UV Protection',
            'description': 'Stylish polarized sunglasses with 100% UV protection. Lightweight and durable frame.',
            'price': Decimal('24.99'),
            'stock': 190,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Baseball Cap Adjustable Cotton',
            'description': 'Classic baseball cap in 100% cotton with adjustable strap. Available in multiple colors.',
            'price': Decimal('14.99'),
            'stock': 240,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Backpack Laptop Travel 40L',
            'description': 'Large capacity travel backpack with laptop compartment, USB charging port, and water-resistant material.',
            'price': Decimal('49.99'),
            'stock': 125,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Wallet Genuine Leather RFID Blocking',
            'description': 'Slim leather wallet with RFID protection, multiple card slots, and ID window.',
            'price': Decimal('29.99'),
            'stock': 175,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Socks Athletic Cushioned (6-Pack)',
            'description': 'Comfortable athletic socks with arch support and moisture-wicking fabric. Fits shoe size 6-12.',
            'price': Decimal('19.99'),
            'stock': 260,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Gloves Winter Touchscreen Compatible',
            'description': 'Warm winter gloves with touchscreen fingertips. Soft fleece lining and anti-slip palm.',
            'price': Decimal('16.99'),
            'stock': 200,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Tie Set with Pocket Square (3-Pack)',
            'description': 'Classic silk ties with matching pocket squares. Perfect for business and formal occasions.',
            'price': Decimal('34.99'),
            'stock': 140,
            'rating': Decimal('4.7')
        },
    ]
    
    # More Home & Kitchen Products
    more_home = [
        {
            'name': 'Blender High-Speed 1200W',
            'description': 'Professional blender with 1200W motor, 6 blades, and 10 speed settings. Makes smoothies in seconds.',
            'price': Decimal('79.99'),
            'stock': 95,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Knife Set Kitchen 15-Piece',
            'description': 'Professional knife set with stainless steel blades, ergonomic handles, and wooden block.',
            'price': Decimal('69.99'),
            'stock': 110,
            'rating': Decimal('4.8')
        },
        {
            'name': 'Microwave Oven 1.1 Cu.Ft',
            'description': 'Compact microwave with 1000W power, 10 power levels, and express cooking buttons.',
            'price': Decimal('89.99'),
            'stock': 75,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Dish Drying Rack Stainless Steel',
            'description': 'Large capacity dish rack with utensil holder, cutting board slot, and drip tray.',
            'price': Decimal('34.99'),
            'stock': 155,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Food Storage Containers Set (20-Piece)',
            'description': 'BPA-free plastic containers with snap-lock lids. Microwave, freezer, and dishwasher safe.',
            'price': Decimal('29.99'),
            'stock': 185,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Shower Curtain Waterproof with Hooks',
            'description': 'Mildew-resistant shower curtain with reinforced holes and 12 rust-proof hooks.',
            'price': Decimal('19.99'),
            'stock': 210,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Bath Towel Set 6-Piece Egyptian Cotton',
            'description': 'Luxury Egyptian cotton towels - 2 bath, 2 hand, 2 face towels. Super soft and absorbent.',
            'price': Decimal('44.99'),
            'stock': 130,
            'rating': Decimal('4.8')
        },
        {
            'name': 'Trash Can Touchless 13 Gallon',
            'description': 'Automatic sensor trash can with soft-close lid, fingerprint-proof finish, and odor filter.',
            'price': Decimal('59.99'),
            'stock': 85,
            'rating': Decimal('4.6')
        },
    ]
    
    # More Sports & Fitness Products
    more_sports = [
        {
            'name': 'Treadmill Folding Electric',
            'description': 'Compact folding treadmill with LCD display, 12 programs, and 300lb weight capacity.',
            'price': Decimal('299.99'),
            'stock': 35,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Exercise Bike Stationary Indoor',
            'description': 'Quiet magnetic resistance bike with adjustable seat, LCD monitor, and tablet holder.',
            'price': Decimal('199.99'),
            'stock': 55,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Kettlebell Set 3-Piece (10, 15, 20 lbs)',
            'description': 'Vinyl-coated kettlebells with wide handles. Perfect for strength training and cardio.',
            'price': Decimal('79.99'),
            'stock': 95,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Pull-Up Bar Doorway Mounted',
            'description': 'Heavy-duty pull-up bar with multiple grip positions. No screws needed, fits most doorways.',
            'price': Decimal('29.99'),
            'stock': 165,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Ankle Weights Adjustable (Pair)',
            'description': 'Comfortable ankle weights with adjustable straps. Each weight adjustable from 1-5 lbs.',
            'price': Decimal('24.99'),
            'stock': 180,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Yoga Block and Strap Set',
            'description': 'High-density foam yoga blocks (2-pack) with 8ft cotton strap. Perfect for beginners.',
            'price': Decimal('19.99'),
            'stock': 200,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Fitness Tracker Smart Band',
            'description': 'Activity tracker with heart rate monitor, sleep tracking, and 14-day battery life.',
            'price': Decimal('39.99'),
            'stock': 145,
            'rating': Decimal('4.6')
        },
    ]
    
    # More Books Products
    more_books = [
        {
            'name': 'Web Development Complete Course',
            'description': 'Learn HTML, CSS, JavaScript, React, and Node.js. Includes 50+ projects and exercises.',
            'price': Decimal('49.99'),
            'stock': 85,
            'rating': Decimal('4.8')
        },
        {
            'name': 'Financial Freedom Guide',
            'description': 'Practical guide to investing, saving, and building wealth. Includes worksheets and calculators.',
            'price': Decimal('29.99'),
            'stock': 115,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Gardening for Beginners',
            'description': 'Complete guide to growing vegetables, herbs, and flowers. Includes planting calendar.',
            'price': Decimal('24.99'),
            'stock': 130,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Drawing and Sketching Masterclass',
            'description': 'Learn to draw portraits, landscapes, and still life. Step-by-step tutorials included.',
            'price': Decimal('34.99'),
            'stock': 95,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Yoga and Meditation Guide',
            'description': 'Comprehensive yoga guide with 100+ poses, breathing exercises, and meditation techniques.',
            'price': Decimal('27.99'),
            'stock': 120,
            'rating': Decimal('4.6')
        },
    ]
    
    # More Beauty & Personal Care Products
    more_beauty = [
        {
            'name': 'Retinol Cream Anti-Aging Night',
            'description': 'Powerful retinol night cream with hyaluronic acid. Reduces wrinkles and fine lines.',
            'price': Decimal('34.99'),
            'stock': 155,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Jade Roller and Gua Sha Set',
            'description': 'Natural jade facial roller and gua sha tool for lymphatic drainage and facial massage.',
            'price': Decimal('19.99'),
            'stock': 185,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Hair Straightener Ceramic Flat Iron',
            'description': 'Professional ceramic flat iron with adjustable temperature, auto shut-off, and dual voltage.',
            'price': Decimal('44.99'),
            'stock': 125,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Perfume Gift Set Women (3-Pack)',
            'description': 'Luxury perfume collection with floral, fruity, and oriental scents. 30ml each bottle.',
            'price': Decimal('39.99'),
            'stock': 140,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Manicure Pedicure Kit 18-Piece',
            'description': 'Professional nail care kit with stainless steel tools and leather case.',
            'price': Decimal('24.99'),
            'stock': 170,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Body Lotion Moisturizing 16oz',
            'description': 'Hydrating body lotion with shea butter and vitamin E. Non-greasy formula.',
            'price': Decimal('16.99'),
            'stock': 210,
            'rating': Decimal('4.5')
        },
    ]
    
    # More Toys & Games Products
    more_toys = [
        {
            'name': 'LEGO Compatible Building Set 500 Pieces',
            'description': 'Creative building blocks with instruction booklet. Compatible with major brands.',
            'price': Decimal('29.99'),
            'stock': 155,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Drone with Camera 1080P',
            'description': 'Beginner-friendly drone with HD camera, altitude hold, and one-key takeoff/landing.',
            'price': Decimal('89.99'),
            'stock': 75,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Coloring Books for Adults (4-Pack)',
            'description': 'Stress-relief coloring books with intricate designs. Includes mandala, nature, and patterns.',
            'price': Decimal('19.99'),
            'stock': 190,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Playing Cards Deck Set (12-Pack)',
            'description': 'Standard playing cards in bulk. Perfect for parties, games, and magic tricks.',
            'price': Decimal('14.99'),
            'stock': 240,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Stuffed Animal Plush Teddy Bear',
            'description': 'Soft and cuddly teddy bear, 18 inches tall. Hypoallergenic and machine washable.',
            'price': Decimal('24.99'),
            'stock': 165,
            'rating': Decimal('4.7')
        },
    ]
    
    # More Automotive Products
    more_automotive = [
        {
            'name': 'Car Seat Covers Full Set',
            'description': 'Universal fit car seat covers with airbag compatible design. Easy to install and clean.',
            'price': Decimal('49.99'),
            'stock': 105,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Car Floor Mats All-Weather (4-Piece)',
            'description': 'Heavy-duty rubber floor mats with raised edges. Protects from mud, snow, and spills.',
            'price': Decimal('39.99'),
            'stock': 135,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Windshield Sun Shade Reflective',
            'description': 'Foldable sun shade keeps car cool. Fits most vehicles, easy to install and store.',
            'price': Decimal('12.99'),
            'stock': 230,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Car Organizer Trunk Storage',
            'description': 'Collapsible trunk organizer with multiple compartments and non-slip bottom.',
            'price': Decimal('24.99'),
            'stock': 175,
            'rating': Decimal('4.6')
        },
        {
            'name': 'LED Headlight Bulbs H11 (Pair)',
            'description': 'Super bright LED headlight bulbs, 6000K white light, 50,000 hour lifespan.',
            'price': Decimal('34.99'),
            'stock': 145,
            'rating': Decimal('4.7')
        },
    ]
    
    # Office Supplies Products
    office_products = [
        {
            'name': 'Office Chair Ergonomic Mesh',
            'description': 'Comfortable mesh office chair with lumbar support, adjustable height, and armrests.',
            'price': Decimal('129.99'),
            'stock': 65,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Desk Organizer Set 5-Piece',
            'description': 'Bamboo desk organizer with pen holder, paper tray, and drawer. Keeps desk tidy.',
            'price': Decimal('29.99'),
            'stock': 155,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Printer Paper 500 Sheets',
            'description': 'Bright white printer paper, 8.5x11 inches, 20lb weight. Jam-free performance.',
            'price': Decimal('9.99'),
            'stock': 300,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Sticky Notes Assorted Colors (12-Pack)',
            'description': 'Colorful sticky notes in various sizes. 100 sheets per pad, strong adhesive.',
            'price': Decimal('12.99'),
            'stock': 250,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Stapler Heavy Duty with Staples',
            'description': 'Professional stapler staples up to 50 sheets. Includes 5000 staples.',
            'price': Decimal('16.99'),
            'stock': 200,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Whiteboard Magnetic 36x24 inches',
            'description': 'Dry erase whiteboard with aluminum frame, marker tray, and mounting hardware.',
            'price': Decimal('39.99'),
            'stock': 95,
            'rating': Decimal('4.7')
        },
        {
            'name': 'File Folders Letter Size (100-Pack)',
            'description': 'Manila file folders with 1/3 cut tabs. Durable and acid-free.',
            'price': Decimal('14.99'),
            'stock': 220,
            'rating': Decimal('4.5')
        },
    ]
    
    # Pet Supplies Products
    pet_products = [
        {
            'name': 'Dog Food Dry Premium 30lbs',
            'description': 'High-protein dog food with real chicken, vegetables, and no artificial flavors.',
            'price': Decimal('49.99'),
            'stock': 85,
            'rating': Decimal('4.8')
        },
        {
            'name': 'Cat Litter Box Self-Cleaning',
            'description': 'Automatic self-cleaning litter box with odor control and waste drawer.',
            'price': Decimal('149.99'),
            'stock': 45,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Pet Carrier Airline Approved',
            'description': 'Soft-sided pet carrier with ventilation, shoulder strap, and storage pocket.',
            'price': Decimal('34.99'),
            'stock': 125,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Dog Leash and Collar Set',
            'description': 'Durable nylon leash (6ft) and adjustable collar with reflective stitching.',
            'price': Decimal('19.99'),
            'stock': 180,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Cat Scratching Post with Toys',
            'description': 'Tall scratching post covered in sisal rope with hanging toys and platform.',
            'price': Decimal('39.99'),
            'stock': 110,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Pet Water Fountain 2L',
            'description': 'Automatic pet water fountain with filter, quiet pump, and LED light.',
            'price': Decimal('29.99'),
            'stock': 145,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Dog Toys Variety Pack (10-Piece)',
            'description': 'Assorted dog toys including rope, balls, and squeaky toys. Durable and safe.',
            'price': Decimal('24.99'),
            'stock': 165,
            'rating': Decimal('4.6')
        },
    ]
    
    # Garden & Outdoor Products
    garden_products = [
        {
            'name': 'Garden Tool Set 10-Piece',
            'description': 'Complete gardening tool set with trowel, pruner, rake, and carrying bag.',
            'price': Decimal('39.99'),
            'stock': 115,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Garden Hose 50ft Expandable',
            'description': 'Lightweight expandable hose with 9-pattern spray nozzle. No kinks or tangles.',
            'price': Decimal('29.99'),
            'stock': 140,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Plant Pots Ceramic Set (6-Pack)',
            'description': 'Decorative ceramic pots with drainage holes and saucers. Various sizes.',
            'price': Decimal('34.99'),
            'stock': 125,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Solar Garden Lights (12-Pack)',
            'description': 'Waterproof solar pathway lights with auto on/off. No wiring needed.',
            'price': Decimal('39.99'),
            'stock': 155,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Lawn Sprinkler Oscillating',
            'description': 'Adjustable sprinkler covers up to 3600 sq ft. Durable metal construction.',
            'price': Decimal('24.99'),
            'stock': 175,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Garden Kneeler and Seat',
            'description': 'Foldable garden kneeler with tool pouches. Converts to seat. Supports 330lbs.',
            'price': Decimal('44.99'),
            'stock': 95,
            'rating': Decimal('4.7')
        },
    ]
    
    # Add all products
    all_products = [
        (electronics, more_electronics),
        (fashion, more_fashion),
        (home, more_home),
        (sports, more_sports),
        (books, more_books),
        (beauty, more_beauty),
        (toys, more_toys),
        (automotive, more_automotive),
        (office, office_products),
        (pet, pet_products),
        (garden, garden_products),
    ]
    
    total_added = 0
    
    for category, products in all_products:
        print(f"\n📦 Adding {category.name} products...")
        for product_data in products:
            product, created = Product.objects.get_or_create(
                name=product_data['name'],
                category=category,
                defaults={
                    'description': product_data['description'],
                    'price': product_data['price'],
                    'stock': product_data['stock'],
                    'rating': product_data['rating'],
                }
            )
            if created:
                total_added += 1
                print(f"  ✅ {product.name} - ${product.price}")
            else:
                print(f"  ⏭️  {product.name} (already exists)")
    
    print("\n" + "=" * 60)
    print(f"🎉 Successfully added {total_added} new products!")
    print(f"📊 Total products in store: {Product.objects.count()}")
    print("=" * 60)
    
    # Display category summary
    print("\n📊 Products by Category:")
    for category in Category.objects.all().order_by('name'):
        count = Product.objects.filter(category=category).count()
        print(f"  • {category.name}: {count} products")
    
    print("\n✅ All done! Your store now has 120+ products!")

if __name__ == '__main__':
    add_more_products()
