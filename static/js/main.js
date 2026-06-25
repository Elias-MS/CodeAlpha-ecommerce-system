// Main JavaScript File for E-Commerce Store

// Auto-hide alerts after 5 seconds
document.addEventListener('DOMContentLoaded', function() {
    const alerts = document.querySelectorAll('.alert');
    alerts.forEach(function(alert) {
        setTimeout(function() {
            const bsAlert = new bootstrap.Alert(alert);
            bsAlert.close();
        }, 5000);
    });
});

// Confirm before clearing cart
function confirmClearCart() {
    return confirm('Are you sure you want to clear your cart?');
}

// Confirm before deleting
function confirmDelete(itemName) {
    return confirm(`Are you sure you want to delete ${itemName}?`);
}

// Update cart quantity
function updateQuantity(cartItemId, quantity) {
    if (quantity < 1) {
        return confirmDelete('this item from cart');
    }
    return true;
}

// Add to cart animation
function addToCartAnimation(button) {
    const originalText = button.innerHTML;
    button.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Adding...';
    button.disabled = true;
    
    setTimeout(function() {
        button.innerHTML = '<i class="fas fa-check"></i> Added!';
        setTimeout(function() {
            button.innerHTML = originalText;
            button.disabled = false;
        }, 1000);
    }, 500);
}

// Search functionality
const searchInput = document.querySelector('input[name="search"]');
if (searchInput) {
    searchInput.addEventListener('keypress', function(e) {
        if (e.key === 'Enter') {
            e.preventDefault();
            this.form.submit();
        }
    });
}

// Form validation
const forms = document.querySelectorAll('.needs-validation');
forms.forEach(function(form) {
    form.addEventListener('submit', function(event) {
        if (!form.checkValidity()) {
            event.preventDefault();
            event.stopPropagation();
        }
        form.classList.add('was-validated');
    }, false);
});

// Smooth scroll to top
function scrollToTop() {
    window.scrollTo({
        top: 0,
        behavior: 'smooth'
    });
}

// Add scroll to top button
window.addEventListener('scroll', function() {
    const scrollBtn = document.getElementById('scrollTopBtn');
    if (scrollBtn) {
        if (window.pageYOffset > 300) {
            scrollBtn.style.display = 'block';
        } else {
            scrollBtn.style.display = 'none';
        }
    }
});

// Image lazy loading
document.addEventListener('DOMContentLoaded', function() {
    const images = document.querySelectorAll('img[data-src]');
    const imageObserver = new IntersectionObserver(function(entries, observer) {
        entries.forEach(function(entry) {
            if (entry.isIntersecting) {
                const img = entry.target;
                img.src = img.dataset.src;
                img.removeAttribute('data-src');
                imageObserver.unobserve(img);
            }
        });
    });
    
    images.forEach(function(img) {
        imageObserver.observe(img);
    });
});

// Price formatting
function formatPrice(price) {
    return '$' + parseFloat(price).toFixed(2);
}

// Calculate cart total
function calculateCartTotal() {
    let total = 0;
    const cartItems = document.querySelectorAll('.cart-item');
    cartItems.forEach(function(item) {
        const price = parseFloat(item.dataset.price);
        const quantity = parseInt(item.querySelector('input[name="quantity"]').value);
        total += price * quantity;
    });
    return total;
}

// Update cart display
function updateCartDisplay() {
    const total = calculateCartTotal();
    const totalElement = document.getElementById('cartTotal');
    if (totalElement) {
        totalElement.textContent = formatPrice(total);
    }
}

// Quantity input validation
const quantityInputs = document.querySelectorAll('input[type="number"]');
quantityInputs.forEach(function(input) {
    input.addEventListener('change', function() {
        const min = parseInt(this.min) || 1;
        const max = parseInt(this.max) || 999;
        let value = parseInt(this.value);
        
        if (value < min) {
            this.value = min;
        } else if (value > max) {
            this.value = max;
            alert(`Maximum quantity available is ${max}`);
        }
    });
});

// Checkout form validation
const checkoutForm = document.getElementById('checkoutForm');
if (checkoutForm) {
    checkoutForm.addEventListener('submit', function(e) {
        const phone = document.getElementById('phone').value;
        const phoneRegex = /^[0-9]{10,15}$/;
        
        if (!phoneRegex.test(phone.replace(/[^0-9]/g, ''))) {
            e.preventDefault();
            alert('Please enter a valid phone number (10-15 digits)');
            return false;
        }
    });
}

// Product rating stars
function renderStars(rating) {
    const fullStars = Math.floor(rating);
    const halfStar = rating % 1 >= 0.5 ? 1 : 0;
    const emptyStars = 5 - fullStars - halfStar;
    
    let stars = '';
    for (let i = 0; i < fullStars; i++) {
        stars += '<i class="fas fa-star text-warning"></i>';
    }
    if (halfStar) {
        stars += '<i class="fas fa-star-half-alt text-warning"></i>';
    }
    for (let i = 0; i < emptyStars; i++) {
        stars += '<i class="far fa-star text-warning"></i>';
    }
    
    return stars;
}

// Console welcome message
console.log('%c Welcome to E-Shop! ', 'background: #0d6efd; color: white; font-size: 20px; padding: 10px;');
console.log('%c Happy Shopping! 🛒 ', 'background: #198754; color: white; font-size: 16px; padding: 5px;');
