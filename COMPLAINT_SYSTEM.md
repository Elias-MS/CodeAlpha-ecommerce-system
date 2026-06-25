# 📢 Complaint System Documentation

## Overview
The Complaint System allows users to submit complaints with images, receive private admin replies, and view public announcements when complaints are deemed useful for all customers.

---

## 🎯 Features

### For Users:
1. **Submit Complaints** with optional images
2. **Track Complaint Status** (Pending, Under Review, Resolved, Closed)
3. **Receive Private Admin Replies**
4. **View Public Announcements** from useful complaints
5. **Upload Clear Images** to support complaints

### For Admins:
1. **View All Complaints** in admin panel
2. **Reply Privately** to users
3. **Change Complaint Status**
4. **Announce Useful Complaints** to all users
5. **View Image Previews** in admin panel
6. **Manage Announcements**

---

## 📊 Database Models

### Complaint Model
```python
Fields:
- user (ForeignKey to User)
- complaint_type (CharField with choices)
- subject (CharField, max 200)
- message (TextField)
- image (ImageField, optional)
- status (CharField with choices)
- admin_reply (TextField, private)
- is_announced (Boolean)
- announcement_title (CharField)
- created_at (DateTime)
- updated_at (DateTime)
- replied_at (DateTime, nullable)

Complaint Types:
- Product Quality
- Delivery Issue
- Customer Service
- Payment Issue
- Website Issue
- Other

Status Options:
- Pending
- Under Review
- Resolved
- Closed
```

### Announcement Model
```python
Fields:
- complaint (OneToOneField to Complaint)
- title (CharField, max 200)
- content (TextField)
- created_at (DateTime)
- is_active (Boolean)
```

---

## 🔗 URLs

| URL | View | Description | Access |
|-----|------|-------------|--------|
| `/users/complaints/submit/` | submit_complaint | Submit new complaint | Logged-in users |
| `/users/complaints/` | my_complaints | View user's complaints | Logged-in users |
| `/users/complaints/<id>/` | complaint_detail | View complaint details | Logged-in users |
| `/users/announcements/` | announcements | View public announcements | Everyone |

---

## 🎨 User Interface

### 1. Submit Complaint Page
**URL:** `/users/complaints/submit/`

**Features:**
- Complaint type dropdown (6 options)
- Subject field (required)
- Detailed message textarea (required)
- Image upload (optional)
  - Accepts: JPG, PNG, GIF
  - Max size: 5MB
  - Clear image requirement
- Submit button
- Link to view existing complaints

**Form Fields:**
```html
- Complaint Type* (dropdown)
- Subject* (text input)
- Detailed Message* (textarea)
- Upload Image (file input, optional)
```

### 2. My Complaints Page
**URL:** `/users/complaints/`

**Displays:**
- All user's complaints in card layout
- Complaint type badge
- Status badge (color-coded)
- Subject and truncated message
- Image thumbnail (if uploaded)
- Submission date
- Admin reply indicator
- Public announcement indicator
- View details button

**Status Colors:**
- Pending: Yellow
- Under Review: Blue
- Resolved: Green
- Closed: Gray

### 3. Complaint Detail Page
**URL:** `/users/complaints/<id>/`

**Shows:**
- Full complaint details
- Uploaded image (full size)
- Status and type badges
- Admin reply (if available)
- Reply timestamp
- Public announcement notice
- Complaint ID
- Last updated date

### 4. Announcements Page
**URL:** `/users/announcements/`

**Features:**
- Public announcements from useful complaints
- Announcement title and content
- Related images
- Posted date
- Category tag
- Info section about announcements

---

## 👨‍💼 Admin Panel Features

### Complaint Management

**Admin URL:** `/admin/users/complaint/`

**List View Displays:**
- User
- Subject
- Complaint Type
- Status (editable inline)
- Is Announced
- Has Image (✓/✗)
- Created Date

**Detail View Sections:**

#### 1. Complaint Information
- User (read-only)
- Complaint Type
- Subject
- Message
- Image upload
- Image Preview (shows uploaded image)
- Created Date (read-only)

