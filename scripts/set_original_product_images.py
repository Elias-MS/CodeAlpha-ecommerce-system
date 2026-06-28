"""
Set Original Product Images
Uses direct CDN URLs to download real product images
"""

import os
import django
import urllib.request
import time

os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'ecommerce.settings')
django.setup()

from products.models import Product
from PIL import Image
from io import BytesIO
from django.core.files.base import ContentFile

# Product-specific image URLs from free CDNs
PRODUCT_IMAGE_URLS = {
    # Electronics
    'laptop': 'https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=800&h=800&fit=crop',
    'phone': 'https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=800&h=800&fit=crop',
    'speaker': 'https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?w=800&h=800&fit=crop',
    'headphones': 'https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=800&h=800&fit=crop',
    'keyboard': 'https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=800&h=800&fit=crop',
    'mouse': 'https://images.unsplash.com/photo-1527864550417-7fd91fc51a46?w=800&h=800&fit=crop',
    'watch': 'https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=800&h=800&fit=crop',
    'camera': 'https://images.unsplash.com/photo-1526170375885-4d8ecf77b99f?w=800&h=800&fit=crop',
    'led': 'https://images.unsplash.com/photo-1550985616-10810253b84d?w=800&h=800&fit=crop',
    'light': 'https://images.unsplash.com/photo-1550985616-10810253b84d?w=800&h=800&fit=crop',
    'cable': 'https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=800&h=800&fit=crop',
    'usb': 'https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=800&h=800&fit=crop',
    'powerbank': 'https://images.unsplash.com/photo-1609091839311-d5365f9ff1c5?w=800&h=800&fit=crop',
    'tablet': 'https://images.unsplash.com/photo-1544244015-0df4b3ffc6b0?w=800&h=800&fit=crop',
    'monitor': 'https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?w=800&h=800&fit=crop',
    'dashcam': 'https://images.unsplash.com/photo-1519641471654-76ce0107ad1b?w=800&h=800&fit=crop',
    
    # Clothing
    'jeans': 'https://images.unsplash.com/photo-1542272604-787c3835535d?w=800&h=800&fit=crop',
    'tshirt': 'https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?w=800&h=800&fit=crop',
    'jacket': 'https://images.unsplash.com/photo-1551028719-00167b16eac5?w=800&h=800&fit=crop',
    'shoes': 'https://images.unsplash.com/photo-1460353581641-37baddab0fa2?w=800&h=800&fit=crop',
    'socks': 'https://images.unsplash.com/photo-1586350977771-b3b0abd50c82?w=800&h=800&fit=crop',
    'gloves': 'https://images.unsplash.com/photo-1520903920243-00d872a2d1c9?w=800&h=800&fit=crop',
    'cap': 'https://images.unsplash.com/photo-1588850561407-ed78c282e89b?w=800&h=800&fit=crop',
    'belt': 'https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=800&h=800&fit=crop',
    'scarf': 'https://images.unsplash.com/photo-1520903920243-00d872a2d1c9?w=800&h=800&fit=crop',
    'wallet': 'https://images.unsplash.com/photo-1627123424574-724758594e93?w=800&h=800&fit=crop',
    
    # Home & Kitchen
    'blender': 'https://images.unsplash.com/photo-1585515320310-259814833e62?w=800&h=800&fit=crop',
    'coffee': 'https://images.unsplash.com/photo-1517668808822-9ebb02f2a0e6?w=800&h=800&fit=crop',
    'airfryer': 'https://images.unsplash.com/photo-1585515320310-259814833e62?w=800&h=800&fit=crop',
    'fryer': 'https://images.unsplash.com/photo-1585515320310-259814833e62?w=800&h=800&fit=crop',
    'kettle': 'https://images.unsplash.com/photo-1517668808822-9ebb02f2a0e6?w=800&h=800&fit=crop',
    'vacuum': 'https://images.unsplash.com/photo-1558317374-067fb5f30001?w=800&h=800&fit=crop',
    'microwave': 'https://images.unsplash.com/photo-1585659722983-3a675dabf23d?w=800&h=800&fit=crop',
    'knife': 'https://images.unsplash.com/photo-1593618998160-e34014e67546?w=800&h=800&fit=crop',
    'cookware': 'https://images.unsplash.com/photo-1556909114-f6e7ad7d3136?w=800&h=800&fit=crop',
    'pillow': 'https://images.unsplash.com/photo-1584100936595-c0654b55a2e2?w=800&h=800&fit=crop',
    'towel': 'https://images.unsplash.com/photo-1622290291468-a28f7a7dc6a8?w=800&h=800&fit=crop',
    'trash': 'https://images.unsplash.com/photo-1610557892470-55d9e80c0bce?w=800&h=800&fit=crop',
    'shower': 'https://images.unsplash.com/photo-1620626011761-996317b8d101?w=800&h=800&fit=crop',
    'storage': 'https://images.unsplash.com/photo-1610557892470-55d9e80c0bce?w=800&h=800&fit=crop',
    'container': 'https://images.unsplash.com/photo-1610557892470-55d9e80c0bce?w=800&h=800&fit=crop',
    'cutting': 'https://images.unsplash.com/photo-1556909114-f6e7ad7d3136?w=800&h=800&fit=crop',
    'board': 'https://images.unsplash.com/photo-1556909114-f6e7ad7d3136?w=800&h=800&fit=crop',
    
    # Fashion
    'bag': 'https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=800&h=800&fit=crop',
    'sunglasses': 'https://images.unsplash.com/photo-1511499767150-a48a237f0083?w=800&h=800&fit=crop',
    'tie': 'https://images.unsplash.com/photo-1507679799987-c73779587ccf?w=800&h=800&fit=crop',
    
    # Books
    'book': 'https://images.unsplash.com/photo-1495446815901-a7297e633e8d?w=800&h=800&fit=crop',
    'guide': 'https://images.unsplash.com/photo-1495446815901-a7297e633e8d?w=800&h=800&fit=crop',
    'handbook': 'https://images.unsplash.com/photo-1495446815901-a7297e633e8d?w=800&h=800&fit=crop',
    'drawing': 'https://images.unsplash.com/photo-1513364776144-60967b0f800f?w=800&h=800&fit=crop',
    'sketching': 'https://images.unsplash.com/photo-1513364776144-60967b0f800f?w=800&h=800&fit=crop',
    'photography': 'https://images.unsplash.com/photo-1452587925148-ce544e77e70d?w=800&h=800&fit=crop',
    'business': 'https://images.unsplash.com/photo-1507842217343-583bb7270b66?w=800&h=800&fit=crop',
    'strategy': 'https://images.unsplash.com/photo-1507842217343-583bb7270b66?w=800&h=800&fit=crop',
    'leadership': 'https://images.unsplash.com/photo-1507842217343-583bb7270b66?w=800&h=800&fit=crop',
    'python': 'https://images.unsplash.com/photo-1526374965328-7f61d4dc18c5?w=800&h=800&fit=crop',
    'programming': 'https://images.unsplash.com/photo-1526374965328-7f61d4dc18c5?w=800&h=800&fit=crop',
    'development': 'https://images.unsplash.com/photo-1526374965328-7f61d4dc18c5?w=800&h=800&fit=crop',
    'course': 'https://images.unsplash.com/photo-1495446815901-a7297e633e8d?w=800&h=800&fit=crop',
    
    # Sports
    'dumbbell': 'https://images.unsplash.com/photo-1517836357463-d25dfeac3438?w=800&h=800&fit=crop',
    'yoga': 'https://images.unsplash.com/photo-1544367567-0f2fcb009e0b?w=800&h=800&fit=crop',
    'fitness': 'https://images.unsplash.com/photo-1517836357463-d25dfeac3438?w=800&h=800&fit=crop',
    'bottle': 'https://images.unsplash.com/photo-1602143407151-7111542de6e8?w=800&h=800&fit=crop',
    'resistance': 'https://images.unsplash.com/photo-1598289431512-b97b0917affc?w=800&h=800&fit=crop',
    'band': 'https://images.unsplash.com/photo-1598289431512-b97b0917affc?w=800&h=800&fit=crop',
    'foam': 'https://images.unsplash.com/photo-1601574968106-b312ac309953?w=800&h=800&fit=crop',
    'roller': 'https://images.unsplash.com/photo-1601574968106-b312ac309953?w=800&h=800&fit=crop',
    'jump': 'https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?w=800&h=800&fit=crop',
    'rope': 'https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?w=800&h=800&fit=crop',
    'pullup': 'https://images.unsplash.com/photo-1534438327276-14e5300c3a48?w=800&h=800&fit=crop',
    'bar': 'https://images.unsplash.com/photo-1534438327276-14e5300c3a48?w=800&h=800&fit=crop',
    'kettlebell': 'https://images.unsplash.com/photo-1517836357463-d25dfeac3438?w=800&h=800&fit=crop',
    'bike': 'https://images.unsplash.com/photo-1576435728678-68d0fbf94e91?w=800&h=800&fit=crop',
    'treadmill': 'https://images.unsplash.com/photo-1538805060514-97d9cc17730c?w=800&h=800&fit=crop',
    'ab': 'https://images.unsplash.com/photo-1598971639058-fab3c3109a00?w=800&h=800&fit=crop',
    
    # Automotive
    'car': 'https://images.unsplash.com/photo-1492144534655-ae79c964c9d7?w=800&h=800&fit=crop',
    'tire': 'https://images.unsplash.com/photo-1486262715619-67b85e0b08d3?w=800&h=800&fit=crop',
    'windshield': 'https://images.unsplash.com/photo-1449965408869-eaa3f722e40d?w=800&h=800&fit=crop',
    'shade': 'https://images.unsplash.com/photo-1449965408869-eaa3f722e40d?w=800&h=800&fit=crop',
    
    # Toys
    'toy': 'https://images.unsplash.com/photo-1558060370-d644479cb6f7?w=800&h=800&fit=crop',
    'puzzle': 'https://images.unsplash.com/photo-1587731556938-38755b4803a6?w=800&h=800&fit=crop',
    'lego': 'https://images.unsplash.com/photo-1587654780291-39c9404d746b?w=800&h=800&fit=crop',
    'game': 'https://images.unsplash.com/photo-1610890716171-6b1bb98ffd09?w=800&h=800&fit=crop',
    'coloring': 'https://images.unsplash.com/photo-1513364776144-60967b0f800f?w=800&h=800&fit=crop',
    'drone': 'https://images.unsplash.com/photo-1473968512647-3e447244af8f?w=800&h=800&fit=crop',
    'robot': 'https://images.unsplash.com/photo-1485827404703-89b55fcc595e?w=800&h=800&fit=crop',
    'remote': 'https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=800&h=800&fit=crop',
    
    # Beauty
    'makeup': 'https://images.unsplash.com/photo-1512496015851-a90fb38ba796?w=800&h=800&fit=crop',
    'perfume': 'https://images.unsplash.com/photo-1541643600914-78b084683601?w=800&h=800&fit=crop',
    'brush': 'https://images.unsplash.com/photo-1512496015851-a90fb38ba796?w=800&h=800&fit=crop',
    'lotion': 'https://images.unsplash.com/photo-1556228578-0d85b1a4d571?w=800&h=800&fit=crop',
    'manicure': 'https://images.unsplash.com/photo-1604654894610-df63bc536371?w=800&h=800&fit=crop',
    'nail': 'https://images.unsplash.com/photo-1604654894610-df63bc536371?w=800&h=800&fit=crop',
    'straightener': 'https://images.unsplash.com/photo-1522338242992-e1a54906a8da?w=800&h=800&fit=crop',
    'hair': 'https://images.unsplash.com/photo-1522338242992-e1a54906a8da?w=800&h=800&fit=crop',
    'jade': 'https://images.unsplash.com/photo-1570172619644-dfd03ed5d881?w=800&h=800&fit=crop',
    'roller': 'https://images.unsplash.com/photo-1570172619644-dfd03ed5d881?w=800&h=800&fit=crop',
    'retinol': 'https://images.unsplash.com/photo-1556228578-0d85b1a4d571?w=800&h=800&fit=crop',
    'cream': 'https://images.unsplash.com/photo-1556228578-0d85b1a4d571?w=800&h=800&fit=crop',
    'mask': 'https://images.unsplash.com/photo-1596755389378-c31d21fd1273?w=800&h=800&fit=crop',
    'dryer': 'https://images.unsplash.com/photo-1522338242992-e1a54906a8da?w=800&h=800&fit=crop',
    'serum': 'https://images.unsplash.com/photo-1556228578-0d85b1a4d571?w=800&h=800&fit=crop',
    'vitamin': 'https://images.unsplash.com/photo-1556228578-0d85b1a4d571?w=800&h=800&fit=crop',
    
    # Office
    'paper': 'https://images.unsplash.com/photo-1586075010923-2dd4570fb338?w=800&h=800&fit=crop',
    'folder': 'https://images.unsplash.com/photo-1586075010923-2dd4570fb338?w=800&h=800&fit=crop',
    'desk': 'https://images.unsplash.com/photo-1518455027359-f3f8164ba6bd?w=800&h=800&fit=crop',
    'chair': 'https://images.unsplash.com/photo-1580480055273-228ff5388ef8?w=800&h=800&fit=crop',
    'whiteboard': 'https://images.unsplash.com/photo-1611532736597-de2d4265fba3?w=800&h=800&fit=crop',
    'stapler': 'https://images.unsplash.com/photo-1611532736597-de2d4265fba3?w=800&h=800&fit=crop',
    'sticky': 'https://images.unsplash.com/photo-1586075010923-2dd4570fb338?w=800&h=800&fit=crop',
    'notes': 'https://images.unsplash.com/photo-1586075010923-2dd4570fb338?w=800&h=800&fit=crop',
    
    # Pet
    'dog': 'https://images.unsplash.com/photo-1587300003388-59208cc962cb?w=800&h=800&fit=crop',
    'cat': 'https://images.unsplash.com/photo-1574158622682-e40e69881006?w=800&h=800&fit=crop',
    'pet': 'https://images.unsplash.com/photo-1450778869180-41d0601e046e?w=800&h=800&fit=crop',
    
    # Garden
    'garden': 'https://images.unsplash.com/photo-1416879595882-3373a0480b5b?w=800&h=800&fit=crop',
    'plant': 'https://images.unsplash.com/photo-1485955900006-10f4d324d411?w=800&h=800&fit=crop',
}

