# 💳 Payment Reference Number Feature

## Overview
The Payment Reference Number feature allows customers to enter their transaction/reference number when using online payment methods (Card, UPI, Net Banking). This helps track and verify online payments.

---

## 🎯 Features

### For Customers:
1. **Enter Payment Reference** for online payments
2. **Optional for COD** - Reference field hidden for Cash on Delivery
3. **View Reference Number** in order details and order history
4. **Track Payments** using reference number

### For Admins:
1. **View Payment References** in admin panel
2. **Search Orders** by payment reference
3. **Verify Payments** using reference numbers
4. **Filter and Sort** orders with payment information

---

## 🔄 How It Works

### Customer Workflow:

```
1. Customer adds items to cart
2. Proceeds to checkout
3. Fills shipping information
4. Selects payment method:
   
   IF Cash on Delivery (COD):
   - Reference field is hidden
   - No reference number required
   
   IF Online Payment (Card/UPI/Net Banking):
   - Reference field appears automatically
   - Customer must enter transaction/reference number
   - Field is marked as required
   
5. Submits order with payment reference
6. Views reference number in order confirmation
7. Can track order using reference number
```

### Admin Workflow:

```
1. Admin receives order notification
2. Views order in admin panel
3. Sees payment method and reference number
4. Verifies payment using reference number
5. Updates payment status to "Paid"
6. Updates order status accordingly
```

---

## 📋 Payment Methods

### Cash on Delivery (COD)
- **Reference Required:** ❌ No
- **Payment Status:** Automatically set to "Paid"
- **Reference Field:** Hidden

### Credit/Debit Card
- **Reference Required:** ✅ Yes
- **Payment Status:** Pending (until admin verifies)
- **Reference Field:** Visible and required
- **Example:** `TXN123456789`, `CARD-2024-001`

### UPI
- **Reference Required:** ✅ Yes
- **Payment Status:** Pending (until admin verifies)
- **Reference Field:** Visible and required
- **Example:** `UPI123456789`, `402912345678`

### Net Banking
- **Reference Required:** ✅ Yes
- **Payment Status:** Pending (until admin verifies)
- **Reference Field:** Visible and required
- **Example:** `NB123456789`, `REF-2024-001`

---

## 🎨 User Interface

### Checkout Page

**Payment Method Selection:**
```
○ Cash on Delivery
○ Credit/Debit Card
○ UPI
○ Net Banking
```

**Payment Reference Field (shown for online payments):**
```
Payment Reference/Transaction Number *
(Required for online payments)
┌─────────────────────────────────────┐
│ Enter transaction/reference number  │
└─────────────────────────────────────┘
Please enter your transaction ID or reference 
number after completing the payment.
```

### Order Success Page

**Order Details:**
```
Order ID: ORD-ABC12345
Order Date: May 29, 2026
Total Amount: $150.00
Payment Method: UPI
Payment Reference: UPI123456789  ← Displayed here
Order Status: Pending
```

### Order Detail Page

**Payment Information:**
```
Payment Method: Credit/Debit Card
Payment Status: Paid
Payment Reference: TXN987654321  ← Displayed here
```

---

## 👨‍💼 Admin Panel Features

### Order List View

**Columns Displayed:**
- Order ID
- User
- Total Price
- Payment Method
- **Payment Reference** ← New column
- Order Status
- Payment Status
- Created Date

### Order Detail View

**Organized in Sections:**

#### 1. Order Information
- Order ID
- User
- Total Price
- Order Status
- Created Date
- Updated Date

#### 2. Shipping Information
- Full Name
- Email
- Phone
- Shipping Address
- City, State, Zipcode

#### 3. Payment Information
- Payment Method
- **Payment Reference** ← New field
- Payment Status

### Search Functionality

Admins can search orders by:
- Order ID
- Username
- Email
- Phone
- **Payment Reference** ← New search field

---

## 🔐 Validation Rules

### Frontend Validation (JavaScript)
1. **Show/Hide Logic:**
   - COD selected → Hide reference field
   - Online payment selected → Show reference field

2. **Required Attribute:**
   - COD → Reference field not required
   - Online payment → Reference field required

### Backend Validation (Django)
1. **Required Field Check:**
   ```python
   if payment_method in ['card', 'upi', 'netbanking'] and not payment_reference:
       error: "Please enter payment reference/transaction number"
   ```

2. **Field Length:**
   - Maximum: 100 characters
   - Minimum: No minimum (can be empty for COD)

3. **Allowed Characters:**
   - Alphanumeric
   - Hyphens, underscores
   - No special validation (flexible format)

---

## 💾 Database Schema

### Order Model - New Field

```python
payment_reference = models.CharField(
    max_length=100, 
    blank=True, 
    help_text='Transaction/Reference number for online payments'
)
```

**Field Properties:**
- **Type:** CharField
- **Max Length:** 100 characters
- **Blank:** True (optional)
- **Default:** Empty string
- **Help Text:** "Transaction/Reference number for online payments"

### Migration

**File:** `orders/migrations/0002_order_payment_reference.py`

```python
operations = [
    migrations.AddField(
        model_name='order',
        name='payment_reference',
        field=models.CharField(
            blank=True, 
            help_text='Transaction/Reference number for online payments', 
            max_length=100
        ),
    ),
]
```

