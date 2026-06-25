// 🌍 INSTANT LANGUAGE TRANSLATION SYSTEM
// Translates the entire website instantly without page reload

// Translation Dictionary
const translations = {
    'en': {
        // Navigation
        'Home': 'Home',
        'Products': 'Products',
        'About Us': 'About Us',
        'Contact': 'Contact',
        'Announcements': 'Announcements',
        'Dashboard': 'Dashboard',
        'Cart': 'Cart',
        'Orders': 'Orders',
        'Admin': 'Admin',
        'Profile': 'Profile',
        'Logout': 'Logout',
        'Login': 'Login',
        'Register': 'Register',
        
        // Product
        'Add to Cart': 'Add to Cart',
        'Buy Now': 'Buy Now',
        'View Details': 'View Details',
        'View Product': 'View Product',
        'Price': 'Price',
        'Stock': 'Stock',
        'Category': 'Category',
        'Rating': 'Rating',
        'Reviews': 'Reviews',
        'Description': 'Description',
        'Available': 'Available',
        'Out of Stock': 'Out of Stock',
        
        // Cart & Checkout
        'Shopping Cart': 'Shopping Cart',
        'Checkout': 'Checkout',
        'Subtotal': 'Subtotal',
        'Total': 'Total',
        'Quantity': 'Quantity',
        'Remove': 'Remove',
        'Continue Shopping': 'Continue Shopping',
        'Proceed to Checkout': 'Proceed to Checkout',
        'Empty Cart': 'Your cart is empty',
        
        // User
        'Welcome': 'Welcome',
        'My Account': 'My Account',
        'My Orders': 'My Orders',
        'Order History': 'Order History',
        'Submit Complaint': 'Submit Complaint',
        'My Complaints': 'My Complaints',
        
        // Common
        'Search': 'Search',
        'Search products...': 'Search products...',
        'Filter': 'Filter',
        'Sort': 'Sort',
        'Save': 'Save',
        'Cancel': 'Cancel',
        'Submit': 'Submit',
        'Edit': 'Edit',
        'Delete': 'Delete',
        'Back': 'Back',
        'Next': 'Next',
        'Previous': 'Previous',
        'Loading...': 'Loading...',
        
        // Footer
        'Quick Links': 'Quick Links',
        'Follow Us': 'Follow Us',
        'All Rights Reserved': 'All Rights Reserved',
        'Your one-stop online shopping destination for quality products at great prices.': 'Your one-stop online shopping destination for quality products at great prices.',
    },
    
    'am': {  // Amharic (አማርኛ)
        // Navigation
        'Home': 'መነሻ',
        'Products': 'ምርቶች',
        'About Us': 'ስለ እኛ',
        'Contact': 'አግኙን',
        'Announcements': 'ማስታወቂያዎች',
        'Dashboard': 'ዳሽቦርድ',
        'Cart': 'ጋሪ',
        'Orders': 'ትዕዛዞች',
        'Admin': 'አስተዳዳሪ',
        'Profile': 'መገለጫ',
        'Logout': 'ውጣ',
        'Login': 'ግባ',
        'Register': 'ተመዝገብ',
        
        // Product
        'Add to Cart': 'ወደ ጋሪ ጨምር',
        'Buy Now': 'አሁን ግዛ',
        'View Details': 'ዝርዝር ይመልከቱ',
        'View Product': 'ምርት ይመልከቱ',
        'Price': 'ዋጋ',
        'Stock': 'አክሲዮን',
        'Category': 'ምድብ',
        'Rating': 'ደረጃ',
        'Reviews': 'ግምገማዎች',
        'Description': 'መግለጫ',
        'Available': 'ይገኛል',
        'Out of Stock': 'ከአክሲዮን ውጭ',
        
        // Cart & Checkout
        'Shopping Cart': 'የግዢ ጋሪ',
        'Checkout': 'ክፈል',
        'Subtotal': 'ንዑስ ድምር',
        'Total': 'ጠቅላላ',
        'Quantity': 'ብዛት',
        'Remove': 'አስወግድ',
        'Continue Shopping': 'ግዢን ቀጥል',
        'Proceed to Checkout': 'ወደ ክፍያ ይቀጥሉ',
        'Empty Cart': 'ጋሪዎ ባዶ ነው',
        
        // User
        'Welcome': 'እንኳን ደህና መጡ',
        'My Account': 'የእኔ መለያ',
        'My Orders': 'የእኔ ትዕዛዞች',
        'Order History': 'የትዕዛዝ ታሪክ',
        'Submit Complaint': 'ቅሬታ አቅርብ',
        'My Complaints': 'የእኔ ቅሬታዎች',
        
        // Common
        'Search': 'ፈልግ',
        'Search products...': 'ምርቶችን ፈልግ...',
        'Filter': 'አጣራ',
        'Sort': 'ደርድር',
        'Save': 'አስቀምጥ',
        'Cancel': 'ሰርዝ',
        'Submit': 'አስገባ',
        'Edit': 'አርትዕ',
        'Delete': 'ሰርዝ',
        'Back': 'ተመለስ',
        'Next': 'ቀጣይ',
        'Previous': 'ቀዳሚ',
        'Loading...': 'በመጫን ላይ...',
        
        // Footer
        'Quick Links': 'ፈጣን አገናኞች',
        'Follow Us': 'ተከተሉን',
        'All Rights Reserved': 'ሁሉም መብቶች የተጠበቁ ናቸው',
        'Your one-stop online shopping destination for quality products at great prices.': 'በጥሩ ዋጋ ጥራት ያላቸውን ምርቶች የሚያገኙበት የመስመር ላይ ግዢ መድረሻ።',
    },
    
    'ti': {  // Tigrinya (ትግርኛ)
        // Navigation
        'Home': 'መበገሲ',
        'Products': 'ፍርያት',
        'About Us': 'ብዛዕባና',
        'Contact': 'ርኸቡና',
        'Announcements': 'መግለጺታት',
        'Dashboard': 'ዳሽቦርድ',
        'Cart': 'ዓረብያ',
        'Orders': 'ትእዛዛት',
        'Admin': 'አስተዳዳሪ',
        'Profile': 'መግለጺ',
        'Logout': 'ውጻእ',
        'Login': 'ኣቱ',
        'Register': 'ምዝገባ',
        
        // Product
        'Add to Cart': 'ናብ ዓረብያ ወስኽ',
        'Buy Now': 'ሕጂ ግዛእ',
        'View Details': 'ዝርዝር ርአ',
        'View Product': 'ፍርያት ርአ',
        'Price': 'ዋጋ',
        'Stock': 'ክምዒት',
        'Category': 'ምድብ',
        'Rating': 'ደረጃ',
        'Reviews': 'ግምገማታት',
        'Description': 'መግለጺ',
        'Available': 'ይርከብ',
        'Out of Stock': 'ካብ ክምዒት ወጻኢ',
        
        // Cart & Checkout
        'Shopping Cart': 'ዓረብያ ዕዳጋ',
        'Checkout': 'ክፈል',
        'Subtotal': 'ንኡስ ድምር',
        'Total': 'ጠቕላላ',
        'Quantity': 'ብዝሒ',
        'Remove': 'ኣልግስ',
        'Continue Shopping': 'ዕዳጋ ቀጽል',
        'Proceed to Checkout': 'ናብ ክፍሊት ቀጽል',
        'Empty Cart': 'ዓረብያኻ ባዶ እዩ',
        
        // User
        'Welcome': 'እንቋዕ ብደሓን መጻእኩም',
        'My Account': 'መለያይ',
        'My Orders': 'ትእዛዛተይ',
        'Order History': 'ታሪኽ ትእዛዝ',
        'Submit Complaint': 'ቅሬታ አቅርብ',
        'My Complaints': 'ቅሬታታተይ',
        
        // Common
        'Search': 'ድለ',
        'Search products...': 'ፍርያት ድለ...',
        'Filter': 'ፍልተር',
        'Sort': 'ደርድር',
        'Save': 'ዓቅብ',
        'Cancel': 'ሰርዝ',
        'Submit': 'ኣእቱ',
        'Edit': 'ኣርትዕ',
        'Delete': 'ሰርዝ',
        'Back': 'ተመለስ',
        'Next': 'ቀጻሊ',
        'Previous': 'ቀዳማይ',
        'Loading...': 'ይጽዕን ኣሎ...',
        
        // Footer
        'Quick Links': 'ቅልጡፍ መራኸቢታት',
        'Follow Us': 'ተኸተሉና',
        'All Rights Reserved': 'ኩሉ መሰላት ተሓሊዩ',
        'Your one-stop online shopping destination for quality products at great prices.': 'ብጽቡቕ ዋጋ ጽቡቕ ፍርያት ዝረኽቡሉ ናይ ኢንተርነት ዕዳጋ መድረኽ።',
    },
    
    'hi': {  // Hindi (हिन्दी)
        // Navigation
        'Home': 'होम',
        'Products': 'उत्पाद',
        'About Us': 'हमारे बारे में',
        'Contact': 'संपर्क करें',
        'Announcements': 'घोषणाएं',
        'Dashboard': 'डैशबोर्ड',
        'Cart': 'कार्ट',
        'Orders': 'ऑर्डर',
        'Admin': 'व्यवस्थापक',
        'Profile': 'प्रोफ़ाइल',
        'Logout': 'लॉगआउट',
        'Login': 'लॉगिन',
        'Register': 'रजिस्टर',
        
        // Product
        'Add to Cart': 'कार्ट में जोड़ें',
        'Buy Now': 'अभी खरीदें',
        'View Details': 'विवरण देखें',
        'View Product': 'उत्पाद देखें',
        'Price': 'कीमत',
        'Stock': 'स्टॉक',
        'Category': 'श्रेणी',
        'Rating': 'रेटिंग',
        'Reviews': 'समीक्षाएं',
        'Description': 'विवरण',
        'Available': 'उपलब्ध',
        'Out of Stock': 'स्टॉक में नहीं',
        
        // Cart & Checkout
        'Shopping Cart': 'शॉपिंग कार्ट',
        'Checkout': 'चेकआउट',
        'Subtotal': 'उपयोग',
        'Total': 'कुल',
        'Quantity': 'मात्रा',
        'Remove': 'हटाएं',
        'Continue Shopping': 'खरीदारी जारी रखें',
        'Proceed to Checkout': 'चेकआउट पर जाएं',
        'Empty Cart': 'आपकी कार्ट खाली है',
        
        // User
        'Welcome': 'स्वागत है',
        'My Account': 'मेरा खाता',
        'My Orders': 'मेरे ऑर्डर',
        'Order History': 'ऑर्डर इतिहास',
        'Submit Complaint': 'शिकायत दर्ज करें',
        'My Complaints': 'मेरी शिकायतें',
        
        // Common
        'Search': 'खोजें',
        'Search products...': 'उत्पाद खोजें...',
        'Filter': 'फ़िल्टर',
        'Sort': 'क्रमबद्ध करें',
        'Save': 'सहेजें',
        'Cancel': 'रद्द करें',
        'Submit': 'जमा करें',
        'Edit': 'संपादित करें',
        'Delete': 'हटाएं',
        'Back': 'वापस',
        'Next': 'अगला',
        'Previous': 'पिछला',
        'Loading...': 'लोड हो रहा है...',
        
        // Footer
        'Quick Links': 'त्वरित लिंक',
        'Follow Us': 'हमें फॉलो करें',
        'All Rights Reserved': 'सर्वाधिकार सुरक्षित',
        'Your one-stop online shopping destination for quality products at great prices.': 'बेहतरीन कीमतों पर गुणवत्तापूर्ण उत्पादों के लिए आपका वन-स्टॉप ऑनलाइन शॉपिंग गंतव्य।',
    },
    
    'ar': {  // Arabic (العربية)
        // Navigation
        'Home': 'الرئيسية',
        'Products': 'المنتجات',
        'About Us': 'من نحن',
        'Contact': 'اتصل بنا',
        'Announcements': 'الإعلانات',
        'Dashboard': 'لوحة التحكم',
        'Cart': 'السلة',
        'Orders': 'الطلبات',
        'Admin': 'المسؤول',
        'Profile': 'الملف الشخصي',
        'Logout': 'تسجيل الخروج',
        'Login': 'تسجيل الدخول',
        'Register': 'التسجيل',
        
        // Product
        'Add to Cart': 'أضف إلى السلة',
        'Buy Now': 'اشتر الآن',
        'View Details': 'عرض التفاصيل',
        'View Product': 'عرض المنتج',
        'Price': 'السعر',
        'Stock': 'المخزون',
        'Category': 'الفئة',
        'Rating': 'التقييم',
        'Reviews': 'المراجعات',
        'Description': 'الوصف',
        'Available': 'متوفر',
        'Out of Stock': 'غير متوفر',
        
        // Cart & Checkout
        'Shopping Cart': 'سلة التسوق',
        'Checkout': 'الدفع',
        'Subtotal': 'المجموع الفرعي',
        'Total': 'المجموع',
        'Quantity': 'الكمية',
        'Remove': 'إزالة',
        'Continue Shopping': 'متابعة التسوق',
        'Proceed to Checkout': 'المتابعة للدفع',
        'Empty Cart': 'سلتك فارغة',
        
        // User
        'Welcome': 'مرحبا',
        'My Account': 'حسابي',
        'My Orders': 'طلباتي',
        'Order History': 'سجل الطلبات',
        'Submit Complaint': 'تقديم شكوى',
        'My Complaints': 'شكاواي',
        
        // Common
        'Search': 'بحث',
        'Search products...': 'البحث عن المنتجات...',
        'Filter': 'تصفية',
        'Sort': 'ترتيب',
        'Save': 'حفظ',
        'Cancel': 'إلغاء',
        'Submit': 'إرسال',
        'Edit': 'تعديل',
        'Delete': 'حذف',
        'Back': 'رجوع',
        'Next': 'التالي',
        'Previous': 'السابق',
        'Loading...': 'جار التحميل...',
        
        // Footer
        'Quick Links': 'روابط سريعة',
        'Follow Us': 'تابعنا',
        'All Rights Reserved': 'جميع الحقوق محفوظة',
        'Your one-stop online shopping destination for quality products at great prices.': 'وجهتك الشاملة للتسوق عبر الإنترنت للمنتجات عالية الجودة بأسعار رائعة.',
    },
    
    'es': {  // Spanish (Español)
        // Navigation
        'Home': 'Inicio',
        'Products': 'Productos',
        'About Us': 'Sobre Nosotros',
        'Contact': 'Contacto',
        'Announcements': 'Anuncios',
        'Dashboard': 'Panel',
        'Cart': 'Carrito',
        'Orders': 'Pedidos',
        'Admin': 'Administrador',
        'Profile': 'Perfil',
        'Logout': 'Cerrar Sesión',
        'Login': 'Iniciar Sesión',
        'Register': 'Registrarse',
        
        // Product
        'Add to Cart': 'Añadir al Carrito',
        'Buy Now': 'Comprar Ahora',
        'View Details': 'Ver Detalles',
        'View Product': 'Ver Producto',
        'Price': 'Precio',
        'Stock': 'Stock',
        'Category': 'Categoría',
        'Rating': 'Calificación',
        'Reviews': 'Reseñas',
        'Description': 'Descripción',
        'Available': 'Disponible',
        'Out of Stock': 'Agotado',
        
        // Cart & Checkout
        'Shopping Cart': 'Carrito de Compras',
        'Checkout': 'Pagar',
        'Subtotal': 'Subtotal',
        'Total': 'Total',
        'Quantity': 'Cantidad',
        'Remove': 'Eliminar',
        'Continue Shopping': 'Continuar Comprando',
        'Proceed to Checkout': 'Proceder al Pago',
        'Empty Cart': 'Tu carrito está vacío',
        
        // User
        'Welcome': 'Bienvenido',
        'My Account': 'Mi Cuenta',
        'My Orders': 'Mis Pedidos',
        'Order History': 'Historial de Pedidos',
        'Submit Complaint': 'Enviar Queja',
        'My Complaints': 'Mis Quejas',
        
        // Common
        'Search': 'Buscar',
        'Search products...': 'Buscar productos...',
        'Filter': 'Filtrar',
        'Sort': 'Ordenar',
        'Save': 'Guardar',
        'Cancel': 'Cancelar',
        'Submit': 'Enviar',
        'Edit': 'Editar',
        'Delete': 'Eliminar',
        'Back': 'Atrás',
        'Next': 'Siguiente',
        'Previous': 'Anterior',
        'Loading...': 'Cargando...',
        
        // Footer
        'Quick Links': 'Enlaces Rápidos',
        'Follow Us': 'Síguenos',
        'All Rights Reserved': 'Todos los Derechos Reservados',
        'Your one-stop online shopping destination for quality products at great prices.': 'Tu destino de compras en línea para productos de calidad a excelentes precios.',
    },
};

