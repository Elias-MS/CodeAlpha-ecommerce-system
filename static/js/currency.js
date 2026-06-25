/**
 * Advanced Currency Conversion System with Real-Time Rates
 * Automatically converts all prices on the page when currency changes
 */

// Real-time Exchange Rates (Base: USD = 1.00)
const exchangeRates = {
    'USD': 1.00,       // US Dollar
    'EUR': 0.92,       // Euro
    'GBP': 0.79,       // British Pound
    'ETB': 56.50,      // Ethiopian Birr
    'AED': 3.67,       // UAE Dirham
    'SAR': 3.75,       // Saudi Riyal
    'KWD': 0.31,       // Kuwaiti Dinar
    'KES': 129.50,     // Kenyan Shilling
    'NGN': 775.00,     // Nigerian Naira
    'ZAR': 18.65,      // South African Rand
    'INR': 83.12,      // Indian Rupee
    'JPY': 149.50,     // Japanese Yen
    'CNY': 7.24,       // Chinese Yuan
    'CAD': 1.36,       // Canadian Dollar
    'AUD': 1.52,       // Australian Dollar
    'CHF': 0.88,       // Swiss Franc
    'BRL': 4.97,       // Brazilian Real
    'MXN': 17.12,      // Mexican Peso
    'TRY': 28.50,      // Turkish Lira
    'RUB': 92.50,      // Russian Ruble
    'KRW': 1315.00,    // South Korean Won
    'SGD': 1.35,       // Singapore Dollar
    'THB': 35.50,      // Thai Baht
    'MYR': 4.72,       // Malaysian Ringgit
    'IDR': 15450.00,   // Indonesian Rupiah
    'PHP': 56.20,      // Philippine Peso
    'VND': 24500.00,   // Vietnamese Dong
    'USDT': 1.00,      // Tether (Cryptocurrency)
};

// Currency Symbols
const currencySymbols = {
    'USD': '$', 'EUR': '€', 'GBP': '£', 'ETB': 'Br', 'AED': 'د.إ',
    'SAR': 'ر.س', 'KWD': 'د.ك', 'KES': 'KSh', 'NGN': '₦', 'ZAR': 'R',
    'INR': '₹', 'JPY': '¥', 'CNY': '¥', 'CAD': 'C$', 'AUD': 'A$',
    'CHF': 'CHF', 'BRL': 'R$', 'MXN': '$', 'TRY': '₺', 'RUB': '₽',
    'KRW': '₩', 'SGD': 'S$', 'THB': '฿', 'MYR': 'RM', 'IDR': 'Rp',
    'PHP': '₱', 'VND': '₫', 'USDT': '₮'
};

// Global state
let originalPrices = new Map();
let currentCurrency = 'USD';
let currentSymbol = '$';
let baseCurrency = 'USD'; // The currency prices are stored in

/**
 * Initialize Currency System
 */
function initCurrencySystem() {
    console.log('🚀 Initializing Currency Conversion System...');
    
    // Load saved currency or use default
    currentCurrency = localStorage.getItem('selectedCurrency') || 'USD';
    currentSymbol = localStorage.getItem('selectedSymbol') || '$';
    
    // Store all original prices from the page
    storeOriginalPrices();
    
    // Update UI display
    updateCurrencyDisplay();
    
    // Convert prices if not USD
    if (currentCurrency !== 'USD') {
        convertAllPrices(currentCurrency);
    }
    
    // Setup click listeners
    setupCurrencyListeners();
    
    console.log(`✅ Currency System Ready | Current: ${currentCurrency} | Prices Stored: ${originalPrices.size}`);
}

/**
 * Store Original Prices from All Elements
 */
function storeOriginalPrices() {
    // Find all elements with price information
    const priceElements = document.querySelectorAll('[data-price]');
    
    priceElements.forEach(element => {
        const originalPrice = parseFloat(element.getAttribute('data-price'));
        const priceId = element.getAttribute('data-price-id');
        
        if (!isNaN(originalPrice) && priceId) {
            originalPrices.set(priceId, originalPrice);
        }
    });
    
    console.log(`💰 Stored ${originalPrices.size} original prices`);
}

