# 📢 Complaint & Announcement System - Complete Guide

## ✅ System Overview

Your E-Commerce Store has a complete complaint and announcement system:

### 🎯 How It Works:

1. **User submits complaint** (e.g., payment error, delivery issue)
2. **Admin reviews complaint** in admin panel
3. **Admin replies privately** to the user
4. **Admin marks as "announced"** if useful for all users
5. **Announcement created automatically** and shown to everyone

---

## 👤 For Users - How to Submit Complaints

### Step 1: Login to Your Account
```
http://127.0.0.1:8000/users/login/
```

### Step 2: Go to Submit Complaint
**Two ways to access:**
1. Click your **username** in navbar → Select **"Submit Complaint"**
2. Direct URL: `http://127.0.0.1:8000/users/complaints/submit/`

### Step 3: Fill the Complaint Form

**Complaint Types Available:**
- 💳 **Payment Issue** - Payment errors, failed transactions, refund issues
- 📦 **Delivery Issue** - Late delivery, damaged items, wrong address
- 🛍️ **Product Quality** - Defective products, wrong items, quality issues
- 💬 **Customer Service** - Support issues, communication problems
- 🌐 **Website Issue** - Technical problems, bugs, errors
- 📝 **Other** - Any other issues

**Required Fields:**
- **Complaint Type** - Select from dropdown
- **Subject** - Brief title (e.g., "Payment failed but money deducted")
- **Detailed Message** - Explain the issue clearly

**Optional:**
- **Upload Image** - Screenshot of error, photo of damaged item, etc.
  - Formats: JPG, PNG, GIF
  - Max size: 5MB

### Step 4: Submit
Click **"Submit Complaint"** button

### Step 5: Track Your Complaint
- Go to **"My Complaints"** from user menu
- View status: Pending → Under Review → Resolved → Closed
- Check for admin reply (private, only you can see)
- See if it was announced publicly

---

## 👨‍💼 For Admin - How to Handle Complaints

### Step 1: Access Admin Panel
```
http://127.0.0.1:8000/admin/
Login: admin / admin123
```

### Step 2: Go to Complaints Section
Click **"Complaints"** in the admin panel

### Step 3: Review Complaint
Click on any complaint to view:
- User information
- Complaint type and subject
- Detailed message
- Uploaded image (if any)
- Submission date

### Step 4: Reply to User (Private)

**In "Admin Response" section:**
1. Change **Status** to "Under Review"
2. Write **Admin Reply** (private message to user)
   - Example: "We've investigated your payment issue. The amount will be refunded within 3-5 business days."
3. System automatically sets **Replied At** timestamp

**Important:** Admin reply is **PRIVATE** - only the user who submitted the complaint can see it.

### Step 5: Create Public Announcement (Optional)

**If the complaint is useful for all users:**

**In "Public Announcement" section:**
1. Check ✅ **"Is Announced"** checkbox
2. Enter **"Announcement Title"** (or leave blank to use subject)
   - Example: "Payment Gateway Issue Resolved"
3. Click **"Save"**

**What happens:**
- System automatically creates an **Announcement**
- Announcement includes:
  - Title
  - Original complaint message
  - Admin response
  - Related image (if uploaded)
- Announcement appears on public announcements page
- All users can see it (not just the complainant)

### Step 6: Update Status
Change status as you progress:
- **Pending** → New complaint
- **Under Review** → Being investigated
- **Resolved** → Issue fixed
- **Closed** → Completed

---

## 📣 Announcements - Public Information

### For Users - View Announcements

**Access:**
- Click **"Announcements"** in navbar
- Direct URL: `http://127.0.0.1:8000/users/announcements/`

**What You'll See:**
- All active public announcements
- Announcement title and content
- Related images
- Complaint type badge
- Posted date

**Example Announcements:**
- "Payment Gateway Issue Resolved"
- "Delivery Delays Due to Weather"
- "New Payment Method Added"
- "Website Maintenance Schedule"

