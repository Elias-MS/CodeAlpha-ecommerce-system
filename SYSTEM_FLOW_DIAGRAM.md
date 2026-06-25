# 📊 System Flow Diagram

## 🔄 Complete Workflow

```
┌─────────────────────────────────────────────────────────────────┐
│                    COMPLAINT & ANNOUNCEMENT SYSTEM               │
└─────────────────────────────────────────────────────────────────┘

┌──────────────┐
│   👤 USER    │
└──────┬───────┘
       │
       │ 1. Has Issue (Payment Error, Delivery Problem, etc.)
       │
       ▼
┌──────────────────────┐
│  Submit Complaint    │
│  ─────────────────   │
│  • Type: Payment     │
│  • Subject: Error    │
│  • Message: Details  │
│  • Image: Screenshot │
└──────┬───────────────┘
       │
       │ 2. Complaint Saved to Database
       │
       ▼
┌──────────────────────┐
│   Status: PENDING    │
│   🟡 Waiting Review  │
└──────┬───────────────┘
       │
       │ 3. Admin Notified
       │
       ▼
┌──────────────────────┐
│   👨‍💼 ADMIN PANEL    │
│  ─────────────────   │
│  Views Complaint:    │
│  • User Info         │
│  • Complaint Details │
│  • Image Preview     │
└──────┬───────────────┘
       │
       │ 4. Admin Reviews
       │
       ▼
┌──────────────────────────────────────────────────────────┐
│                    ADMIN DECISION                         │
└──────┬───────────────────────────────────────────────────┘
       │
       ├─────────────────────┬─────────────────────────────┐
       │                     │                             │
       ▼                     ▼                             ▼
┌──────────────┐    ┌──────────────┐           ┌──────────────┐
│   PRIVATE    │    │   PRIVATE    │           │   PUBLIC     │
│   REPLY      │    │   REPLY      │           │ ANNOUNCEMENT │
│   ONLY       │    │     +        │           │              │
└──────┬───────┘    │ ANNOUNCEMENT │           └──────┬───────┘
       │            └──────┬───────┘                  │
       │                   │                          │
       │ 5a. Admin         │ 5b. Admin                │ 5c. Admin
       │ writes reply      │ writes reply             │ marks as
       │                   │ + checks                 │ announced
       │                   │ "Is Announced"           │
       ▼                   ▼                          ▼
┌──────────────┐    ┌──────────────┐           ┌──────────────┐
│ User sees    │    │ User sees    │           │ Everyone     │
│ reply in     │    │ reply in     │           │ sees on      │
│ "My          │    │ "My          │           │ Announce-    │
│ Complaints"  │    │ Complaints"  │           │ ments page   │
└──────────────┘    │              │           └──────────────┘
                    │ + Everyone   │
                    │ sees on      │
                    │ Announce-    │
                    │ ments page   │
                    └──────────────┘

═══════════════════════════════════════════════════════════════

                    EXAMPLE SCENARIOS

═══════════════════════════════════════════════════════════════

SCENARIO 1: PRIVATE ISSUE (Payment Error)
──────────────────────────────────────────

User: "Payment failed but money deducted"
  ↓
Admin: Reviews → Investigates → Replies privately
  ↓
Admin Reply: "Amount will be refunded in 3-5 days"
  ↓
Status: Resolved
  ↓
Result: ✅ User sees reply (PRIVATE)
        ❌ No public announcement


SCENARIO 2: PUBLIC ISSUE (Website Bug)
───────────────────────────────────────

User: "Checkout button not working on mobile"
  ↓
Admin: Reviews → Fixes bug → Replies + Announces
  ↓
Admin Reply: "Bug fixed! You can now checkout on mobile"
  ↓
Admin: ✅ Checks "Is Announced"
  ↓
Announcement Created: "Mobile Checkout Issue Fixed"
  ↓
Result: ✅ User sees reply (PRIVATE)
        ✅ Everyone sees announcement (PUBLIC)


═══════════════════════════════════════════════════════════════

                PASSWORD RESET FLOW

═══════════════════════════════════════════════════════════════

┌──────────────┐
│   👤 USER    │
│ Forgot Pass  │
└──────┬───────┘
       │
       │ 1. Clicks "Forgot Password?"
       │
       ▼
┌──────────────────────┐
│  Enter Email Address │
└──────┬───────────────┘
       │
       │ 2. Submits email
       │
       ▼
┌──────────────────────┐
│  System Generates:   │
│  • Unique Token      │
│  • Reset Link        │
│  • Expiry (24h)      │
└──────┬───────────────┘
       │
       │ 3. Sends Email
       │
       ├─────────────────────┬─────────────────────┐
       │                     │                     │
       ▼                     ▼                     ▼
┌──────────────┐    ┌──────────────┐    ┌──────────────┐
│ DEVELOPMENT  │    │  PRODUCTION  │    │   TERMINAL   │
│    MODE      │    │     MODE     │    │    OUTPUT    │
└──────┬───────┘    └──────┬───────┘    └──────┬───────┘
       │                   │                    │
       │ Email printed     │ Email sent         │ Shows:
       │ to console        │ to inbox           │ • Subject
       │                   │                    │ • From
       ▼                   ▼                    │ • To
┌──────────────┐    ┌──────────────┐           │ • Link
│ Check        │    │ Check        │           │
│ Terminal     │    │ Email Inbox  │           ▼
└──────┬───────┘    └──────┬───────┘    ┌──────────────┐
       │                   │             │ Copy Link    │
       └───────────────────┴─────────────┴──────┬───────┘
                                                 │
                                                 │ 4. User clicks link
                                                 │
                                                 ▼
                                        ┌──────────────────────┐
                                        │  Reset Password Page │
                                        │  ─────────────────   │
                                        │  • Enter new pass    │
                                        │  • Confirm pass      │
                                        └──────┬───────────────┘
                                               │
                                               │ 5. Submits new password
                                               │
                                               ▼
                                        ┌──────────────────────┐
                                        │  Password Updated!   │
                                        │  ✅ Success          │
                                        └──────┬───────────────┘
                                               │
                                               │ 6. Redirects to login
                                               │
                                               ▼
                                        ┌──────────────────────┐
                                        │  Login with New Pass │
                                        │  ✅ Access Granted   │
                                        └──────────────────────┘

═══════════════════════════════════════════════════════════════

                    KEY DIFFERENCES

═══════════════════════════════════════════════════════════════

┌─────────────────────┬──────────────┬──────────────┐
│      FEATURE        │  COMPLAINT   │ ANNOUNCEMENT │
├─────────────────────┼──────────────┼──────────────┤
│ Who Submits?        │ User         │ Admin        │
│ Who Can See?        │ User + Admin │ Everyone     │
│ Admin Reply?        │ Private      │ Public       │
│ Purpose?            │ Report Issue │ Inform All   │
│ Image Upload?       │ Yes          │ From Complaint│
│ Status Tracking?    │ Yes          │ Active/Inactive│
└─────────────────────┴──────────────┴──────────────┘

═══════════════════════════════════════════════════════════════

                    ADMIN WORKFLOW

═══════════════════════════════════════════════════════════════

Step 1: Login to Admin Panel
  ↓
Step 2: Go to "Complaints"
  ↓
Step 3: Click on Complaint
  ↓
Step 4: Review Details
  ├─ User info
  ├─ Complaint type
  ├─ Message
  └─ Image (if uploaded)
  ↓
Step 5: Change Status to "Under Review"
  ↓
Step 6: Write Admin Reply (Private)
  ↓
Step 7: Decide:
  ├─ Private Issue? → Just save
  └─ Public Issue? → Check "Is Announced" + Add title
  ↓
Step 8: Click "Save"
  ↓
Step 9: System Automatically:
  ├─ Sets "Replied At" timestamp
  ├─ Saves admin reply
  └─ Creates announcement (if checked)
  ↓
Step 10: Change Status to "Resolved"

═══════════════════════════════════════════════════════════════

                    USER WORKFLOW

═══════════════════════════════════════════════════════════════

Step 1: Login to Account
  ↓
Step 2: Click Username → "Submit Complaint"
  ↓
Step 3: Fill Form:
  ├─ Select Type (Payment, Delivery, etc.)
  ├─ Enter Subject
  ├─ Write Message
  └─ Upload Image (optional)
  ↓
Step 4: Click "Submit Complaint"
  ↓
Step 5: Go to "My Complaints"
  ↓
Step 6: Track Status:
  ├─ 🟡 Pending
  ├─ 🔵 Under Review
  ├─ 🟢 Resolved
  └─ ⚫ Closed
  ↓
Step 7: Check for Admin Reply
  ↓
Step 8: See if Announced Publicly

═══════════════════════════════════════════════════════════════

                    SUMMARY

═══════════════════════════════════════════════════════════════

✅ COMPLAINTS
   • Users report issues
   • Admin replies privately
   • Status tracking
   • Image support

✅ ANNOUNCEMENTS
   • Admin creates from complaints
   • Public for everyone
   • Includes admin response
   • Can be activated/deactivated

✅ PASSWORD RESET
   • Email-based system
   • Secure token
   • 24-hour expiry
   • Console output (dev)

═══════════════════════════════════════════════════════════════

All systems are WORKING and READY! 🎉

