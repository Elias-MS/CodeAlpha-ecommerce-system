# 🚀 Apply Payment Reference Migration

## Quick Setup Guide

### Step 1: Apply the Migration

Run this command in your terminal:

```bash
python manage.py migrate orders
```

Or if `python` doesn't work, try:

```bash
py manage.py migrate orders
```

### Step 2: Verify Migration

You should see output like:

```
Running migrations:
  Applying orders.0002_order_payment_reference... OK
```

### Step 3: Test the Feature

1. **Start the server:**
   ```bash
   python manage.py runserver
   ```

2. **Go to checkout:**
   - Login with test account: `testuser` / `test123`
   - Add products to cart
   - Go to checkout: http://127.0.0.1:8000/orders/checkout/

3. **Test payment methods:**
   - Select "Cash on Delivery" → Reference field hidden ✓
   - Select "Credit/Debit Card" → Reference field appears ✓
   - Select "UPI" → Reference field appears ✓
   - Select "Net Banking" → Reference field appears ✓

4. **Place test order:**
   - Select an online payment method
   - Enter test reference: `TEST123456789`
   - Fill shipping information
   - Click "Place Order"

5. **Verify in order details:**
   - Check order success page
   - View order details
   - Payment reference should be displayed

### Step 4: Check Admin Panel

1. **Login to admin:**
   - Go to: http://127.0.0.1:8000/admin/
   - Login: `admin` / `admin123`

2. **View orders:**
   - Click "Orders"
   - See "Payment Reference" column
   - Click on an order to see details

3. **Test search:**
   - Search by payment reference number
   - Should find the order

---

## ✅ What Was Added

### Database:
- ✅ New field: `payment_reference` in Order model

### Frontend:
- ✅ Payment reference input field in checkout
- ✅ JavaScript to show/hide field based on payment method
- ✅ Display reference in order details
- ✅ Display reference in order success page

### Backend:
- ✅ Validation for online payments
- ✅ Save reference number with order
- ✅ Admin panel integration

### Admin:
- ✅ Payment reference column in order list
- ✅ Search by payment reference
- ✅ Organized fields in sections

---

## 🎯 Usage

### For COD (Cash on Delivery):
- Reference field is **hidden**
- No reference number needed
- Order placed normally

### For Online Payments (Card/UPI/Net Banking):
- Reference field **appears automatically**
- Reference number is **required**
- Must enter transaction/reference number
- Order cannot be placed without reference

---

## 📞 Need Help?

If you encounter any issues:

1. **Migration Error:**
   - Delete `orders/migrations/0002_order_payment_reference.py`
   - Run: `python manage.py makemigrations orders`
   - Run: `python manage.py migrate orders`

2. **Field Not Showing:**
   - Clear browser cache
   - Hard refresh (Ctrl + F5)
   - Check JavaScript console for errors

3. **Database Error:**
   - Backup your database first
   - Run: `python manage.py migrate --run-syncdb`

---

## 📖 Documentation

For complete documentation, see:
- **PAYMENT_REFERENCE_FEATURE.md** - Full feature documentation
- **README.md** - Main project documentation

---

**Ready to use!** 🎉