#### 2. Admin Response
- Status dropdown
- Admin Reply textarea (private)
- Replied At timestamp (auto-set)

**Note:** Admin reply is sent privately to the user only.

#### 3. Public Announcement
- Is Announced checkbox
- Announcement Title field

**How it works:**
1. Admin reviews complaint
2. Admin writes private reply
3. If complaint is useful, check "Is Announced"
4. Enter announcement title (or use subject)
5. Save - announcement is created automatically

### Announcement Management

**Admin URL:** `/admin/users/announcement/`

**Features:**
- View all announcements
- Toggle active/inactive
- Edit title and content
- View related complaint
- Delete announcements

---

## 🔄 Workflow

### User Workflow:

```
1. User logs in
2. Navigates to "Submit Complaint" (from user menu)
3. Fills complaint form
4. Optionally uploads clear image
5. Submits complaint
6. Views "My Complaints" to track status
7. Receives private admin reply
8. If announced, sees it in public announcements
```

### Admin Workflow:

```
1. Admin logs into admin panel
2. Goes to Complaints section
3. Reviews complaint and image
4. Changes status to "Under Review"
5. Writes private reply to user
6. If complaint is useful:
   - Checks "Is Announced"
   - Enters announcement title
   - Saves (announcement auto-created)
7. Changes status to "Resolved"
```

---

## 📸 Image Upload Guidelines

### For Users:
- **Upload clear, specific images** related to your complaint
- **Supported formats:** JPG, PNG, GIF
- **Maximum size:** 5MB
- **Examples:**
  - Product defect photos
  - Delivery issue screenshots
  - Payment error screenshots
  - Website bug screenshots

### Image Requirements:
- Clear and focused
- Good lighting
- Relevant to complaint
- Shows the issue clearly
- Not blurry or pixelated

---

## 💡 Best Practices

### For Users:

1. **Be Specific:**
   - Provide detailed information
   - Include order numbers if applicable
   - Mention dates and times

2. **Upload Clear Images:**
   - Take clear photos
   - Ensure good lighting
   - Show the issue clearly

3. **Choose Correct Type:**
   - Select appropriate complaint type
   - Helps admin route to right team

4. **Check Status:**
   - Monitor complaint status
   - Read admin replies promptly

### For Admins:

1. **Respond Promptly:**
   - Review complaints regularly
   - Reply within 24-48 hours
   - Update status appropriately

2. **Private Replies:**
   - Keep sensitive info private
   - Use admin reply for personal responses
   - Be professional and helpful

3. **Public Announcements:**
   - Only announce useful complaints
   - Edit content for clarity
   - Remove sensitive information
   - Make title descriptive

4. **Image Review:**
   - Check uploaded images
   - Verify complaint validity
   - Use images as evidence

---

## 🔐 Security & Privacy

### Privacy Features:
- ✅ Admin replies are **private** (only user can see)
- ✅ User can only view their own complaints
- ✅ Announcements are **sanitized** (no personal info)
- ✅ Images are stored securely
- ✅ Login required to submit complaints

### Data Protection:
- User information is protected
- Only admins can see all complaints
- Personal details not shown in announcements
- Secure file upload handling

---

## 📱 Responsive Design

All complaint pages are fully responsive:
- Mobile-friendly forms
- Touch-friendly buttons
- Responsive image display
- Optimized for all screen sizes

---

## 🎨 UI Components

### Status Badges:
- **Pending:** Yellow badge with dark text
- **Under Review:** Blue badge
- **Resolved:** Green badge
- **Closed:** Gray badge

### Icons Used:
- 📢 Complaint submission
- 📋 My complaints list
- 👁️ View details
- 💬 Admin reply
- 📣 Public announcement
- 📷 Image upload
- ✅ Resolved status

---

## 🔔 Notifications

### User Notifications:
- Success message on complaint submission
- Alert when admin replies
- Notice when complaint is announced

### Admin Notifications:
- New complaints appear in admin panel
- Can filter by status and type
- Search functionality available