### For Admin - Manage Announcements

**Access:**
```
Admin Panel → Announcements
```

**You Can:**
- View all announcements
- Toggle **"Is Active"** to show/hide
- Edit title and content
- View related complaint
- Delete announcements

---

## 🔐 Password Reset Through Email

### For Users - Reset Your Password

### Step 1: Go to Login Page
```
http://127.0.0.1:8000/users/login/
```

### Step 2: Click "Forgot Password?"
Link is below the login form

### Step 3: Enter Your Email
Enter the email address associated with your account

### Step 4: Check Your Email
**In Development Mode:**
- Email is printed to the **console/terminal** where Django server is running
- Look for the password reset link in the terminal output

**In Production Mode:**
- Email is sent to your inbox
- Check spam folder if not received

### Step 5: Click Reset Link
Link format: `http://127.0.0.1:8000/users/password-reset/{uid}/{token}/`

**Link expires after 24 hours**

### Step 6: Enter New Password
- Enter new password
- Confirm new password
- Click "Reset Password"

### Step 7: Login with New Password
You can now login with your new password

---

## 📧 Email Configuration

### Current Setup (Development):

**In `ecommerce/settings.py`:**
```python
EMAIL_BACKEND = 'django.core.mail.backends.console.EmailBackend'
```

**What this means:**
- Emails are printed to the **terminal/console**
- No actual emails are sent
- Perfect for testing

**To see password reset emails:**
1. Look at the terminal where Django server is running
2. Find the email output
3. Copy the reset link
4. Paste in browser

### For Production:

**Change to SMTP:**
```python
EMAIL_BACKEND = 'django.core.mail.backends.smtp.EmailBackend'
EMAIL_HOST = 'smtp.gmail.com'  # or your email provider
EMAIL_PORT = 587
EMAIL_USE_TLS = True
EMAIL_HOST_USER = 'your-email@gmail.com'
EMAIL_HOST_PASSWORD = 'your-app-password'
DEFAULT_FROM_EMAIL = 'noreply@yourstore.com'
```

---

## 🎯 Example Workflows

### Example 1: Payment Error Complaint

**User Side:**
1. User tries to pay but gets error
2. User logs in
3. User submits complaint:
   - Type: "Payment Issue"
   - Subject: "Payment failed but money deducted"
   - Message: "I tried to pay for order #ORD-ABC123 using UPI. Payment failed but Rs. 500 was deducted from my account."
   - Image: Screenshot of payment error

**Admin Side:**
1. Admin sees complaint in admin panel
2. Admin changes status to "Under Review"
3. Admin investigates with payment gateway
4. Admin writes private reply:
   - "We've verified your payment. The amount was held temporarily and will be released back to your account within 3-5 business days. We apologize for the inconvenience."
5. Admin changes status to "Resolved"
6. Admin does NOT announce (private issue)

**Result:**
- User sees admin reply in "My Complaints"
- User's issue is resolved
- No public announcement (private matter)

---

### Example 2: Website Bug (Useful for All)

**User Side:**
1. User finds bug in checkout
2. User submits complaint:
   - Type: "Website Issue"
   - Subject: "Checkout button not working on mobile"
   - Message: "When I try to checkout on my phone, the 'Place Order' button doesn't respond. Using Chrome on Android."
   - Image: Screenshot of the issue

**Admin Side:**
1. Admin sees complaint
2. Admin changes status to "Under Review"
3. Admin fixes the bug
4. Admin writes private reply:
   - "Thank you for reporting this! We've fixed the mobile checkout issue. You can now place orders from your phone."
5. Admin checks ✅ "Is Announced"
6. Admin enters announcement title:
   - "Mobile Checkout Issue Fixed"
7. Admin clicks "Save"

**Result:**
- User sees private admin reply
- **Announcement created automatically**
- All users see announcement on announcements page:
  - Title: "Mobile Checkout Issue Fixed"
  - Content: Original complaint + Admin response
  - Image: Screenshot of the issue
