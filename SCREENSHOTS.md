# Screenshots & UI Documentation

Visual guide to the E-Commerce Store application interface.

## 📸 Application Screenshots

### 1. Home Page

**URL:** `http://127.0.0.1:8000/`

**Features Visible:**
- Hero section with welcome message
- Shop Now call-to-action button
- Category cards with product counts
- Featured products grid (8 products)
- Features section (Fast Delivery, Secure Payment, Easy Returns, 24/7 Support)
- Navigation bar with search
- Footer with links

**Layout:**
```
┌─────────────────────────────────────────────────────┐
│  [Logo] E-Shop    Home  Products  [Search] 🔍       │
│                                    Cart Login        │
├─────────────────────────────────────────────────────┤
│                                                      │
│  Welcome to E-Shop                    🛒            │
│  Discover amazing products...                       │
│  [Shop Now]                                         │
│                                                      │
├─────────────────────────────────────────────────────┤
│           Shop by Category                          │
│  [Electronics] [Clothing] [Books] [Home]           │
├─────────────────────────────────────────────────────┤
│           Featured Products                         │
│  [Product1] [Product2] [Product3] [Product4]       │
│  [Product5] [Product6] [Product7] [Product8]       │
│                                                      │
│  [View All Products →]                              │
├─────────────────────────────────────────────────────┤
│  🚚 Fast      🔒 Secure    🔄 Easy      💬 24/7    │
│  Delivery     Payment      Returns     Support      │
└─────────────────────────────────────────────────────┘
```

---

### 2. Product Listing Page

**URL:** `http://127.0.0.1:8000/products/`

**Features Visible:**
- Sidebar with filters (Search, Category)
- Product grid (3 columns)
- Product cards with image, name, price, rating
- "View Details" buttons
- Stock availability badges
- Category badges
- Apply/Clear filters buttons

**Layout:**
```
┌──────────────┬──────────────────────────────────────┐
│  Filters     │         All Products                 │
│              │                                      │
│ [Search]     │  [Product1]  [Product2]  [Product3] │
│              │  $99.99      $199.99     $49.99     │
│ Category:    │  ⭐4.5       ⭐4.7        ⭐4.3      │
│ [All]        │  [View]      [View]      [View]     │
│ Electronics  │                                      │
│ Clothing     │  [Product4]  [Product5]  [Product6] │
│ Books        │  $19.99      $59.99      $39.99     │
│              │  ⭐4.2       ⭐4.4        ⭐4.8      │
│ [Apply]      │  [View]      [View]      [View]     │
│ [Clear]      │                                      │
└──────────────┴──────────────────────────────────────┘
```

---

### 3. Product Detail Page

**URL:** `http://127.0.0.1:8000/product/1/`

**Features Visible:**
- Large product image
- Product name and description
- Price display
- Category badge
- Rating display
- Stock availability
- Quantity selector
- Add to Cart button
- Customer reviews section
- Write a Review form
- Related products section
- Breadcrumb navigation

**Layout:**
```
┌─────────────────────────────────────────────────────┐
│  Home > Products > Wireless Headphones              │
├──────────────────────┬──────────────────────────────┤
│                      │  Wireless Headphones         │
│   [Product Image]    │  Electronics  ⭐4.5          │
│                      │                              │
│                      │  $99.99                      │
│                      │                              │
│                      │  ✅ In Stock (50 available)  │
│                      │                              │
│                      │  Description:                │
│                      │  High-quality wireless...    │
│                      │                              │
│                      │  Quantity: [1] ▼             │
│                      │  [Add to Cart]               │
├──────────────────────┴──────────────────────────────┤
│  Customer Reviews                                   │
│  ┌────────────────────────────────────────────┐    │
│  │ Write a Review                             │    │
│  │ Rating: [Select] ▼                         │    │
│  │ Comment: [____________]                    │    │
│  │ [Submit Review]                            │    │
│  └────────────────────────────────────────────┘    │
│                                                      │
│  ⭐⭐⭐⭐⭐ John Doe - May 20, 2026                  │
│  "Great product! Highly recommended."               │
├─────────────────────────────────────────────────────┤
│  Related Products                                   │
│  [Product1]  [Product2]  [Product3]  [Product4]    │
└─────────────────────────────────────────────────────┘
```