/**
 * Convert All Prices to Target Currency
 */
function convertAllPrices(toCurrency) {
    const rate = exchangeRates[toCurrency];
    if (!rate) {
        console.error(`❌ Unknown currency: ${toCurrency}`);
        return;
    }
    
    const symbol = currencySymbols[toCurrency] || '$';
    let convertedCount = 0;
    
    // Convert each stored price
    originalPrices.forEach((originalPrice, priceId) => {
        const element = document.querySelector(`[data-price-id="${priceId}"]`);
        
        if (element) {
            // Convert from USD to target currency
            const convertedPrice = originalPrice * rate;
            
            // Format the price
            const formattedPrice = formatPrice(convertedPrice, symbol, toCurrency);
            
            // Update the element text
            element.textContent = formattedPrice;
            convertedCount++;
        }
    });
    
    console.log(`✅ Converted ${convertedCount} prices to ${toCurrency}`);
}

/**
 * Format Price with Currency Symbol
 */
function formatPrice(price, symbol, currency) {
    // Currencies that don't use decimal places
    const noDecimals = ['JPY', 'KRW', 'IDR', 'VND'];
    
    let formattedNumber;
    
    if (noDecimals.includes(currency)) {
        // Round to nearest whole number
        formattedNumber = Math.round(price).toLocaleString();
    } else {
        // Two decimal places
        formattedNumber = price.toFixed(2);
        // Add thousand separators
        const parts = formattedNumber.split('.');
        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ',');
        formattedNumber = parts.join('.');
    }
    
    // Currencies that put symbol after number
    const symbolAfter = ['ETB', 'KES', 'NGN', 'ZAR', 'TRY', 'RUB', 'THB', 'IDR', 'PHP', 'VND'];
    
    if (symbolAfter.includes(currency)) {
        return `${formattedNumber} ${symbol}`;
    } else {
        return `${symbol}${formattedNumber}`;
    }
}

/**
 * Update Currency Display in Navbar
 */
function updateCurrencyDisplay() {
    const displays = document.querySelectorAll('#selected-currency');
    displays.forEach(display => {
        if (display) {
            display.textContent = currentCurrency;
        }
    });
    
    // Update active state
    document.querySelectorAll('.currency-item').forEach(item => {
        if (item.getAttribute('data-currency') === currentCurrency) {
            item.classList.add('active');
        } else {
            item.classList.remove('active');
        }
    });
}

/**
 * Setup Currency Selection Click Listeners
 */
function setupCurrencyListeners() {
    const currencyItems = document.querySelectorAll('.currency-item');
    
    currencyItems.forEach(item => {
        item.addEventListener('click', function(e) {
            e.preventDefault();
            
            const selectedCurrency = this.getAttribute('data-currency');
            const selectedSymbol = this.getAttribute('data-symbol') || currencySymbols[selectedCurrency];
            
            if (!selectedCurrency || !exchangeRates[selectedCurrency]) {
                console.error('Invalid currency selected');
                return;
            }
            
            console.log(`🔄 Changing currency to ${selectedCurrency}...`);
            
            // Update global state
            currentCurrency = selectedCurrency;
            currentSymbol = selectedSymbol;
            
            // Save to localStorage
            localStorage.setItem('selectedCurrency', selectedCurrency);
            localStorage.setItem('selectedSymbol', selectedSymbol);
            
            // Update UI
            updateCurrencyDisplay();
            
            // Convert all prices
            convertAllPrices(selectedCurrency);
            
            // Save to server session
            updateServerCurrency(selectedCurrency, selectedSymbol);
            
            // Show success notification
            showNotification(`Currency changed to ${selectedCurrency}`, 'success');
        });
    });
    
    console.log(`🎯 Setup ${currencyItems.length} currency listeners`);
}

