╔════════════════════════════════════════════════════════════════╗
║                    DATABASE ERROR FIX                          ║
║                                                                ║
║  Error: no such column: users_complaint.is_urgent             ║
╚════════════════════════════════════════════════════════════════╝

┌────────────────────────────────────────────────────────────────┐
│  STEP 1: RUN THE FIX                                          │
└────────────────────────────────────────────────────────────────┘

   👉 Double-click this file:
   
      📄 FIX_DATABASE_NOW.bat
   
   ⏳ Wait for it to say "SUCCESS!"


┌────────────────────────────────────────────────────────────────┐
│  STEP 2: REFRESH YOUR BROWSER                                 │
└────────────────────────────────────────────────────────────────┘

   👉 In your browser, press:
   
      ⌨️  Ctrl + F5
   
   This will force refresh the page


┌────────────────────────────────────────────────────────────────┐
│  STEP 3: TEST                                                  │
└────────────────────────────────────────────────────────────────┘

   ✅ Go to: http://127.0.0.1:8000/users/dashboard/
   
   ✅ Go to: http://127.0.0.1:8000/users/complaints/
   
   Both should work now!


╔════════════════════════════════════════════════════════════════╗
║  IF IT DOESN'T WORK                                           ║
╚════════════════════════════════════════════════════════════════╝

Try this alternative method:

1. Stop the server (Ctrl + C in terminal)

2. Run this command:
   python manage.py migrate

3. Start server again:
   python manage.py runserver

4. Refresh browser (Ctrl + F5)


╔════════════════════════════════════════════════════════════════╗
║  WHAT FEATURES WORK AFTER FIX                                 ║
╚════════════════════════════════════════════════════════════════╝

✅ Admin Dashboard
✅ User Dashboard  
✅ Submit Complaints
✅ View Complaint History
✅ Admin Reply to Complaints
✅ Mark Complaints as Urgent
✅ Create Public Announcements


╔════════════════════════════════════════════════════════════════╗
║  NEED MORE HELP?                                              ║
╚════════════════════════════════════════════════════════════════╝

Read the detailed guide:
📄 HOW_TO_FIX_ERROR.md