---

### 4. Registration Page

**URL:** `http://127.0.0.1:8000/users/register/`

**Features Visible:**
- Registration form
- Username field
- Email field
- Password field
- Confirm Password field
- Register button
- Link to login page
- Form validation

**Layout:**
```
┌─────────────────────────────────────────────────────┐
│              👤 Create Account                      │
├─────────────────────────────────────────────────────┤
│                                                      │
│  👤 Username:    [________________]                 │
│                                                      │
│  ✉️ Email:       [________________]                 │
│                                                      │
│  🔒 Password:    [________________]                 │
│                                                      │
│  🔒 Confirm:     [________________]                 │
│                                                      │
│  [Register]                                         │
│                                                      │
│  Already have an account? Login here                │
│                                                      │
└─────────────────────────────────────────────────────┘
```

---

### 5. Login Page

**URL:** `http://127.0.0.1:8000/users/login/`

**Features Visible:**
- Login form
- Username field
- Password field
- Login button
- Link to registration page

**Layout:**
```
┌─────────────────────────────────────────────────────┐
│              🔐 Login                               │
├─────────────────────────────────────────────────────┤
│                                                      │
│  👤 Username:    [________________]                 │
│                                                      │
│  🔒 Password:    [________________]                 │
│                                                      │
│  [Login]                                            │
│                                                      │
│  Don't have an account? Register here               │
│                                                      │
└─────────────────────────────────────────────────────┘
```

---

### 6. User Profile Page

**URL:** `http://127.0.0.1:8000/users/profile/`

**Features Visible:**
- User avatar placeholder
- Username and email display
- Profile navigation menu
- Edit profile form
- Phone, address, city, state, zipcode fields
- Update button

**Layout:**
```
┌──────────────┬──────────────────────────────────────┐
│   👤         │  ✏️ Edit Profile                     │
│  johndoe     │                                      │
│  john@...    │  Username: johndoe (disabled)        │
│              │  Email: john@... (disabled)          │
│ [Profile]    │                                      │
│ [Orders]     │  Phone: [________________]           │
│ [Cart]       │                                      │
│              │  Address: [________________]         │
│              │           [________________]         │
│              │                                      │
│              │  City:    [________]                 │
│              │  State:   [________]                 │
│              │  Zipcode: [________]                 │
│              │                                      │
│              │  [Update Profile]                    │
└──────────────┴──────────────────────────────────────┘
```

---

### 7. Shopping Cart Page

**URL:** `http://127.0.0.1:8000/cart/`

**Features Visible:**
- Cart items list
- Product images
- Product names and prices
- Quantity controls
- Remove buttons
- Subtotals
- Order summary card
- Total price
- Proceed to Checkout button
- Continue Shopping link
- Clear Cart button

**Layout:**
```
┌─────────────────────────────────────────────────────┐
│  🛒 Shopping Cart                                   │
├──────────────────────────────────┬──────────────────┤
│  [Img] Wireless Headphones       │  Order Summary  │
│        Electronics               │                  │
│        $99.99  [2] 🔄  $199.98  │  Subtotal:      │
│        [Remove]                  │  $259.97        │
│                                  │                  │
│  [Img] Smart Watch               │  Shipping:      │
│        Electronics               │  Free           │
│        $199.99 [1] 🔄  $199.99  │                  │
│        [Remove]                  │  Total:         │
│                                  │  $259.97        │
│  [← Continue Shopping]           │                  │
│  [Clear Cart]                    │  [Checkout]     │
└──────────────────────────────────┴──────────────────┘
```

---

### 8. Checkout Page

**URL:** `http://127.0.0.1:8000/orders/checkout/`

**Features Visible:**
- Shipping information form
- Full name, email, phone fields
- Address fields
- Payment method selection
- Order summary
- Place Order button