---

## 🚀 How to Use

### For Customers:

#### Step 1: Complete Payment
1. Make payment through your chosen method (Card/UPI/Net Banking)
2. Note down the transaction/reference number provided by your bank/payment gateway

#### Step 2: Enter Reference at Checkout
1. Go to checkout page
2. Fill shipping information
3. Select your payment method
4. **Enter the transaction/reference number** in the field that appears
5. Click "Place Order"

#### Step 3: Track Your Order
1. Save your Order ID and Payment Reference
2. View order details anytime
3. Contact support with reference number if needed

### For Admins:

#### Step 1: View Orders
1. Login to admin panel (`/admin/`)
2. Go to "Orders" section
3. See payment reference in order list

#### Step 2: Verify Payment
1. Click on an order to view details
2. Check payment method and reference number
3. Verify payment with bank/payment gateway using reference
4. Update payment status to "Paid" if verified

#### Step 3: Search by Reference
1. Use search box in admin panel
2. Enter payment reference number
3. Find specific order quickly

---

## 📊 Example Reference Numbers

### Credit/Debit Card
- `TXN123456789`
- `CARD-2024-05-29-001`
- `AUTH-987654321`
- `REF123ABC456`

### UPI
- `402912345678`
- `UPI123456789`
- `UTR-2024-001`
- `123456789012`

### Net Banking
- `NB123456789`
- `REF-2024-001`
- `NEFT-123456`
- `RTGS-987654`

---

## 🔧 Technical Implementation

### Files Modified:

1. **orders/models.py**
   - Added `payment_reference` field to Order model

2. **orders/views.py**
   - Added reference field to form data extraction
   - Added validation for online payments
   - Saves reference number with order

3. **templates/orders/checkout.html**
   - Added payment reference input field
   - Added JavaScript to show/hide field
   - Added validation messages

4. **templates/orders/order_detail.html**
   - Display payment reference in order details

5. **templates/orders/order_success.html**
   - Display payment reference in order confirmation

6. **orders/admin.py**
   - Added payment_reference to list display
   - Added to search fields
   - Organized fields in sections

7. **orders/migrations/0002_order_payment_reference.py**
   - Database migration file

---

## 🎓 Best Practices

### For Customers:

1. **Save Your Reference:**
   - Take screenshot of payment confirmation
   - Note down reference number
   - Keep for future reference

2. **Enter Correctly:**
   - Copy-paste reference number if possible
   - Double-check before submitting
   - Include all characters (no spaces)

3. **Contact Support:**
   - Provide reference number when contacting support
   - Helps quick resolution of payment issues

### For Admins:

1. **Verify Payments:**
   - Check reference number with payment gateway
   - Update payment status promptly
   - Keep records for accounting

2. **Handle Disputes:**
   - Use reference number to track payment
   - Verify with bank/gateway
   - Resolve customer issues quickly

3. **Regular Audits:**
   - Match reference numbers with bank statements
   - Identify discrepancies
   - Maintain accurate records

---

## 🐛 Troubleshooting

### Issue: Reference field not showing

**Solution:**
- Ensure you selected an online payment method (not COD)
- Refresh the page
- Clear browser cache

### Issue: Cannot submit without reference

**Solution:**
- Reference is required for online payments
- Enter your transaction/reference number
- If you don't have one, select COD instead

### Issue: Reference number not saved

**Solution:**
- Check if you entered the reference before submitting
- Verify in order details page
- Contact admin if issue persists

### Issue: Cannot find order by reference

**Solution:**
- Ensure reference number is correct
- Check for typos or extra spaces
- Search by Order ID instead

---

## 📈 Benefits

### For Business:
✅ Better payment tracking
✅ Easier payment verification
✅ Reduced payment disputes
✅ Improved accounting
✅ Faster order processing

### For Customers:
✅ Payment proof
✅ Easy order tracking
✅ Quick dispute resolution
✅ Transparent process
✅ Better support experience

---

## 🔄 Future Enhancements

Potential improvements:
- Auto-verify payments with payment gateway API
- Send payment confirmation emails with reference
- Payment reference format validation
- Payment gateway integration
- Real-time payment status updates
- Payment receipt generation
- Refund tracking with reference numbers

---

## 📝 Summary

The Payment Reference Number feature provides:
- ✅ Required reference field for online payments
- ✅ Optional for Cash on Delivery
- ✅ Automatic show/hide based on payment method
- ✅ Display in order details and admin panel
- ✅ Search functionality by reference number
- ✅ Better payment tracking and verification

**Perfect for:**
- E-commerce stores
- Online payment verification
- Payment tracking
- Customer support
- Accounting and reconciliation

---

## 🚀 Getting Started

### Apply Migration:

```bash
python manage.py migrate orders
```

### Test the Feature:

1. Go to checkout page
2. Select an online payment method
3. See reference field appear
4. Enter a test reference number
5. Place order
6. View reference in order details

### Admin Setup:

1. Login to admin panel
2. Go to Orders section
3. View payment reference column
4. Search by reference number
5. Verify payments

---

**Last Updated:** May 29, 2026
**Version:** 1.0
**Feature Status:** ✅ Active