def get_product_image_url(product_name):
    """Get the appropriate image URL for a product"""
    name_lower = product_name.lower()
    
    # Electronics
    if 'laptop' in name_lower:
        return PRODUCT_IMAGE_URLS['laptop']
    elif 'phone' in name_lower or 'smartphone' in name_lower or 'mobile' in name_lower:
        return PRODUCT_IMAGE_URLS['phone']
    elif 'speaker' in name_lower:
        return PRODUCT_IMAGE_URLS['speaker']
    elif 'headphone' in name_lower or 'earbuds' in name_lower or 'earphone' in name_lower:
        return PRODUCT_IMAGE_URLS['headphones']
    elif 'keyboard' in name_lower:
        return PRODUCT_IMAGE_URLS['keyboard']
    elif 'mouse' in name_lower:
        return PRODUCT_IMAGE_URLS['mouse']
    elif 'watch' in name_lower:
        return PRODUCT_IMAGE_URLS['watch']
    elif 'camera' in name_lower or 'webcam' in name_lower:
        return PRODUCT_IMAGE_URLS['camera']
    elif 'led' in name_lower or 'light' in name_lower or 'bulb' in name_lower or 'lamp' in name_lower:
        return PRODUCT_IMAGE_URLS['led']
    elif 'cable' in name_lower or 'hdmi' in name_lower:
        return PRODUCT_IMAGE_URLS['cable']
    elif 'usb' in name_lower or 'flash' in name_lower or 'hub' in name_lower:
        return PRODUCT_IMAGE_URLS['usb']
    elif 'power bank' in name_lower or 'powerbank' in name_lower or 'charger' in name_lower:
        return PRODUCT_IMAGE_URLS['powerbank']
    elif 'tablet' in name_lower or 'ipad' in name_lower:
        return PRODUCT_IMAGE_URLS['tablet']
    elif 'monitor' in name_lower or 'display' in name_lower:
        return PRODUCT_IMAGE_URLS['monitor']
    elif 'dash' in name_lower and 'cam' in name_lower:
        return PRODUCT_IMAGE_URLS['dashcam']
    
    # Clothing & Fashion
    elif 'jeans' in name_lower or 'denim' in name_lower:
        return PRODUCT_IMAGE_URLS['jeans']
    elif 't-shirt' in name_lower or 'tshirt' in name_lower or 'tee' in name_lower:
        return PRODUCT_IMAGE_URLS['tshirt']
    elif 'jacket' in name_lower or 'coat' in name_lower:
        return PRODUCT_IMAGE_URLS['jacket']
    elif 'shoes' in name_lower or 'sneakers' in name_lower:
        return PRODUCT_IMAGE_URLS['shoes']
    elif 'socks' in name_lower:
        return PRODUCT_IMAGE_URLS['socks']
    elif 'gloves' in name_lower:
        return PRODUCT_IMAGE_URLS['gloves']
    elif 'cap' in name_lower or 'hat' in name_lower:
        return PRODUCT_IMAGE_URLS['cap']
    elif 'belt' in name_lower:
        return PRODUCT_IMAGE_URLS['belt']
    elif 'scarf' in name_lower:
        return PRODUCT_IMAGE_URLS['scarf']
    elif 'wallet' in name_lower:
        return PRODUCT_IMAGE_URLS['wallet']
    elif 'bag' in name_lower or 'backpack' in name_lower:
        return PRODUCT_IMAGE_URLS['bag']
    elif 'sunglasses' in name_lower:
        return PRODUCT_IMAGE_URLS['sunglasses']
    elif 'tie' in name_lower:
        return PRODUCT_IMAGE_URLS['tie']
    
    # Home & Kitchen
    elif 'blender' in name_lower:
        return PRODUCT_IMAGE_URLS['blender']
    elif 'coffee' in name_lower:
        return PRODUCT_IMAGE_URLS['coffee']
    elif 'fryer' in name_lower:
        return PRODUCT_IMAGE_URLS['fryer']
    elif 'kettle' in name_lower:
        return PRODUCT_IMAGE_URLS['kettle']
    elif 'vacuum' in name_lower:
        return PRODUCT_IMAGE_URLS['vacuum']
    elif 'microwave' in name_lower:
        return PRODUCT_IMAGE_URLS['microwave']
    elif 'knife' in name_lower:
        return PRODUCT_IMAGE_URLS['knife']
    elif 'cookware' in name_lower or 'pot' in name_lower or 'pan' in name_lower:
        return PRODUCT_IMAGE_URLS['cookware']
    elif 'pillow' in name_lower:
        return PRODUCT_IMAGE_URLS['pillow']
    elif 'towel' in name_lower:
        return PRODUCT_IMAGE_URLS['towel']
    elif 'trash' in name_lower or 'can' in name_lower:
        return PRODUCT_IMAGE_URLS['trash']
    elif 'shower' in name_lower or 'curtain' in name_lower:
        return PRODUCT_IMAGE_URLS['shower']
    elif 'storage' in name_lower or 'container' in name_lower:
        return PRODUCT_IMAGE_URLS['storage']
    elif 'cutting' in name_lower or ('bamboo' in name_lower and 'board' in name_lower):
        return PRODUCT_IMAGE_URLS['cutting']
    
    # Books
    elif 'book' in name_lower or 'guide' in name_lower or 'handbook' in name_lower:
        return PRODUCT_IMAGE_URLS['book']
    elif 'drawing' in name_lower or 'sketching' in name_lower or 'art' in name_lower:
        return PRODUCT_IMAGE_URLS['drawing']
    elif 'photography' in name_lower:
        return PRODUCT_IMAGE_URLS['photography']
    elif 'business' in name_lower or 'strategy' in name_lower or 'leadership' in name_lower:
        return PRODUCT_IMAGE_URLS['business']
    elif 'python' in name_lower or 'programming' in name_lower:
        return PRODUCT_IMAGE_URLS['python']
    elif 'web' in name_lower and 'development' in name_lower:
        return PRODUCT_IMAGE_URLS['development']
    elif 'course' in name_lower or 'masterclass' in name_lower:
        return PRODUCT_IMAGE_URLS['course']
    
    # Sports & Fitness
    elif 'dumbbell' in name_lower or 'weight' in name_lower:
        return PRODUCT_IMAGE_URLS['dumbbell']
    elif 'yoga' in name_lower or 'mat' in name_lower:
        return PRODUCT_IMAGE_URLS['yoga']
    elif 'fitness' in name_lower or 'tracker' in name_lower:
        return PRODUCT_IMAGE_URLS['fitness']
    elif 'bottle' in name_lower:
        return PRODUCT_IMAGE_URLS['bottle']
    elif 'resistance' in name_lower or 'band' in name_lower:
        return PRODUCT_IMAGE_URLS['resistance']
    elif 'foam' in name_lower and 'roller' in name_lower:
        return PRODUCT_IMAGE_URLS['foam']
    elif 'jump' in name_lower or 'rope' in name_lower:
        return PRODUCT_IMAGE_URLS['jump']
    elif 'pull' in name_lower and 'bar' in name_lower:
        return PRODUCT_IMAGE_URLS['pullup']
    elif 'kettlebell' in name_lower:
        return PRODUCT_IMAGE_URLS['kettlebell']
    elif 'bike' in name_lower or 'cycle' in name_lower:
        return PRODUCT_IMAGE_URLS['bike']
    elif 'treadmill' in name_lower:
        return PRODUCT_IMAGE_URLS['treadmill']
    elif 'ab' in name_lower and 'roller' in name_lower:
        return PRODUCT_IMAGE_URLS['ab']
    
    # Automotive
    elif 'car' in name_lower or 'auto' in name_lower or 'vehicle' in name_lower:
        return PRODUCT_IMAGE_URLS['car']
    elif 'tire' in name_lower:
        return PRODUCT_IMAGE_URLS['tire']
    elif 'windshield' in name_lower or 'shade' in name_lower or 'sun' in name_lower:
        return PRODUCT_IMAGE_URLS['windshield']
    
    # Toys
    elif 'toy' in name_lower or 'stuffed' in name_lower or 'teddy' in name_lower:
        return PRODUCT_IMAGE_URLS['toy']
    elif 'puzzle' in name_lower:
        return PRODUCT_IMAGE_URLS['puzzle']
    elif 'lego' in name_lower or 'blocks' in name_lower or 'building' in name_lower:
        return PRODUCT_IMAGE_URLS['lego']
    elif 'game' in name_lower or 'board' in name_lower or 'playing' in name_lower or 'cards' in name_lower:
        return PRODUCT_IMAGE_URLS['game']
    elif 'coloring' in name_lower:
        return PRODUCT_IMAGE_URLS['coloring']
    elif 'drone' in name_lower:
        return PRODUCT_IMAGE_URLS['drone']
    elif 'robot' in name_lower or 'stem' in name_lower:
        return PRODUCT_IMAGE_URLS['robot']
    elif 'remote' in name_lower and 'control' in name_lower:
        return PRODUCT_IMAGE_URLS['remote']
    
    # Beauty
    elif 'makeup' in name_lower or 'brush' in name_lower:
        return PRODUCT_IMAGE_URLS['makeup']
    elif 'perfume' in name_lower:
        return PRODUCT_IMAGE_URLS['perfume']
    elif 'lotion' in name_lower or 'body' in name_lower:
        return PRODUCT_IMAGE_URLS['lotion']
    elif 'manicure' in name_lower or 'pedicure' in name_lower or 'nail' in name_lower:
        return PRODUCT_IMAGE_URLS['manicure']
    elif 'straightener' in name_lower or 'flat' in name_lower:
        return PRODUCT_IMAGE_URLS['straightener']
    elif 'hair' in name_lower and 'dryer' in name_lower:
        return PRODUCT_IMAGE_URLS['dryer']
    elif 'jade' in name_lower or 'gua sha' in name_lower:
        return PRODUCT_IMAGE_URLS['jade']
    elif 'retinol' in name_lower or 'anti-aging' in name_lower:
        return PRODUCT_IMAGE_URLS['retinol']
    elif 'cream' in name_lower or 'serum' in name_lower or 'vitamin' in name_lower:
        return PRODUCT_IMAGE_URLS['serum']
    elif 'mask' in name_lower or 'facial' in name_lower:
        return PRODUCT_IMAGE_URLS['mask']
    
    # Office
    elif 'paper' in name_lower or 'folder' in name_lower:
        return PRODUCT_IMAGE_URLS['paper']
    elif 'desk' in name_lower or 'organizer' in name_lower:
        return PRODUCT_IMAGE_URLS['desk']
    elif 'chair' in name_lower:
        return PRODUCT_IMAGE_URLS['chair']
    elif 'whiteboard' in name_lower:
        return PRODUCT_IMAGE_URLS['whiteboard']
    elif 'stapler' in name_lower or 'staples' in name_lower:
        return PRODUCT_IMAGE_URLS['stapler']
    elif 'sticky' in name_lower or 'notes' in name_lower:
        return PRODUCT_IMAGE_URLS['sticky']
    
    # Pet
    elif 'dog' in name_lower:
        return PRODUCT_IMAGE_URLS['dog']
    elif 'cat' in name_lower:
        return PRODUCT_IMAGE_URLS['cat']
    elif 'pet' in name_lower:
        return PRODUCT_IMAGE_URLS['pet']
    
    # Garden
    elif 'garden' in name_lower or 'lawn' in name_lower or 'outdoor' in name_lower:
        return PRODUCT_IMAGE_URLS['garden']
    elif 'plant' in name_lower or 'pot' in name_lower:
        return PRODUCT_IMAGE_URLS['plant']
    
    else:
        return None