**Layout:**
```
┌──────────────────────────────────┬──────────────────┐
│  💳 Checkout                     │  Order Summary  │
│                                  │                  │
│  Shipping Information            │  Headphones x2  │
│                                  │  $199.98        │
│  Full Name: [________________]   │                  │
│  Email:     [________________]   │  Smart Watch x1 │
│  Phone:     [________________]   │  $199.99        │
│                                  │                  │
│  Address:   [________________]   │  ─────────────  │
│             [________________]   │  Subtotal:      │
│                                  │  $259.97        │
│  City:    [________]             │                  │
│  State:   [________]             │  Shipping:      │
│  Zipcode: [________]             │  Free           │
│                                  │                  │
│  Payment Method:                 │  ─────────────  │
│  ⚪ Cash on Delivery             │  Total:         │
│  ⚪ Credit/Debit Card            │  $259.97        │
│  ⚪ UPI                          │                  │
│  ⚪ Net Banking                  │                  │
│                                  │                  │
│  [✅ Place Order]                │                  │
└──────────────────────────────────┴──────────────────┘
```

---

### 9. Order Success Page

**URL:** `http://127.0.0.1:8000/orders/success/ORD-12345678/`

**Features Visible:**
- Success checkmark icon
- Order confirmation message
- Order ID display
- Order details
- Shipping information
- Ordered items list
- View Order Details button
- Continue Shopping button

**Layout:**
```
┌─────────────────────────────────────────────────────┐
│                    ✅                               │
│         Order Placed Successfully!                  │
│         Thank you for your purchase!                │
│                                                      │
│  ┌────────────────────────────────────────────┐    │
│  │ Order ID: ORD-12345678                     │    │
│  │ Please save this order ID                  │    │
│  └────────────────────────────────────────────┘    │
│                                                      │
│  Order Details                                      │
│  Order Date: May 29, 2026                          │
│  Total Amount: $259.97                             │
│  Payment Method: Cash on Delivery                  │
│  Status: Pending                                   │
│                                                      │
│  Shipping Address:                                  │
│  John Doe                                          │
│  123 Main Street                                   │
│  New York, NY 10001                                │
│  Phone: 1234567890                                 │
│                                                      │
│  Ordered Items:                                     │
│  Wireless Headphones x2  $199.98                   │
│  Smart Watch x1          $199.99                   │
│                                                      │
│  [View Order Details]  [Continue Shopping]         │
└─────────────────────────────────────────────────────┘
```

---

### 10. Order History Page

**URL:** `http://127.0.0.1:8000/orders/history/`

**Features Visible:**
- Orders table
- Order ID, Date, Total, Payment Method, Status
- View button for each order
- Status badges with colors
- Empty state message

**Layout:**
```
┌─────────────────────────────────────────────────────┐
│  📦 Order History                                   │
├─────────────────────────────────────────────────────┤
│                                                      │
│  Order ID      Date        Total    Payment  Status │
│  ORD-12345678  May 29     $259.97   COD    [Pending]│
│                                            [View]    │
│                                                      │
│  ORD-87654321  May 28     $149.99   Card  [Shipped]│
│                                            [View]    │
│                                                      │
│  ORD-11223344  May 27     $89.99    UPI [Delivered]│
│                                            [View]    │
│                                                      │
└─────────────────────────────────────────────────────┘
```

---

### 11. Order Detail Page

**URL:** `http://127.0.0.1:8000/orders/detail/ORD-12345678/`

**Features Visible:**
- Order information
- Order status and payment status
- Ordered items table
- Shipping information
- Back to Orders button
- Contact Support button

