# 🔑 Test Credentials - E-Commerce Store

## 🌐 Website URLs

**Main Website:** http://127.0.0.1:8000/
**Admin Panel:** http://127.0.0.1:8000/admin/
**Login Page:** http://127.0.0.1:8000/users/login/
**Register Page:** http://127.0.0.1:8000/users/register/

---

## 👨‍💼 Admin Account

**For Admin Panel Access**

```
Username: admin
Password: admin123
Email: admin@example.com
```

**Admin Panel:** http://127.0.0.1:8000/admin/

**What You Can Do:**
- Manage all products
- Manage categories
- View all orders
- Manage users
- Update stock levels
- Change order status
- View statistics

---

## 👥 Test User Accounts

### User 1: Test User
```
Username: testuser
Password: test123
Email: test@example.com
Name: Test User
```

### User 2: John Doe
```
Username: john
Password: john123
Email: john@example.com
Name: John Doe
```

### User 3: Jane Smith
```
Username: jane
Password: jane123
Email: jane@example.com
Name: Jane Smith
```

### User 4: Customer Demo
```
Username: customer
Password: customer123
Email: customer@example.com
Name: Customer Demo
```

---

## 🛒 How to Test

### 1. Test as Customer (5 minutes)

**Step 1: Login**
- Go to: http://127.0.0.1:8000/users/login/
- Use any test user credentials above
- Example: `testuser` / `test123`

**Step 2: Browse Products**
- Click "Products" in navigation
- View 15 available products
- Use search and filters

**Step 3: Add to Cart**
- Click on any product
- Select quantity
- Click "Add to Cart"
- Add multiple products

**Step 4: View Cart**
- Click cart icon in navigation
- Update quantities
- Remove items if needed

**Step 5: Checkout**
- Click "Proceed to Checkout"
- Shipping info is pre-filled
- Select payment method
- Click "Place Order"

**Step 6: View Orders**
- Click "Orders" in navigation
- See your order history
- Click on order to see details

---

### 2. Test as Admin (5 minutes)

**Step 1: Login to Admin**
- Go to: http://127.0.0.1:8000/admin/
- Username: `admin`
- Password: `admin123`

**Step 2: Manage Products**
- Click "Products"
- View all 15 products
- Click "Add Product" to create new
- Edit existing products
- Update stock levels

**Step 3: Manage Orders**
- Click "Orders"
- View all customer orders
- Click on an order
- Update order status
- Update payment status

**Step 4: Manage Users**
- Click "Users"
- View all registered users
- View user details
- Manage permissions

**Step 5: Manage Categories**
- Click "Categories"
- View 4 categories
- Add new categories
- Edit existing ones

---

## 💰 Currency Settings

**Current Currency:** $ (US Dollar)

**To Change Currency:**

1. Open `ecommerce/settings.py`
2. Find these lines:
```python
CURRENCY_SYMBOL = '$'  # Change to: €, £, ₹, ¥, etc.
CURRENCY_CODE = 'USD'  # Change to: EUR, GBP, INR, JPY, etc.
```

**Examples:**
- Euro: `CURRENCY_SYMBOL = '€'` and `CURRENCY_CODE = 'EUR'`
- Pound: `CURRENCY_SYMBOL = '£'` and `CURRENCY_CODE = 'GBP'`
- Rupee: `CURRENCY_SYMBOL = '₹'` and `CURRENCY_CODE = 'INR'`
- Yen: `CURRENCY_SYMBOL = '¥'` and `CURRENCY_CODE = 'JPY'`

3. Restart the server

---

## 📦 Available Products

**15 Products across 4 categories:**

### Electronics (4 products)
- Wireless Headphones - $99.99
- Smart Watch - $199.99
- Laptop Backpack - $49.99
- Bluetooth Speaker - $79.99

### Clothing (4 products)
- Cotton T-Shirt - $19.99
- Denim Jeans - $59.99
- Running Shoes - $89.99
- Winter Jacket - $129.99

### Books (3 products)
- Python Programming - $39.99
- Web Development Book - $44.99
- Data Science Handbook - $54.99

### Home & Kitchen (4 products)
- Coffee Maker - $79.99
- Blender - $69.99
- Cookware Set - $149.99
- Air Fryer - $99.99

---

## 🎯 Quick Test Scenarios

### Scenario 1: Complete Purchase Flow
1. Login as `testuser` / `test123`
2. Browse products
3. Add 3 different products to cart
4. Go to cart and update quantities
5. Proceed to checkout
6. Complete order
7. View order confirmation
8. Check order history

### Scenario 2: Admin Management
1. Login to admin panel
2. Add a new product
3. View recent orders
4. Update an order status to "Shipped"
5. Add a new category
6. Update product stock

### Scenario 3: Multiple Users
1. Login as `john` / `john123`
2. Place an order
3. Logout
4. Login as `jane` / `jane123`
5. Place another order
6. Login to admin
7. View both orders

---

## 🔒 Security Notes

**⚠️ IMPORTANT:**

1. **Change Passwords in Production**
   - These are test credentials only
   - Use strong passwords for production
   - Change admin password immediately

2. **Don't Share Credentials**
   - Keep admin credentials secure
   - Use different passwords for each user

3. **For Development Only**
   - These credentials are for testing
   - Not suitable for production use

---

## 📝 Notes

- All test users have pre-filled profile information
- Phone: 1234567890
- Address: 123 Test Street
- City: Test City
- State: Test State
- Zipcode: 12345

- You can edit profile information after login
- Go to: Profile → Edit Profile

---

## 🆘 Troubleshooting

### Can't Login?
- Check username and password (case-sensitive)
- Make sure server is running
- Try different browser
- Clear browser cache

### Forgot Password?
- Use the credentials listed above
- Or create a new user via registration

### Admin Panel Not Working?
- Make sure you're using admin credentials
- URL must be: http://127.0.0.1:8000/admin/
- Check if server is running

---

## 📞 Quick Reference

| Account | Username | Password | Purpose |
|---------|----------|----------|---------|
| Admin | admin | admin123 | Admin panel access |
| Test User | testuser | test123 | General testing |
| John | john | john123 | Customer testing |
| Jane | jane | jane123 | Customer testing |
| Customer | customer | customer123 | Demo account |

---

**All accounts are ready to use!**

**Start testing at:** http://127.0.0.1:8000/

**Happy Testing! 🚀**

---

*Last Updated: May 29, 2026*
*E-Commerce Store v1.0*