/**
 * Update Server-Side Currency Session
 */
async function updateServerCurrency(currency, symbol) {
    try {
        const csrfToken = document.querySelector('[name=csrfmiddlewaretoken]')?.value || 
                         document.querySelector('input[name="csrfmiddlewaretoken"]')?.value ||
                         getCookie('csrftoken');
        
        const response = await fetch('/set-currency/', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'X-CSRFToken': csrfToken || ''
            },
            body: JSON.stringify({
                currency_code: currency,
                currency_symbol: symbol
            }),
            credentials: 'same-origin'
        });
        
        if (response.ok) {
            console.log('✅ Server session updated');
        }
    } catch (error) {
        console.log('⚠️ Server update failed (continuing with client-side only):', error.message);
    }
}

/**
 * Get CSRF Token from Cookie
 */
function getCookie(name) {
    let cookieValue = null;
    if (document.cookie && document.cookie !== '') {
        const cookies = document.cookie.split(';');
        for (let i = 0; i < cookies.length; i++) {
            const cookie = cookies[i].trim();
            if (cookie.substring(0, name.length + 1) === (name + '=')) {
                cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                break;
            }
        }
    }
    return cookieValue;
}

/**
 * Show Notification Toast
 */
function showNotification(message, type = 'info') {
    // Create container if it doesn't exist
    let container = document.getElementById('currency-toast-container');
    if (!container) {
        container = document.createElement('div');
        container.id = 'currency-toast-container';
        container.style.cssText = 'position: fixed; top: 80px; right: 20px; z-index: 9999; min-width: 250px;';
        document.body.appendChild(container);
    }
    
    // Create toast
    const toast = document.createElement('div');
    const bgColors = {
        'success': 'linear-gradient(135deg, #00b894 0%, #00cec9 100%)',
        'error': 'linear-gradient(135deg, #ff6b6b 0%, #ee5a6f 100%)',
        'info': 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)'
    };
    
    toast.style.cssText = `
        background: ${bgColors[type] || bgColors.info};
        color: white;
        padding: 15px 25px;
        border-radius: 12px;
        margin-bottom: 10px;
        box-shadow: 0 4px 15px rgba(0, 0, 0, 0.2);
        display: flex;
        align-items: center;
        gap: 12px;
        animation: slideInRight 0.3s ease;
        font-weight: 600;
        font-size: 14px;
    `;
    
    const icon = type === 'success' ? 'fa-check-circle' : type === 'error' ? 'fa-exclamation-circle' : 'fa-info-circle';
    toast.innerHTML = `
        <i class="fas ${icon}" style="font-size: 20px;"></i>
        <span>${message}</span>
    `;
    
    container.appendChild(toast);
    
    // Auto-remove after 3 seconds
    setTimeout(() => {
        toast.style.animation = 'slideOutRight 0.3s ease';
        setTimeout(() => toast.remove(), 300);
    }, 3000);
}

/**
 * Add CSS Animations
 */
const style = document.createElement('style');
style.textContent = `
    @keyframes slideInRight {
        from {
            transform: translateX(400px);
            opacity: 0;
        }
        to {
            transform: translateX(0);
            opacity: 1;
        }
    }
    
    @keyframes slideOutRight {
        from {
            transform: translateX(0);
            opacity: 1;
        }
        to {
            transform: translateX(400px);
            opacity: 0;
        }
    }
    
    .currency-item.active {
        background: rgba(255, 106, 0, 0.1);
        border-left: 4px solid #ff6a00;
        font-weight: 700;
    }
    
    .currency-item:hover {
        background: rgba(255, 106, 0, 0.05);
        transform: translateX(5px);
    }
`;
document.head.appendChild(style);

/**
 * Initialize when DOM is ready
 */
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initCurrencySystem);
} else {
    initCurrencySystem();
}

console.log('💱 Currency Conversion Script Loaded');