// Get translation for a key
function translate(key, lang) {
    if (translations[lang] && translations[lang][key]) {
        return translations[lang][key];
    }
    return key; // Return original if translation not found
}

// Translate all text on the page
function translatePage(lang) {
    // Translate all elements with data-translate attribute
    document.querySelectorAll('[data-translate]').forEach(function(el) {
        const key = el.getAttribute('data-translate');
        el.textContent = translate(key, lang);
    });
    
    // Translate placeholders
    document.querySelectorAll('[data-translate-placeholder]').forEach(function(el) {
        const key = el.getAttribute('data-translate-placeholder');
        el.placeholder = translate(key, lang);
    });
    
    // Translate titles
    document.querySelectorAll('[data-translate-title]').forEach(function(el) {
        const key = el.getAttribute('data-translate-title');
        el.title = translate(key, lang);
    });
}

// Initialize language system
document.addEventListener('DOMContentLoaded', function() {
    const languageItems = document.querySelectorAll('.language-item');
    const selectedLanguage = document.getElementById('selected-language');
    const selectedLanguage2 = document.getElementById('selected-language-2');
    
    // Load saved language from localStorage
    const savedLang = localStorage.getItem('selectedLanguage') || 'en';
    const savedLangName = localStorage.getItem('selectedLanguageName') || 'English';
    
    // Update display
    if (selectedLanguage) {
        selectedLanguage.textContent = savedLang.toUpperCase();
    }
    if (selectedLanguage2) {
        selectedLanguage2.textContent = savedLang.toUpperCase();
    }
    
    // Apply saved language on page load
    if (savedLang !== 'en') {
        translatePage(savedLang);
    }
    
    // Handle language selection
    languageItems.forEach(function(item) {
        item.addEventListener('click', function(e) {
            e.preventDefault();
            
            const lang = this.getAttribute('data-lang');
            const langName = this.getAttribute('data-name');
            
            // Update display
            if (selectedLanguage) {
                selectedLanguage.textContent = lang.toUpperCase();
            }
            if (selectedLanguage2) {
                selectedLanguage2.textContent = lang.toUpperCase();
            }
            
            // Save to localStorage
            localStorage.setItem('selectedLanguage', lang);
            localStorage.setItem('selectedLanguageName', langName);
            
            // Remove active class from all items
            languageItems.forEach(function(i) {
                i.classList.remove('active');
            });
            
            // Add active class to selected item
            this.classList.add('active');
            
            // Translate the page instantly
            translatePage(lang);
            
            // Show notification
            const notification = document.createElement('div');
            notification.className = 'alert alert-success alert-dismissible fade show position-fixed top-0 start-50 translate-middle-x mt-3';
            notification.style.zIndex = '9999';
            notification.style.minWidth = '300px';
            notification.innerHTML = `
                <strong>✓ ${translate('Language Changed!', lang)}</strong><br>
                ${translate('Now showing content in', lang)} <strong>${langName}</strong>
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            `;
            document.body.appendChild(notification);
            
            // Auto-dismiss after 3 seconds
            setTimeout(function() {
                notification.remove();
            }, 3000);
        });
        
        // Mark current language as active
        if (item.getAttribute('data-lang') === savedLang) {
            item.classList.add('active');
        }
    });
});