**Layout:**
```
┌──────────────────────────────────┬──────────────────┐
│  📦 Order Details                │  Shipping Info  │
│  [← Back to Orders]              │                  │
│                                  │  John Doe       │
│  Order #ORD-12345678             │  john@email.com │
│                                  │  1234567890     │
│  Order Date: May 29, 2026        │                  │
│  Status: [Pending]               │  123 Main St    │
│  Payment: Cash on Delivery       │  New York, NY   │
│  Payment Status: [Pending]       │  10001          │
│                                  │                  │
│  Ordered Items                   │  ─────────────  │
│  ┌────────────────────────────┐  │                  │
│  │ Product    Qty  Price  Sub │  │  Need Help?     │
│  │ Headphones  2  $99.99 $199 │  │  Contact our    │
│  │ Watch       1  $199.99 $199│  │  support team   │
│  │                            │  │                  │
│  │ Total:            $259.97  │  │  [Contact]      │
│  └────────────────────────────┘  │                  │
└──────────────────────────────────┴──────────────────┘
```

---

### 12. Admin Panel

**URL:** `http://127.0.0.1:8000/admin/`

**Features Visible:**
- Django admin interface
- Authentication and Authorization section
- Products, Categories, Reviews
- Cart, Orders sections
- Users, User Profiles
- Add, Change, Delete buttons
- Search functionality
- Filters

**Layout:**
```
┌─────────────────────────────────────────────────────┐
│  Django administration                              │
│  Welcome, admin                                     │
├─────────────────────────────────────────────────────┤
│                                                      │
│  AUTHENTICATION AND AUTHORIZATION                   │
│  Groups                              [Add] [Change] │
│  Users                               [Add] [Change] │
│                                                      │
│  PRODUCTS                                           │
│  Categories                          [Add] [Change] │
│  Products                            [Add] [Change] │
│  Product reviews                     [Add] [Change] │
│                                                      │
│  CART                                               │
│  Carts                               [Add] [Change] │
│  Cart items                          [Add] [Change] │
│                                                      │
│  ORDERS                                             │
│  Orders                              [Add] [Change] │
│  Order items                         [Add] [Change] │
│                                                      │
│  USERS                                              │
│  User profiles                       [Add] [Change] │
│                                                      │
└─────────────────────────────────────────────────────┘
```

---

## 🎨 Color Scheme

### Primary Colors
- **Primary Blue:** `#0d6efd` - Buttons, links, headers
- **Success Green:** `#198754` - Success messages, available status
- **Danger Red:** `#dc3545` - Error messages, delete buttons
- **Warning Yellow:** `#ffc107` - Warning messages, pending status
- **Info Cyan:** `#0dcaf0` - Info messages, processing status

### Neutral Colors
- **Dark:** `#212529` - Text, navbar
- **Light:** `#f8f9fa` - Background, cards
- **Gray:** `#6c757d` - Secondary text, borders

---

## 📱 Responsive Views

### Mobile View (320px - 767px)
- Single column layout
- Stacked navigation
- Hamburger menu
- Full-width cards
- Touch-friendly buttons
- Optimized images

### Tablet View (768px - 1023px)
- Two column layout
- Collapsible sidebar
- Medium-sized cards
- Optimized spacing

### Desktop View (1024px+)
- Multi-column layout
- Full sidebar
- Large product images
- Optimal spacing
- Hover effects

---

## 🖼️ Image Guidelines

### Product Images
- **Recommended Size:** 800x800px
- **Format:** JPG or PNG
- **Max File Size:** 2MB
- **Aspect Ratio:** 1:1 (square)

### Category Icons
- **Size:** 64x64px
- **Format:** SVG or PNG
- **Style:** Font Awesome icons

---

## 🎯 UI Components

### Buttons
- Primary (Blue)
- Secondary (Gray)
- Success (Green)
- Danger (Red)
- Warning (Yellow)
- Info (Cyan)
- Outline variants

### Cards
- Product cards
- Category cards
- Order summary cards
- Profile cards
- Info cards

### Forms
- Text inputs
- Email inputs
- Password inputs
- Number inputs
- Textareas
- Select dropdowns
- Radio buttons
- Checkboxes

### Navigation
- Top navbar
- Breadcrumbs
- Pagination (ready)
- Tabs (ready)

### Feedback
- Alerts (success, error, warning, info)
- Toasts (ready)
- Modals (ready)
- Loading spinners

---

**UI Documentation Complete! 🎨**

For actual screenshots, run the application and capture screens of each page.
