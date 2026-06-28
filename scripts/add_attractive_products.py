"""
Add Attractive Products to E-Commerce Store
This script adds a variety of products across different categories
"""

import os
import django

# Setup Django
os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'ecommerce.settings')
django.setup()

from products.models import Category, Product
from decimal import Decimal

def add_products():
    """Add attractive products to the store"""
    
    print("🛍️ Adding Attractive Products to Store...")
    print("=" * 60)
    
    # Get or create categories
    electronics, _ = Category.objects.get_or_create(
        name='Electronics',
        defaults={'description': 'Latest electronic gadgets and devices'}
    )
    
    fashion, _ = Category.objects.get_or_create(
        name='Fashion',
        defaults={'description': 'Trendy clothing and accessories'}
    )
    
    home, _ = Category.objects.get_or_create(
        name='Home & Kitchen',
        defaults={'description': 'Home appliances and kitchen essentials'}
    )
    
    sports, _ = Category.objects.get_or_create(
        name='Sports & Fitness',
        defaults={'description': 'Sports equipment and fitness gear'}
    )
    
    books, _ = Category.objects.get_or_create(
        name='Books',
        defaults={'description': 'Books, magazines, and educational materials'}
    )
    
    beauty, _ = Category.objects.get_or_create(
        name='Beauty & Personal Care',
        defaults={'description': 'Beauty products and personal care items'}
    )
    
    toys, _ = Category.objects.get_or_create(
        name='Toys & Games',
        defaults={'description': 'Toys, games, and entertainment for all ages'}
    )
    
    automotive, _ = Category.objects.get_or_create(
        name='Automotive',
        defaults={'description': 'Car accessories and automotive products'}
    )
    
    print(f"✅ Categories created/verified")
    
    # Electronics Products
    electronics_products = [
        {
            'name': 'Wireless Bluetooth Earbuds Pro',
            'description': 'Premium wireless earbuds with active noise cancellation, 30-hour battery life, and crystal-clear sound quality. Perfect for music lovers and professionals.',
            'price': Decimal('79.99'),
            'stock': 150,
            'rating': Decimal('4.8')
        },
        {
            'name': 'Smart Watch Series 7',
            'description': 'Advanced fitness tracker with heart rate monitor, GPS, sleep tracking, and 50+ sport modes. Water-resistant up to 50m.',
            'price': Decimal('199.99'),
            'stock': 85,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Portable Power Bank 20000mAh',
            'description': 'High-capacity power bank with fast charging, dual USB ports, and LED display. Charges your phone 5-6 times.',
            'price': Decimal('34.99'),
            'stock': 200,
            'rating': Decimal('4.6')
        },
        {
            'name': '4K Webcam with Ring Light',
            'description': 'Professional 4K webcam with built-in ring light, auto-focus, and noise-canceling microphone. Perfect for streaming and video calls.',
            'price': Decimal('89.99'),
            'stock': 75,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Wireless Gaming Mouse RGB',
            'description': 'High-precision gaming mouse with 16000 DPI, customizable RGB lighting, and 7 programmable buttons. Ergonomic design.',
            'price': Decimal('49.99'),
            'stock': 120,
            'rating': Decimal('4.7')
        },
        {
            'name': 'USB-C Hub 7-in-1 Adapter',
            'description': 'Multi-port USB-C hub with HDMI 4K, USB 3.0, SD card reader, and 100W power delivery. Compatible with all USB-C devices.',
            'price': Decimal('39.99'),
            'stock': 180,
            'rating': Decimal('4.6')
        },
        {
            'name': 'LED Desk Lamp with Wireless Charging',
            'description': 'Modern LED desk lamp with adjustable brightness, color temperature control, and built-in wireless phone charger.',
            'price': Decimal('45.99'),
            'stock': 95,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Mechanical Gaming Keyboard RGB',
            'description': 'Professional mechanical keyboard with blue switches, full RGB backlighting, and anti-ghosting. Perfect for gaming and typing.',
            'price': Decimal('69.99'),
            'stock': 110,
            'rating': Decimal('4.8')
        },
    ]
    
    # Fashion Products
    fashion_products = [
        {
            'name': 'Premium Cotton T-Shirt Pack (3-Pack)',
            'description': 'Soft, breathable 100% cotton t-shirts in classic colors. Pre-shrunk and machine washable. Available in S-XXL.',
            'price': Decimal('29.99'),
            'stock': 250,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Slim Fit Denim Jeans',
            'description': 'Stylish slim-fit jeans with stretch fabric for comfort. Classic 5-pocket design. Available in multiple washes.',
            'price': Decimal('49.99'),
            'stock': 180,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Leather Crossbody Bag',
            'description': 'Genuine leather crossbody bag with adjustable strap and multiple compartments. Perfect for daily use.',
            'price': Decimal('79.99'),
            'stock': 90,
            'rating': Decimal('4.8')
        },
        {
            'name': 'Casual Sneakers Unisex',
            'description': 'Comfortable casual sneakers with cushioned insole and breathable mesh. Perfect for walking and everyday wear.',
            'price': Decimal('59.99'),
            'stock': 200,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Wool Blend Winter Coat',
            'description': 'Warm and stylish wool blend coat with button closure and side pockets. Perfect for cold weather.',
            'price': Decimal('129.99'),
            'stock': 65,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Silk Scarf Collection',
            'description': 'Luxurious 100% silk scarf with elegant patterns. Soft, lightweight, and versatile. Multiple colors available.',
            'price': Decimal('34.99'),
            'stock': 150,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Sports Watch Chronograph',
            'description': 'Stylish sports watch with chronograph function, date display, and water resistance. Stainless steel case.',
            'price': Decimal('89.99'),
            'stock': 85,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Leather Belt Premium',
            'description': 'Genuine leather belt with reversible design (black/brown). Durable buckle and adjustable length.',
            'price': Decimal('39.99'),
            'stock': 175,
            'rating': Decimal('4.7')
        },
    ]
    
    # Home & Kitchen Products
    home_products = [
        {
            'name': 'Stainless Steel Cookware Set 10-Piece',
            'description': 'Professional-grade stainless steel cookware set with non-stick coating. Includes pots, pans, and lids. Dishwasher safe.',
            'price': Decimal('149.99'),
            'stock': 55,
            'rating': Decimal('4.8')
        },
        {
            'name': 'Electric Kettle 1.7L Fast Boil',
            'description': 'Rapid-boil electric kettle with auto shut-off, boil-dry protection, and cordless design. Boils water in 3 minutes.',
            'price': Decimal('34.99'),
            'stock': 140,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Memory Foam Pillow Set (2-Pack)',
            'description': 'Ergonomic memory foam pillows with cooling gel and breathable cover. Perfect for side and back sleepers.',
            'price': Decimal('49.99'),
            'stock': 120,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Robot Vacuum Cleaner Smart',
            'description': 'Intelligent robot vacuum with app control, auto-charging, and multiple cleaning modes. Works on all floor types.',
            'price': Decimal('199.99'),
            'stock': 45,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Air Fryer 5.5L Digital',
            'description': 'Large capacity air fryer with digital controls, 8 preset programs, and non-stick basket. Healthy cooking made easy.',
            'price': Decimal('89.99'),
            'stock': 95,
            'rating': Decimal('4.8')
        },
        {
            'name': 'Bamboo Cutting Board Set (3-Piece)',
            'description': 'Eco-friendly bamboo cutting boards in 3 sizes. Knife-friendly, durable, and easy to clean.',
            'price': Decimal('29.99'),
            'stock': 180,
            'rating': Decimal('4.6')
        },
        {
            'name': 'LED String Lights 33ft',
            'description': 'Warm white LED string lights with remote control, 8 lighting modes, and timer function. Perfect for decoration.',
            'price': Decimal('19.99'),
            'stock': 250,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Coffee Maker Programmable 12-Cup',
            'description': 'Programmable coffee maker with auto-brew, pause-and-serve, and keep-warm function. Makes perfect coffee every time.',
            'price': Decimal('59.99'),
            'stock': 110,
            'rating': Decimal('4.5')
        },
    ]
    
    # Sports & Fitness Products
    sports_products = [
        {
            'name': 'Yoga Mat Extra Thick 10mm',
            'description': 'Premium yoga mat with non-slip surface, extra cushioning, and carrying strap. Perfect for yoga, pilates, and stretching.',
            'price': Decimal('34.99'),
            'stock': 160,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Resistance Bands Set (5-Pack)',
            'description': 'Professional resistance bands with 5 different resistance levels, handles, and door anchor. Complete home gym.',
            'price': Decimal('24.99'),
            'stock': 200,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Adjustable Dumbbells 50lbs Pair',
            'description': 'Space-saving adjustable dumbbells with quick-change weight system. Replaces 15 sets of weights.',
            'price': Decimal('149.99'),
            'stock': 65,
            'rating': Decimal('4.8')
        },
        {
            'name': 'Jump Rope Speed with Counter',
            'description': 'Professional speed jump rope with digital counter, adjustable length, and comfortable handles. Great for cardio.',
            'price': Decimal('14.99'),
            'stock': 220,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Foam Roller for Muscle Recovery',
            'description': 'High-density foam roller for deep tissue massage and muscle recovery. Includes exercise guide.',
            'price': Decimal('29.99'),
            'stock': 145,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Sports Water Bottle 32oz Insulated',
            'description': 'Stainless steel insulated water bottle keeps drinks cold for 24h or hot for 12h. Leak-proof and BPA-free.',
            'price': Decimal('24.99'),
            'stock': 190,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Gym Bag Duffel with Shoe Compartment',
            'description': 'Spacious gym bag with separate shoe compartment, wet pocket, and multiple pockets. Water-resistant material.',
            'price': Decimal('39.99'),
            'stock': 125,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Ab Roller Wheel with Knee Pad',
            'description': 'Sturdy ab roller wheel with non-slip handles and thick knee pad. Perfect for core strengthening exercises.',
            'price': Decimal('19.99'),
            'stock': 175,
            'rating': Decimal('4.6')
        },
    ]
    
    # Books Products
    books_products = [
        {
            'name': 'The Complete Guide to Python Programming',
            'description': 'Comprehensive Python programming book for beginners to advanced. Includes practical examples and projects.',
            'price': Decimal('39.99'),
            'stock': 95,
            'rating': Decimal('4.8')
        },
        {
            'name': 'Mindfulness and Meditation Handbook',
            'description': 'Practical guide to mindfulness and meditation with daily exercises and techniques for stress relief.',
            'price': Decimal('24.99'),
            'stock': 130,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Cookbook: Healthy Meals in 30 Minutes',
            'description': '100+ quick and healthy recipes with nutritional information. Perfect for busy lifestyles.',
            'price': Decimal('29.99'),
            'stock': 110,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Business Strategy and Leadership',
            'description': 'Essential guide to business strategy, leadership skills, and organizational management.',
            'price': Decimal('34.99'),
            'stock': 85,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Digital Photography Masterclass',
            'description': 'Complete photography course covering camera settings, composition, lighting, and post-processing.',
            'price': Decimal('44.99'),
            'stock': 75,
            'rating': Decimal('4.8')
        },
        {
            'name': 'Learn Spanish in 30 Days',
            'description': 'Intensive Spanish language course with audio CD, exercises, and cultural insights.',
            'price': Decimal('27.99'),
            'stock': 120,
            'rating': Decimal('4.6')
        },
    ]
    
    # Beauty & Personal Care Products
    beauty_products = [
        {
            'name': 'Vitamin C Serum Anti-Aging',
            'description': 'Powerful vitamin C serum with hyaluronic acid for brightening, anti-aging, and hydration. Suitable for all skin types.',
            'price': Decimal('29.99'),
            'stock': 180,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Electric Facial Cleansing Brush',
            'description': 'Waterproof facial cleansing brush with 3 speed settings and soft silicone bristles. Deep cleans pores.',
            'price': Decimal('39.99'),
            'stock': 140,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Hair Dryer Professional Ionic',
            'description': 'Professional ionic hair dryer with 3 heat settings, cool shot button, and concentrator nozzle. Fast drying.',
            'price': Decimal('49.99'),
            'stock': 95,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Makeup Brush Set 15-Piece',
            'description': 'Professional makeup brush set with synthetic bristles, ergonomic handles, and storage case.',
            'price': Decimal('34.99'),
            'stock': 160,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Natural Face Mask Set (5-Pack)',
            'description': 'Variety pack of natural face masks for different skin concerns. Includes clay, charcoal, and hydrating masks.',
            'price': Decimal('24.99'),
            'stock': 200,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Electric Toothbrush Sonic',
            'description': 'Sonic electric toothbrush with 5 cleaning modes, 2-minute timer, and 30-day battery life. Includes 4 brush heads.',
            'price': Decimal('59.99'),
            'stock': 115,
            'rating': Decimal('4.8')
        },
        {
            'name': 'Nail Care Kit Professional',
            'description': 'Complete nail care kit with electric nail file, cuticle pusher, and 6 attachments. Salon-quality manicure at home.',
            'price': Decimal('29.99'),
            'stock': 145,
            'rating': Decimal('4.5')
        },
    ]
    
    # Toys & Games Products
    toys_products = [
        {
            'name': 'Building Blocks Set 1000 Pieces',
            'description': 'Creative building blocks set with 1000 colorful pieces. Compatible with major brands. Includes storage box.',
            'price': Decimal('39.99'),
            'stock': 125,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Remote Control Car 4WD Off-Road',
            'description': 'High-speed RC car with 4-wheel drive, rechargeable battery, and durable design. Reaches 30mph.',
            'price': Decimal('69.99'),
            'stock': 85,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Educational STEM Robot Kit',
            'description': 'Build and program your own robot. Includes 400+ parts and coding app. Perfect for ages 8+.',
            'price': Decimal('89.99'),
            'stock': 65,
            'rating': Decimal('4.8')
        },
        {
            'name': 'Board Game Family Collection',
            'description': 'Classic board game collection including chess, checkers, backgammon, and more. Fun for all ages.',
            'price': Decimal('34.99'),
            'stock': 150,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Art Supplies Set for Kids',
            'description': 'Complete art set with crayons, markers, colored pencils, paints, and paper. Includes carrying case.',
            'price': Decimal('29.99'),
            'stock': 180,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Puzzle 1000 Pieces Scenic',
            'description': 'Beautiful scenic jigsaw puzzle with 1000 pieces. High-quality printing and precision-cut pieces.',
            'price': Decimal('19.99'),
            'stock': 200,
            'rating': Decimal('4.7')
        },
    ]
    
    # Automotive Products
    automotive_products = [
        {
            'name': 'Car Phone Mount Magnetic',
            'description': 'Strong magnetic car phone mount with 360° rotation and adjustable arm. Works with all phones.',
            'price': Decimal('19.99'),
            'stock': 220,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Dash Cam 1080P Front and Rear',
            'description': 'Dual dash cam with 1080P recording, night vision, G-sensor, and loop recording. Includes 32GB SD card.',
            'price': Decimal('79.99'),
            'stock': 95,
            'rating': Decimal('4.7')
        },
        {
            'name': 'Car Vacuum Cleaner Portable',
            'description': 'Powerful portable car vacuum with HEPA filter, LED light, and multiple attachments. Cordless design.',
            'price': Decimal('44.99'),
            'stock': 130,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Tire Pressure Gauge Digital',
            'description': 'Accurate digital tire pressure gauge with LCD display and auto shut-off. Essential for car maintenance.',
            'price': Decimal('14.99'),
            'stock': 250,
            'rating': Decimal('4.6')
        },
        {
            'name': 'Car Air Purifier Ionizer',
            'description': 'Compact car air purifier with ionizer, removes odors, smoke, and allergens. USB powered.',
            'price': Decimal('29.99'),
            'stock': 165,
            'rating': Decimal('4.5')
        },
        {
            'name': 'Jump Starter Power Bank 12000mAh',
            'description': 'Portable jump starter for cars with 12000mAh battery, LED flashlight, and USB charging ports.',
            'price': Decimal('69.99'),
            'stock': 85,
            'rating': Decimal('4.8')
        },
    ]
    
    # Add all products
    all_products = [
        (electronics, electronics_products),
        (fashion, fashion_products),
        (home, home_products),
        (sports, sports_products),
        (books, books_products),
        (beauty, beauty_products),
        (toys, toys_products),
        (automotive, automotive_products),
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
    for category in Category.objects.all():
        count = Product.objects.filter(category=category).count()
        print(f"  • {category.name}: {count} products")
    
    print("\n✅ All done! Your store is now fully stocked!")

if __name__ == '__main__':
    add_products()