---

## 📊 Admin Panel Filters

### Available Filters:
- Complaint Type
- Status
- Is Announced
- Created Date

### Search Fields:
- Username
- Subject
- Message
- Admin Reply

---

## 🎯 Use Cases

### Example 1: Product Quality Issue
```
User: Submits complaint about defective product
      Uploads clear photo of defect
Admin: Reviews complaint and image
       Replies with return instructions (private)
       Marks as "Resolved"
```

### Example 2: Useful Complaint
```
User: Reports website bug affecting checkout
      Provides detailed steps to reproduce
Admin: Confirms bug, fixes it
       Replies with fix confirmation (private)
       Announces to all users about the fix
       Other users see announcement
```

### Example 3: Delivery Issue
```
User: Complains about late delivery
      Uploads tracking screenshot
Admin: Investigates with shipping partner
       Provides update and compensation (private)
       Marks as "Resolved"
```

---

## 🚀 Getting Started

### For Users:

1. **Login** to your account
2. Click your **username** in navigation
3. Select **"Submit Complaint"**
4. Fill out the form
5. Upload image (optional)
6. Click **"Submit Complaint"**
7. Track status in **"My Complaints"**

### For Admins:

1. Login to **admin panel** (`/admin/`)
2. Go to **"Complaints"** section
3. Click on a complaint to review
4. View image preview
5. Write **admin reply**
6. Update **status**
7. Check **"Is Announced"** if useful
8. **Save** the complaint

---

## 📈 Statistics

### Complaint Metrics:
- Total complaints submitted
- Complaints by type
- Complaints by status
- Average response time
- Announced complaints count

**View in Admin Panel:** Django admin provides built-in filtering and counting.

---

## 🔧 Technical Details

### File Upload:
- **Upload Directory:** `media/complaints/`
- **Allowed Formats:** JPG, JPEG, PNG, GIF
- **Max Size:** 5MB (configurable)
- **Validation:** Django ImageField validation

### Database Relationships:
```
User (1) -----> (Many) Complaint
Complaint (1) -----> (1) Announcement
```

### Auto-Timestamps:
- `created_at`: Set on creation
- `updated_at`: Updated on save
- `replied_at`: Set when admin replies

---

## 🎓 Tips & Tricks

### For Better Complaints:
1. Use descriptive subjects
2. Provide step-by-step details
3. Include relevant order/product info
4. Upload clear, focused images
5. Check FAQ before submitting

### For Efficient Admin Management:
1. Use filters to prioritize
2. Reply to pending complaints first
3. Update status as you progress
4. Announce helpful solutions
5. Keep announcements concise

---

## 🐛 Troubleshooting

### Common Issues:

**Issue:** Image won't upload
- **Solution:** Check file size (max 5MB) and format (JPG, PNG, GIF)

**Issue:** Can't see admin reply
- **Solution:** Refresh page, check complaint detail page

**Issue:** Announcement not showing
- **Solution:** Admin must check "Is Announced" and save

**Issue:** Can't submit complaint
- **Solution:** Ensure all required fields are filled

---

## 📞 Support

For issues with the complaint system:
1. Check this documentation
2. Contact admin through Contact Us page
3. Submit a complaint about the complaint system 😊

---

## 🔄 Future Enhancements

Potential improvements:
- Email notifications for replies
- Complaint categories with auto-routing
- File attachments (PDFs, documents)
- Complaint rating system
- Admin response templates
- Bulk complaint management
- Analytics dashboard
- Export complaints to CSV

---

## 📝 Summary

The Complaint System provides:
- ✅ Easy complaint submission with images
- ✅ Private admin replies
- ✅ Public announcements for useful complaints
- ✅ Status tracking
- ✅ Secure and private
- ✅ Mobile-friendly
- ✅ Admin-friendly management

**Perfect for:**
- Customer support
- Issue tracking
- Quality improvement
- Customer communication
- Public transparency

---

**Last Updated:** May 29, 2026
**Version:** 1.0
