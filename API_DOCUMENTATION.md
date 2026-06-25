# API Documentation - E-Commerce Store

## Overview

This document describes all the available routes/endpoints in the E-Commerce Store application.

**Base URL:** `http://127.0.0.1:8000/`

**Authentication:** Session-based authentication using Django's built-in auth system

---

## Table of Contents

1. [Product Endpoints](#product-endpoints)
2. [User Authentication Endpoints](#user-authentication-endpoints)
3. [Cart Endpoints](#cart-endpoints)
4. [Order Endpoints](#order-endpoints)
5. [Admin Endpoints](#admin-endpoints)

---

## Product Endpoints

### 1. Home Page

**Endpoint:** `/`

**Method:** `GET`

**Description:** Display home page with featured products and categories

**Authentication:** Not required

**Response:** HTML page

**Features:**
- Featured products (8 latest)
- All categories
- Hero section
- Features section

---

### 2. Product List

**Endpoint:** `/products/`

**Method:** `GET`

**Description:** Display all products with search and filter options

**Authentication:** Not required

**Query Parameters:**
- `search` (optional): Search query string
- `category` (optional): Category ID for filtering

**Example:**
```
GET /products/?search=laptop
GET /products/?category=1
GET /products/?search=phone&category=2
```

**Response:** HTML page with filtered products

---

### 3. Product Detail

**Endpoint:** `/product/<int:pk>/`

**Method:** `GET`

**Description:** Display detailed information about a specific product

**Authentication:** Not required

**URL Parameters:**
- `pk`: Product ID (integer)

**Example:**
```
GET /product/1/
```

**Response:** HTML page with:
- Product details
- Reviews
- Related products
- Add to cart form

---

### 4. Add Product Review

**Endpoint:** `/product/<int:pk>/review/`

**Method:** `POST`

**Description:** Add or update a review for a product

**Authentication:** Required (login_required)

**URL Parameters:**
- `pk`: Product ID (integer)

**Form Data:**
- `rating`: Integer (1-5)
- `comment`: Text

**Example:**
```
POST /product/1/review/
Content-Type: application/x-www-form-urlencoded

rating=5&comment=Great product!
```

**Response:** Redirect to product detail page

**Success Message:** "Review added successfully!"

**Error Message:** "Please provide both rating and comment."

---

## User Authentication Endpoints

### 5. User Registration

**Endpoint:** `/users/register/`

**Methods:** `GET`, `POST`

**Description:** User registration page and handler

**Authentication:** Not required (redirects if already authenticated)

**GET Request:**
- Returns registration form

**POST Request:**

**Form Data:**
- `username`: String (required, unique)
- `email`: Email (required, unique)
- `password`: String (required)
- `confirm_password`: String (required, must match password)

**Example:**
```
POST /users/register/
Content-Type: application/x-www-form-urlencoded

username=johndoe&email=john@example.com&password=securepass123&confirm_password=securepass123
```

**Success Response:** Redirect to login page

**Success Message:** "Registration successful! Please login."

**Error Messages:**
- "Username already exists."
- "Email already registered."
- "Passwords do not match."

---

### 6. User Login

**Endpoint:** `/users/login/`

**Methods:** `GET`, `POST`

**Description:** User login page and authentication handler

**Authentication:** Not required (redirects if already authenticated)

**GET Request:**
- Returns login form

**POST Request:**

**Form Data:**
- `username`: String (required)
- `password`: String (required)

**Query Parameters:**
- `next` (optional): Redirect URL after login

**Example:**
```
POST /users/login/?next=/cart/
Content-Type: application/x-www-form-urlencoded

username=johndoe&password=securepass123
```

**Success Response:** Redirect to home or `next` URL

**Success Message:** "Welcome back, {username}!"

**Error Message:** "Invalid username or password."

---

### 7. User Logout

**Endpoint:** `/users/logout/`

**Method:** `GET`

**Description:** Logout current user

**Authentication:** Required (login_required)

**Response:** Redirect to home page

**Success Message:** "You have been logged out successfully."

---

### 8. User Profile

**Endpoint:** `/users/profile/`

**Methods:** `GET`, `POST`

**Description:** View and update user profile

**Authentication:** Required (login_required)

**GET Request:**
- Returns profile form with current data

**POST Request:**

**Form Data:**
- `phone`: String (optional)
- `address`: Text (optional)
- `city`: String (optional)
- `state`: String (optional)
- `zipcode`: String (optional)

**Example:**
```
POST /users/profile/
Content-Type: application/x-www-form-urlencoded

phone=1234567890&address=123 Main St&city=New York&state=NY&zipcode=10001
```

**Success Response:** Redirect to profile page

**Success Message:** "Profile updated successfully!"

---

## Cart Endpoints

### 9. View Cart

**Endpoint:** `/cart/`

**Method:** `GET`

**Description:** Display shopping cart contents

**Authentication:** Required (login_required)

**Response:** HTML page with:
- Cart items
- Quantities
- Prices
- Total
- Checkout button

---

### 10. Add to Cart

**Endpoint:** `/cart/add/<int:pk>/`

**Method:** `POST`

**Description:** Add a product to cart

**Authentication:** Required (login_required)

**URL Parameters:**
- `pk`: Product ID (integer)

**Form Data:**
- `quantity`: Integer (default: 1)

**Example:**
```
POST /cart/add/1/
Content-Type: application/x-www-form-urlencoded

quantity=2
```

**Success Response:** Redirect to cart page

**Success Message:** "{product_name} added to cart!"

**Error Message:** "Insufficient stock available."

---

### 11. Update Cart Item

**Endpoint:** `/cart/update/<int:pk>/`

**Method:** `POST`

**Description:** Update quantity of a cart item

**Authentication:** Required (login_required)

**URL Parameters:**
- `pk`: CartItem ID (integer)

**Form Data:**
- `quantity`: Integer (must be > 0)

**Example:**
```
POST /cart/update/1/
Content-Type: application/x-www-form-urlencoded

quantity=3
```

**Success Response:** Redirect to cart page

**Success Message:** "Cart updated successfully!"

**Error Messages:**
- "Insufficient stock available."
- "Item removed from cart." (if quantity = 0)

---

### 12. Remove from Cart

**Endpoint:** `/cart/remove/<int:pk>/`

**Method:** `GET`

**Description:** Remove an item from cart

**Authentication:** Required (login_required)

**URL Parameters:**
- `pk`: CartItem ID (integer)

**Example:**
```
GET /cart/remove/1/
```

**Success Response:** Redirect to cart page

**Success Message:** "{product_name} removed from cart."

---

### 13. Clear Cart

**Endpoint:** `/cart/clear/`

**Method:** `GET`

**Description:** Remove all items from cart

**Authentication:** Required (login_required)

**Example:**
```
GET /cart/clear/
```

**Success Response:** Redirect to cart page

**Success Message:** "Cart cleared successfully."

---

## Order Endpoints

### 14. Checkout Page

**Endpoint:** `/orders/checkout/`

**Method:** `GET`

**Description:** Display checkout form

**Authentication:** Required (login_required)

**Validation:**
- Cart must not be empty

**Response:** HTML page with:
- Shipping information form
- Payment method selection
- Order summary

**Error:** Redirects to cart if cart is empty

**Error Message:** "Your cart is empty."

---

### 15. Place Order

**Endpoint:** `/orders/place-order/`

**Method:** `POST`

**Description:** Process and create a new order

**Authentication:** Required (login_required)

**Form Data:**
- `full_name`: String (required)
- `email`: Email (required)
- `phone`: String (required)
- `shipping_address`: Text (required)
- `city`: String (required)
- `state`: String (required)
- `zipcode`: String (required)
- `payment_method`: String (required) - Options: 'cod', 'card', 'upi', 'netbanking'

**Example:**
```
POST /orders/place-order/
Content-Type: application/x-www-form-urlencoded

full_name=John Doe&email=john@example.com&phone=1234567890&shipping_address=123 Main St&city=New York&state=NY&zipcode=10001&payment_method=cod
```

**Success Response:** Redirect to order success page

**Success Message:** "Order placed successfully! Order ID: {order_id}"

**Error Messages:**
- "Your cart is empty."
- "Please fill all required fields."

**Side Effects:**
- Creates Order record
- Creates OrderItem records
- Reduces product stock
- Clears user's cart

---

### 16. Order Success

**Endpoint:** `/orders/success/<str:order_id>/`

**Method:** `GET`

**Description:** Display order confirmation

**Authentication:** Required (login_required)

**URL Parameters:**
- `order_id`: Order ID string (e.g., "ORD-12345678")

**Example:**
```
GET /orders/success/ORD-12345678/
```

**Response:** HTML page with:
- Order details
- Shipping information
- Ordered items
- Total amount

---

### 17. Order History

**Endpoint:** `/orders/history/`

**Method:** `GET`

**Description:** Display all orders for current user

**Authentication:** Required (login_required)

**Response:** HTML page with:
- List of all orders
- Order status
- Order dates
- Total amounts

---

### 18. Order Detail

**Endpoint:** `/orders/detail/<str:order_id>/`

**Method:** `GET`

**Description:** Display detailed information about a specific order

**Authentication:** Required (login_required)

**URL Parameters:**
- `order_id`: Order ID string

**Example:**
```
GET /orders/detail/ORD-12345678/
```

**Response:** HTML page with:
- Complete order information
- Shipping details
- Payment information
- Order items
- Order status

---

## Admin Endpoints

### 19. Admin Panel

**Endpoint:** `/admin/`

**Method:** `GET`

**Description:** Django admin interface

**Authentication:** Required (staff/superuser)

**Features:**
- Manage products
- Manage categories
- Manage orders
- Manage users
- View statistics

**Access:**
```
GET /admin/
```

Login with superuser credentials.

---

## Response Codes

| Code | Description |
|------|-------------|
| 200 | Success |
| 302 | Redirect |
| 400 | Bad Request |
| 401 | Unauthorized |
| 403 | Forbidden |
| 404 | Not Found |
| 500 | Server Error |

---

## Authentication

### Session-Based Authentication

The application uses Django's built-in session authentication:

1. **Login:** POST to `/users/login/`
2. **Session Cookie:** Automatically set by Django
3. **Protected Routes:** Require `@login_required` decorator
4. **Logout:** GET to `/users/logout/`

### CSRF Protection

All POST requests require CSRF token:

```html
<form method="post">
    {% csrf_token %}
    <!-- form fields -->
</form>
```

---

## Error Handling

### Common Error Messages

**Authentication Errors:**
- "Please login to continue."
- "Invalid username or password."

**Validation Errors:**
- "Please fill all required fields."
- "Username already exists."
- "Email already registered."
- "Passwords do not match."

**Cart Errors:**
- "Your cart is empty."
- "Insufficient stock available."
- "Product not found."

**Order Errors:**
- "Order not found."
- "Invalid order ID."

---

## Rate Limiting

Currently, no rate limiting is implemented. For production:

- Implement rate limiting on authentication endpoints
- Limit cart operations per user
- Throttle order placement

---

## Testing Endpoints

### Using cURL

**Register User:**
```bash
curl -X POST http://127.0.0.1:8000/users/register/ \
  -d "username=testuser&email=test@example.com&password=test123&confirm_password=test123"
```

**Login:**
```bash
curl -X POST http://127.0.0.1:8000/users/login/ \
  -d "username=testuser&password=test123" \
  -c cookies.txt
```

**Add to Cart:**
```bash
curl -X POST http://127.0.0.1:8000/cart/add/1/ \
  -d "quantity=2" \
  -b cookies.txt
```

### Using Python Requests

```python
import requests

# Base URL
base_url = "http://127.0.0.1:8000"

# Create session
session = requests.Session()

# Register
response = session.post(f"{base_url}/users/register/", data={
    "username": "testuser",
    "email": "test@example.com",
    "password": "test123",
    "confirm_password": "test123"
})

# Login
response = session.post(f"{base_url}/users/login/", data={
    "username": "testuser",
    "password": "test123"
})

# Add to cart
response = session.post(f"{base_url}/cart/add/1/", data={
    "quantity": 2
})

print(response.status_code)
```

---

## Webhooks

Currently not implemented. Future consideration for:
- Payment gateway integration
- Email notifications
- SMS notifications

---

## API Versioning

Current version: **v1.0**

No API versioning implemented as this is a traditional web application with server-side rendering.

---

## Support

For API support or questions:
- Email: support@eshop.com
- Documentation: README.md
- Issues: GitHub Issues

---

**Last Updated:** May 29, 2026
**Version:** 1.0