def download_image_from_url(url):
    """Download image from URL"""
    try:
        print(f"      Downloading from Unsplash...", end=" ")
        
        # Add headers to mimic browser
        request = urllib.request.Request(url)
        request.add_header('User-Agent', 'Mozilla/5.0')
        
        with urllib.request.urlopen(request, timeout=15) as response:
            image_data = response.read()
            image = Image.open(BytesIO(image_data))
            print("✅")
            return image
    except Exception as e:
        print(f"❌ {str(e)[:30]}")
        return None

def update_all_products():
    """Update all products with original product images"""
    
    print("=" * 70)
    print("📸 SETTING ORIGINAL PRODUCT IMAGES")
    print("=" * 70)
    print("Downloading real product photos from Unsplash CDN")
    print("=" * 70)
    print()
    
    products = Product.objects.all()
    total = products.count()
    
    updated = 0
    skipped = 0
    
    for index, product in enumerate(products, 1):
        try:
            print(f"[{index}/{total}] {product.name[:45]}")
            
            # Get image URL for this product
            image_url = get_product_image_url(product.name)
            
            if image_url is None:
                print(f"      No specific image available, skipping...")
                skipped += 1
                continue
            
            # Download image
            image = download_image_from_url(image_url)
            
            if image is None:
                skipped += 1
                continue
            
            # Resize to square
            image = image.resize((800, 800), Image.Resampling.LANCZOS)
            
            # Save to product
            image_io = BytesIO()
            image.save(image_io, format='JPEG', quality=90)
            image_io.seek(0)
            
            filename = f"product_{product.id}.jpg"
            product.image.save(filename, ContentFile(image_io.read()), save=True)
            
            updated += 1
            print(f"      ✅ Saved!\n")
            
            # Delay to avoid rate limiting
            time.sleep(0.5)
            
        except Exception as e:
            print(f"      ❌ Error: {str(e)[:40]}\n")
            skipped += 1
    
    print("=" * 70)
    print(f"🎉 COMPLETE!")
    print(f"   ✅ Updated: {updated}/{total}")
    print(f"   ⚠️  Skipped: {skipped}")
    print("=" * 70)
    print(f"\n🚀 View: http://127.0.0.1:8000/products/")

if __name__ == '__main__':
    update_all_products()