- Everyone knows the bug is fixed

---

### Example 3: Forgot Password

**User Side:**
1. User can't remember password
2. User goes to login page
3. User clicks "Forgot Password?"
4. User enters email: `user@example.com`
5. User checks terminal (development) or email inbox (production)
6. User sees email with reset link
7. User clicks link
8. User enters new password
9. User logs in successfully

**Terminal Output (Development):**
```
Content-Type: text/plain; charset="utf-8"
MIME-Version: 1.0
Content-Transfer-Encoding: 7bit
Subject: Password Reset Request
From: noreply@eshop.com
To: user@example.com

Hello username,

You requested a password reset. Click the link below to reset your password:

http://127.0.0.1:8000/users/password-reset/MQ/abc123-token/

If you didn't request this, please ignore this email.

Best regards,
E-Shop Team
```

---

## 🎨 UI Features

### Complaint Status Badges:
- 🟡 **Pending** - Yellow badge
- 🔵 **Under Review** - Blue badge
- 🟢 **Resolved** - Green badge
- ⚫ **Closed** - Gray badge

### Complaint Type Badges:
- 💳 **Payment Issue** - Red badge
- 📦 **Delivery Issue** - Orange badge
- 🛍️ **Product Quality** - Purple badge
- 💬 **Customer Service** - Blue badge
- 🌐 **Website Issue** - Cyan badge
- 📝 **Other** - Gray badge

### Indicators:
- 💬 **Admin Reply Available** - Shows when admin has replied
- 📣 **Announced Publicly** - Shows if complaint was announced
- 📷 **Image Attached** - Shows if image was uploaded

---

## 📊 Admin Panel Features

### Complaint List View:
- User
- Subject
- Complaint Type
- Status (editable inline)
- Is Announced
- Has Image (✓/✗)
- Created Date

### Filters:
- Complaint Type
- Status
- Is Announced
- Created Date

### Search:
- Username
- Subject
- Message
- Admin Reply

### Complaint Detail View:
**Section 1: Complaint Information**
- User (read-only)
- Complaint Type
- Subject
- Message
- Image upload
- Image Preview (shows uploaded image)
- Created Date (read-only)

**Section 2: Admin Response**
- Status dropdown
- Admin Reply textarea (private)
- Replied At timestamp (auto-set)

**Section 3: Public Announcement**
- Is Announced checkbox
- Announcement Title field

---

## ✅ Summary

### Complaints:
- ✅ Users submit complaints about any issue
- ✅ Admin reviews and replies privately
- ✅ Status tracking (Pending → Resolved)
- ✅ Image upload support
- ✅ Private communication

### Announcements:
- ✅ Admin creates from useful complaints
- ✅ Automatically generated when marked
- ✅ Public for all users to see
- ✅ Includes original complaint + admin response
- ✅ Can be activated/deactivated

### Password Reset:
- ✅ Email-based reset system
- ✅ Secure token generation
- ✅ 24-hour expiration
- ✅ Console output (development)
- ✅ SMTP ready (production)

---

## 🎯 Key Points

1. **Complaints are PRIVATE** - Only user and admin can see
2. **Admin replies are PRIVATE** - Only sent to complaint submitter
3. **Announcements are PUBLIC** - Everyone can see
4. **Admin decides** what to announce
5. **Password reset emails** print to console in development
6. **All features are working** and ready to use

---

## 🚀 Test It Now!

### Test Complaints:
1. Login as user: `testuser` / `test123`
2. Submit a complaint about payment error
3. Login as admin: `admin` / `admin123`
4. Review complaint and reply
5. Mark as announced
6. Check announcements page

### Test Password Reset:
1. Go to login page
2. Click "Forgot Password?"
3. Enter email
4. Check terminal for reset link
5. Click link and reset password

---

**Everything is working perfectly!** 🎉

Your complaint and announcement system is fully functional and ready for users!

